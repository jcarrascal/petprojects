using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.flashpunk
{
    /// <summary>
    /// Main game Sprite class, added to the Flash Stage. Manages the game loop.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// If the game should stop updating/rendering.
        /// </summary>
        public bool paused = false;

        /// <summary>
        /// Cap on the elapsed time (default at 30 FPS). Raise this to allow for lower framerates (eg. 1 / 10).
        /// </summary>
        public float maxElapsed = 0.0333f;

        /// <summary>
        /// The max amount of frames that can be skipped in fixed framerate mode.
        /// </summary>
        public uint maxFrameSkip = 5;

        /// <summary>
        /// The amount of milliseconds between ticks in fixed framerate mode.
        /// </summary>
        public uint tickRate = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Engine()
            : this(FP.graphicsDeviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width,
                   FP.graphicsDeviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height,
                   30, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Engine(int width, int height)
            : this(width, height, 30, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="frameRate">The frame rate.</param>
        public Engine(int width, int height, float frameRate)
            : this(width, height, frameRate, false)
        {
        }

        /// <summary>
        /// Constructor. Defines startup information about your game.
        /// </summary>
        /// <param name="width">The width of your game.</param>
        /// <param name="height">The height of your game.</param>
        /// <param name="frameRate">The game framerate, in frames per second.</param>
        /// <param name="isFixed">if set to <c>true</c> a fixed-framerate should be used.</param>
        public Engine(int width, int height, float frameRate, bool isFixed)
        {
            // XNA Initialization
            FP.graphicsDeviceManager.PreferredBackBufferWidth = width;
            FP.graphicsDeviceManager.PreferredBackBufferHeight = height;
            FP.graphicsDeviceManager.IsFullScreen = true;
            FP.game.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1000 / 30);
            FP.game.IsFixedTimeStep = isFixed;

            // global game properties
            FP.width = width;
            FP.height = height;
            FP.assignedFrameRate = frameRate;
            FP.isFixed = isFixed;

            // global game objects
            FP.engine = this;
            FP.screen = new Screen();
            FP.bounds = new Rectangle(0, 0, width, height);
            FP._world = new World();

            // miscellanious startup stuff
            if (FP.randomSeed == 0) FP.randomizeSeed();
            FP.entity = new Entity();
            FP._time = DateTime.Now.Millisecond;

            // on-stage event listener
            //addEventListener(Event.ADDED_TO_STAGE, onStage);
        }

        /// <summary>
        /// Override this, called after Engine has been added to the stage.
        /// </summary>
        public virtual void init()
        {

        }

        /// <summary>
        /// Updates the game, updating the World and Entities.
        /// </summary>
        public virtual void update()
        {
            if (FP._world.active)
            {
                if (FP._world._tween != null) FP._world.updateTweens();
                FP._world.update();
            }
            FP._world.updateLists();
            if (FP._goto != null) checkWorld();
        }

        /// <summary>
        /// Renders the game, rendering the World and Entities.
        /// </summary>
        public virtual void render()
        {
            // timing stuff
            uint t = (uint)(DateTime.Now.Ticks / 10000);
            if (_frameLast != 0) _frameLast = t;

            // render loop
            FP.screen.swap();
            //Draw.resetTarget();
            FP.screen.refresh();
            if (FP._world.visible) FP._world.render();
            FP.screen.redraw();

            // more timing stuff
            //t = (uint)(DateTime.Now.Ticks / 10000);
            //_frameList.Add(t - _frameLast);
            //_frameListSum += t - _frameLast;
            //if (_frameList.Count > 10) { _frameListSum -= _frameList[0]; _frameList.RemoveAt(0); }
            //FP.frameRate = 1000 / (_frameListSum / _frameList.Count);
            //_frameLast = t;
        }

        /// <summary>
        /// Sets the game's stage properties. Override this to set them differently.
        /// </summary>
        public virtual void setStageProperties()
        {
            //stage.frameRate = FP.assignedFrameRate;
            //stage.align = StageAlign.TOP_LEFT;
            //stage.quality = StageQuality.HIGH;
            //stage.scaleMode = StageScaleMode.NO_SCALE;
            //stage.displayState = StageDisplayState.NORMAL;
        }

#if false
		/** @private Event handler for stage entry. */
		private void onStage(Event e)
		{
			// remove event listener
			removeEventListener(Event.ADDED_TO_STAGE, onStage);
			
			// set stage properties
			FP.stage = stage;
			setStageProperties();
			
			// enable input
			Input.enable();
			
			// switch worlds
			if (FP._goto) checkWorld();
			
			// game start
			init();
			
			// start game loop
			_rate = 1000 / FP.assignedFrameRate;
			if (FP.fixed)
			{
				// fixed framerate
				_skip = _rate * maxFrameSkip;
				_last = _prev = getTimer();
				_timer = new Timer(tickRate);
				_timer.addEventListener(TimerEvent.TIMER, onTimer);
				_timer.start();
			}
			else
			{
				// nonfixed framerate
				_last = getTimer();
				addEventListener(Event.ENTER_FRAME, onEnterFrame);
			}
		}

        /** @private Framerate independent game loop. */
		private void onEnterFrame(Event e)
		{
			// update timer
			_time = _gameTime = getTimer();
			FP._flashTime = _time - _flashTime;
			_updateTime = _time;
			FP.elapsed = (_time - _last) / 1000;
			if (FP.elapsed > maxElapsed) FP.elapsed = maxElapsed;
			FP.elapsed *= FP.rate;
			_last = _time;
			
			// update console
			if (FP._console) FP._console.update();
			
			// update loop
			if (!paused) update();
			
			// update input
			Input.update();
			
			// update timer
			_time = _renderTime = getTimer();
			FP._updateTime = _time - _updateTime;
			
			// render loop
			if (!paused) render();
			
			// update timer
			_time = _flashTime = getTimer();
			FP._renderTime = _time - _renderTime;
			FP._gameTime = _time - _gameTime;
		}
		
		/** @private Fixed framerate game loop. */
		private void onTimer(TimerEvent e)
		{
			// update timer
			_time = getTimer();
			_delta += (_time - _last);
			_last = _time;
			
			// quit if a frame hasn't passed
			if (_delta < _rate) return;
			
			// update timer
			_gameTime = _time;
			FP._flashTime = _time - _flashTime;
			
			// update console
			if (FP._console) FP._console.update();
			
			// update loop
			if (_delta > _skip) _delta = _skip;
			while (_delta > _rate)
			{
				// update timer
				_updateTime = _time;
				_delta -= _rate;
				FP.elapsed = (_time - _prev) / 1000;
				if (FP.elapsed > maxElapsed) FP.elapsed = maxElapsed;
				FP.elapsed *= FP.rate;
				_prev = _time;
				
				// update loop
				if (!paused) update();
				
				// update input
				Input.update();
				
				// update timer
				_time = getTimer();
				FP._updateTime = _time - _updateTime;
			}
			
			// update timer
			_renderTime = _time;
			
			// render loop
			if (!paused) render();
			
			// update timer
			_time = _flashTime = getTimer();
			FP._renderTime = _time - _renderTime;
			FP._gameTime =  _time - _gameTime;
		}
#endif

        /// <summary>
        /// Switch Worlds if they've changed.
        /// </summary>
        private void checkWorld()
        {
            if (FP._goto == null) return;
            FP._world.end();
            FP._world.updateLists();
            if (FP._world != null && FP._world.autoClear && FP._world._tween != null) FP._world.clearTweens();
            FP._world = FP._goto;
            FP._goto = null;
            FP.camera = FP._world.camera;
            FP._world.updateLists();
            FP._world.begin();
            FP._world.updateLists();
        }

        internal void Update(GameTime gameTime)
        {
            // update console
            //if (FP._console) FP._console.update();

            FP.elapsed = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            // update loop
            if (!paused) update();

            // update input
            //Input.update();
        }

        internal void Draw(GameTime gameTime)
        {
            // render loop
            if (!paused) render();
        }

        // Timing information.
        private double _delta = 0;
        private double _time;
        private double _last;
        private Timer _timer;
        private float _rate;
        private float _skip;
        private float _prev;

        // Debug timing information.
        private uint _updateTime;
        private uint _renderTime;
        private uint _gameTime;
        private uint _flashTime;

        // FrameRate tracking.
        private uint _frameLast = 0;
        private uint _frameListSum = 0;
        private List<uint> _frameList = new List<uint>();
    }
}