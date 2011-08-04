namespace net.flashpunk.tweens.misc 
{
	using net.flashpunk.Tween;
	
	/**
	 * Tweens a color's red, green, and blue properties
	 * independently. Can also tween an alpha value.
	 */
	public class ColorTween : Tween
	{
		/**
		 * The current color.
		 */
		public uint color;
		
		/**
		 * The current alpha.
		 */
		public float alpha = 1;
		
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public ColorTween(Function complete = null, uint type = 0) 
		{
			base(0, type, complete);
		}
		
		/**
		 * Tweens the color to a new color and an alpha to a new alpha.
		 * @param	duration		Duration of the tween.
		 * @param	fromColor		Start color.
		 * @param	toColor			End color.
		 * @param	fromAlpha		Start alpha
		 * @param	toAlpha			End alpha.
		 * @param	ease			Optional easer function.
		 */
		public void tween(float duration, uint fromColor, uint toColor, float fromAlpha = 1, float toAlpha = 1, Function ease = null)
		{
			fromColor &= 0xFFFFFF;
			toColor &= 0xFFFFFF;
			color = fromColor;
			_r = fromColor >> 16 & 0xFF;
			_g = fromColor >> 8 & 0xFF;
			_b = fromColor & 0xFF;
			_startR = _r / 255;
			_startG = _g / 255;
			_startB = _b / 255;
			_rangeR = ((toColor >> 16 & 0xFF) / 255) - _startR;
			_rangeG = ((toColor >> 8 & 0xFF) / 255) - _startG;
			_rangeB = ((toColor & 0xFF) / 255) - _startB;
			_startA = alpha = fromAlpha;
			_rangeA = toAlpha - alpha;
			_target = duration;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			alpha = _startA + _rangeA * _t;
			_r = uint((_startR + _rangeR * _t) * 255);
			_g = uint((_startG + _rangeG * _t) * 255);
			_b = uint((_startB + _rangeB * _t) * 255);
			color = _r << 16 | _g << 8 | _b;
		}
		
		/**
		 * Red value of the current color, from 0 to 255.
		 */
		public uint red { get { return _r; }
		
		/**
		 * Green value of the current color, from 0 to 255.
		 */
		public uint green { get { return _g; }
		
		/**
		 * Blue value of the current color, from 0 to 255.
		 */
		public uint blue { get { return _b; }
		
		// Color information.
		/** @private */ private uint _r;
		/** @private */ private uint _g;
		/** @private */ private uint _b;
		/** @private */ private float _startA;
		/** @private */ private float _startR;
		/** @private */ private float _startG;
		/** @private */ private float _startB;
		/** @private */ private float _rangeA;
		/** @private */ private float _rangeR;
		/** @private */ private float _rangeG;
		/** @private */ private float _rangeB;
	}
}