namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.display.BlendMode;
	using flash.display.SpreadMethod;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.FP;
	
	/**
	 * Performance-optimized animated Image. Can have multiple animations,
	 * which draw frames from the provided source image to the screen.
	 */
	public class Spritemap : Image
	{
		/**
		 * If the animation has stopped.
		 */
		public bool complete = true;
		
		/**
		 * Optional callback for animation end.
		 */
		public Function callback;
		
		/**
		 * Animation speed factor, alter this to speed up/slow down all animations.
		 */
		public float rate = 1;
		
		/**
		 * Constructor.
		 * @param	source			Source image.
		 * @param	frameWidth		Frame width.
		 * @param	frameHeight		Frame height.
		 * @param	callback		Optional callback for animation end.
		 */
		public Spritemap(source:*, uint frameWidth = 0, uint frameHeight = 0, Function callback = null) 
		{
			_rect = new Rectangle(0, 0, frameWidth, frameHeight);
			base(source, _rect);
			if (!frameWidth) _rect.width = this.source.width;
			if (!frameHeight) _rect.height = this.source.height;
			_width = this.source.width;
			_height = this.source.height;
			_columns = _width / _rect.width;
			_rows = _height / _rect.height;
			_frameCount = _columns * _rows;
			this.callback = callback;
			updateBuffer();
			active = true;
		}
		
		/**
		 * Updates the spritemap's buffer.
		 */
		override public void updateBuffer(bool clearBefore = false) 
		{
			// get position of the current frame
			_rect.x = _rect.width * _frame;
			_rect.y = uint(_rect.x / _width) * _rect.height;
			_rect.x %= _width;
			if (_flipped) _rect.x = (_width - _rect.width) - _rect.x;
			
			// update the buffer
			base.updateBuffer(clearBefore);
		}
		
		/** @private Updates the animation. */
		override public void update() 
		{
			if (_anim && !complete)
			{
				_timer += (FP.fixed ? _anim._frameRate : _anim._frameRate * FP.elapsed) * rate;
				if (_timer >= 1)
				{
					while (_timer >= 1)
					{
						_timer --;
						_index ++;
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
					if (_anim) _frame = uint(_anim._frames[_index]);
					updateBuffer();
				}
			}
		}
		
		/**
		 * Add an Animation.
		 * @param	name		Name of the animation.
		 * @param	frames		Array of frame indices to animate through.
		 * @param	frameRate	Animation speed.
		 * @param	loop		If the animation should loop.
		 * @return	A new Anim object for the animation.
		 */
		public Anim add(string name, Array frames, float frameRate = 0, bool loop = true)
		{
			if (_anims[name]) throw new Error("Cannot have multiple animations with the same name");
			(_anims[name] = new Anim(name, frames, frameRate, loop))._parent = this;
			return _anims[name];
		}
		
		/**
		 * Plays an animation.
		 * @param	name		Name of the animation to play.
		 * @param	reset		If the animation should force-restart if it is already playing.
		 * @return	Anim object representing the played animation.
		 */
		public Anim play(string name = "", bool reset = false)
		{
			if (!reset && _anim && _anim._name == name) return _anim;
			_anim = _anims[name];
			if (!_anim)
			{
				_frame = _index = 0;
				complete = true;
				updateBuffer();
				return null;
			}
			_index = 0;
			_timer = 0;
			_frame = uint(_anim._frames[0]);
			complete = false;
			updateBuffer();
			return _anim;
		}
		
		/**
		 * Gets the frame index based on the column and row of the source image.
		 * @param	column		Frame column.
		 * @param	row			Frame row.
		 * @return	Frame index.
		 */
		public uint getFrame(uint column = 0, uint row = 0)
		{
			return (row % _rows) * _columns + (column % _columns);
		}
		
		/**
		 * Sets the current display frame based on the column and row of the source image.
		 * When you set the frame, any animations playing will be stopped to force the frame.
		 * @param	column		Frame column.
		 * @param	row			Frame row.
		 */
		public void setFrame(uint column = 0, uint row = 0)
		{
			_anim = null;
			uint frame = (row % _rows) * _columns + (column % _columns);
			if (_frame == frame) return;
			_frame = frame;
			updateBuffer();
		}
		
		/**
		 * Assigns the Spritemap to a random frame.
		 */
		public void randFrame()
		{
			frame = FP.rand(_frameCount);
		}
		
		/**
		 * Sets the frame to the frame index of an animation.
		 * @param	name	Animation to draw the frame frame.
		 * @param	index	Index of the frame of the animation to set to.
		 */
		public void setAnimFrame(string name, int index)
		{
			Array frames = _anims[name]._frames;
			index %= frames.length;
			if (index < 0) index += frames.length;
			frame = frames[index];
		}
		
		/**
		 * Sets the current frame index. When you set this, any
		 * animations playing will be stopped to force the frame.
		 */
		public int frame { get { return _frame; }
		public void frame { set
		{
			_anim = null;
			value %= _frameCount;
			if (value < 0) value = _frameCount + value;
			if (_frame == value) return;
			_frame = value;
			updateBuffer();
		}
		
		/**
		 * Current index of the playing animation.
		 */
		public uint index { get { return _anim ? _index : 0; }
		public void index { set
		{
			if (!_anim) return;
			value %= _anim._frameCount;
			if (_index == value) return;
			_index = value;
			_frame = uint(_anim._frames[_index]);
			updateBuffer();
		}
		
		/**
		 * The amount of frames in the Spritemap.
		 */
		public uint frameCount { get { return _frameCount; }
		
		/**
		 * Columns in the Spritemap.
		 */
		public uint columns { get { return _columns; }
		
		/**
		 * Rows in the Spritemap.
		 */
		public uint rows { get { return _rows; }
		
		/**
		 * The currently playing animation.
		 */
		public string currentAnim { get { return _anim ? _anim._name : ""; }
		
		// Spritemap information.
		/** @private */ protected Rectangle _rect;
		/** @private */ protected uint _width;
		/** @private */ protected uint _height;
		/** @private */ private uint _columns;
		/** @private */ private uint _rows;
		/** @private */ private uint _frameCount;
		/** @private */ private object _anims = { };
		/** @private */ private Anim _anim;
		/** @private */ private uint _index;
		/** @private */ protected uint _frame;
		/** @private */ private float _timer = 0;
	}
}