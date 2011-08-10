using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace net.flashpunk.graphics
{
    /// <summary>
    /// Performance-optimized animated Image. Can have multiple animations,
    /// which draw frames from the provided source image to the screen.
    /// </summary>
    public class Spritemap : Image
    {
        /// <summary>
        /// If the animation has stopped.
        /// </summary>
        public bool complete = true;

        /// <summary>
        /// Optional callback for animation end.
        /// </summary>
        public Action callback;

        /// <summary>
        /// Animation speed factor, alter this to speed up/slow down all animations.
        /// </summary>
        public float rate = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spritemap"/> class.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <param name="frameWidth">Frame width.</param>
        /// <param name="frameHeight">Frame height.</param>
        /// <param name="callback">Optional callback for animation end.</param>
        public Spritemap(Texture2D source)
            : this(source, 0, 0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spritemap"/> class.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <param name="frameWidth">Frame width.</param>
        /// <param name="frameHeight">Frame height.</param>
        /// <param name="callback">Optional callback for animation end.</param>
        public Spritemap(Texture2D source, uint frameWidth)
            : this(source, frameWidth, 0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spritemap"/> class.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <param name="frameWidth">Frame width.</param>
        /// <param name="frameHeight">Frame height.</param>
        /// <param name="callback">Optional callback for animation end.</param>
        public Spritemap(Texture2D source, uint frameWidth, uint frameHeight)
            : this(source, frameWidth, frameHeight, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spritemap"/> class.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <param name="frameWidth">Frame width.</param>
        /// <param name="frameHeight">Frame height.</param>
        /// <param name="callback">Optional callback for animation end.</param>
        public Spritemap(Texture2D source, uint frameWidth, uint frameHeight, Action callback)
            : base(source, new Rectangle(0, 0, (int)frameWidth, (int)frameHeight))
        {
            if (frameWidth == 0) _sourceRect.Width = this.source.Width;
            if (frameHeight == 0) _sourceRect.Height = this.source.Height;
            _width = this.source.Width;
            _height = this.source.Height;
            _columns = _width / _sourceRect.Width;
            _rows = _height / _sourceRect.Height;
            _frameCount = _columns * _rows;
            this.callback = callback;
            updateBuffer();
            active = true;
        }

        /// <summary>
        /// Updates the spritemap's buffer.
        /// </summary>
        public void updateBuffer()
        {
            // get position of the current frame
            _sourceRect.X = _sourceRect.Width * _frame;
            _sourceRect.Y = (_sourceRect.X / _width) * _sourceRect.Height;
            _sourceRect.X %= _width;
            if (flipped) _sourceRect.X = (_width - _sourceRect.Width) - _sourceRect.X;

            // update the buffer
            //base.updateBuffer(clearBefore);
        }

        /** @private Updates the animation. */
        override public void update()
        {
            if (_anim != null && !complete)
            {
                _timer += (FP.isFixed ? _anim._frameRate : _anim._frameRate * FP.elapsed) * rate;
                if (_timer >= 1)
                {
                    while (_timer >= 1)
                    {
                        _timer--;
                        _index++;
                        if (_index == _anim._frameCount)
                        {
                            if (_anim._loop)
                            {
                                _index = 0;
                                if (callback != null) callback();
                            }
                            else
                            {
                                _index = _anim._frameCount - 1;
                                complete = true;
                                if (callback != null) callback();
                                break;
                            }
                        }
                    }
                    if (_anim != null) _frame = _anim._frames[_index];
                    updateBuffer();
                }
            }
        }

        /// <summary>
        /// Add an Animation.
        /// </summary>
        /// <param name="name">Name of the animation.</param>
        /// <param name="frames">Array of frame indices to animate through.</param>
        /// <returns>
        /// A new Anim object for the animation.
        /// </returns>
        public Anim add(string name, int[] frames)
        {
            return add(name, frames, 0, true);
        }

        /// <summary>
        /// Add an Animation.
        /// </summary>
        /// <param name="name">Name of the animation.</param>
        /// <param name="frames">Array of frame indices to animate through.</param>
        /// <param name="frameRate">Animation speed.</param>
        /// <returns>
        /// A new Anim object for the animation.
        /// </returns>
        public Anim add(string name, int[] frames, float frameRate)
        {
            return add(name, frames, frameRate, true);
        }

        /// <summary>
        /// Add an Animation.
        /// </summary>
        /// <param name="name">Name of the animation.</param>
        /// <param name="frames">Array of frame indices to animate through.</param>
        /// <param name="frameRate">Animation speed.</param>
        /// <param name="loop">if set to <c>true</c> the animation should loop..</param>
        /// <returns>A new Anim object for the animation.</returns>
        public Anim add(string name, int[] frames, float frameRate, bool loop)
        {
            if (_anims.ContainsKey(name)) throw new Exception("Cannot have multiple animations with the same name");
            (_anims[name] = new Anim(name, frames, frameRate, loop))._parent = this;
            return _anims[name];
        }

        /// <summary>
        /// Plays an animation.
        /// </summary>
        /// <returns>Anim object representing the played animation.</returns>
        public Anim play()
        {
            return play("", false);
        }

        /// <summary>
        /// Plays an animation.
        /// </summary>
        /// <param name="name">Name of the animation to play.</param>
        /// <returns>Anim object representing the played animation.</returns>
        public Anim play(string name)
        {
            return play(name, true);
        }

        /// <summary>
        /// Plays an animation.
        /// </summary>
        /// <param name="name">Name of the animation to play.</param>
        /// <param name="reset">if set to <c>true</c> the animation should force-restart if it is already playing..</param>
        /// <returns>Anim object representing the played animation.</returns>
        public Anim play(string name, bool reset)
        {
            if (!reset && _anim != null && _anim._name == name) return _anim;
            _anim = _anims[name];
            if (_anim == null)
            {
                _frame = _index = 0;
                complete = true;
                updateBuffer();
                return null;
            }
            _index = 0;
            _timer = 0;
            _frame = _anim._frames[0];
            complete = false;
            updateBuffer();
            return _anim;
        }

        /// <summary>
        /// Gets the frame index based on the column and row of the source image.
        /// </summary>
        /// <returns>Frame index.</returns>
        public int getFrame()
        {
            return getFrame(0, 0);
        }

        /// <summary>
        /// Gets the frame index based on the column and row of the source image.
        /// </summary>
        /// <param name="column">Frame column.</param>
        /// <param name="row">Frame row.</param>
        /// <returns>Frame index.</returns>
        public int getFrame(int column)
        {
            return getFrame(column, 0);
        }

        /// <summary>
        /// Gets the frame index based on the column and row of the source image.
        /// </summary>
        /// <param name="column">Frame column.</param>
        /// <param name="row">Frame row.</param>
        /// <returns>Frame index.</returns>
        public int getFrame(int column, int row)
        {
            return (row % _rows) * _columns + (column % _columns);
        }

        /// <summary>
        /// Sets the current display frame based on the column and row of the source image.
        /// When you set the frame, any animations playing will be stopped to force the frame.
        /// </summary>
        /// <param name="column">Frame column.</param>
        /// <param name="row">Frame row.</param>
        public void setFrame()
        {
            setFrame(0, 0);
        }

        /// <summary>
        /// Sets the current display frame based on the column and row of the source image.
        /// When you set the frame, any animations playing will be stopped to force the frame.
        /// </summary>
        /// <param name="column">Frame column.</param>
        /// <param name="row">Frame row.</param>
        public void setFrame(int column)
        {
            setFrame(column, 0);
        }

        /// <summary>
        /// Sets the current display frame based on the column and row of the source image.
        /// When you set the frame, any animations playing will be stopped to force the frame.
        /// </summary>
        /// <param name="column">Frame column.</param>
        /// <param name="row">Frame row.</param>
        public void setFrame(int column, int row)
        {
            _anim = null;
            int frame = (row % _rows) * _columns + (column % _columns);
            if (_frame == frame) return;
            _frame = frame;
            updateBuffer();
        }

        /// <summary>
        /// Assigns the Spritemap to a random frame.
        /// </summary>
        public void randFrame()
        {
            frame = (int)FP.rand((uint)_frameCount);
        }

        /// <summary>
        /// Sets the frame to the frame index of an animation.
        /// </summary>
        /// <param name="name">Animation to draw the frame frame.</param>
        /// <param name="index">Index of the frame of the animation to set to.</param>
        public void setAnimFrame(string name, int index)
        {
            int[] frames = _anims[name]._frames;
            index %= frames.Length;
            if (index < 0) index += frames.Length;
            frame = frames[index];
        }

        /// <summary>
        /// Sets the current frame index. When you set this, any
        /// animations playing will be stopped to force the frame.
        /// </summary>
        /// <value>
        /// The frame.
        /// </value>
        public int frame
        {
            get { return _frame; }
            set
            {
                _anim = null;
                value %= _frameCount;
                if (value < 0) value = _frameCount + value;
                if (_frame == value) return;
                _frame = value;
                updateBuffer();
            }
        }

        /// <summary>
        /// Current index of the playing animation.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int index
        {
            get { return _anim != null ? _index : 0; }
            set
            {
                if (_anim == null) return;
                value %= _anim._frameCount;
                if (_index == value) return;
                _index = value;
                _frame = _anim._frames[_index];
                updateBuffer();
            }
        }

        /// <summary>
        /// The amount of frames in the Spritemap.
        /// </summary>
        public int frameCount { get { return _frameCount; } }

        /// <summary>
        /// Columns in the Spritemap.
        /// </summary>
        public int columns { get { return _columns; } }

        /// <summary>
        /// Rows in the Spritemap.
        /// </summary>
        public int rows { get { return _rows; } }

        /// <summary>
        /// The currently playing animation.
        /// </summary>
        public string currentAnim { get { return _anim != null ? _anim._name : ""; } }

        // Spritemap information.
        protected int _width;
        protected int _height;
        private int _columns;
        private int _rows;
        private int _frameCount;
        private readonly Dictionary<string, Anim> _anims = new Dictionary<string, Anim>();
        private Anim _anim;
        private int _index;
        protected int _frame;
        private float _timer = 0;
    }
}
