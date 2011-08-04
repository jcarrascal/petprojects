namespace net.flashpunk.tweens.motion 
{
	using flash.geom.Vector2;
	using net.flashpunk.utils.Ease;
	
	/**
	 * Determines motion along a cubic curve.
	 */
	public class CubicMotion : Motion
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public CubicMotion(Function complete = null, uint type = 0)
		{
			base(0, complete, type, null);
		}
		
		/**
		 * Starts moving along the curve.
		 * @param	fromX		X start.
		 * @param	fromY		Y start.
		 * @param	aX			First control x.
		 * @param	aY			First control y.
		 * @param	bX			Second control x.
		 * @param	bY			Second control y.
		 * @param	toX			X finish.
		 * @param	toY			Y finish.
		 * @param	duration	Duration of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotion(float fromX, float fromY, float aX, float aY, float bX, float bY, float toX, float toY, float duration, Function ease = null)
		{
			x = _fromX = fromX;
			y = _fromY = fromY;
			_aX = aX;
			_aY = aY;
			_bX = bX;
			_bY = bY;
			_toX = toX;
			_toY = toY;
			_target = duration;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			x = _t * _t * _t * (_toX + 3 * (_aX - _bX) - _fromX) + 3 * _t * _t * (_fromX - 2 * _aX + _bX) + 3 * _t * (_aX - _fromX) + _fromX;
			y = _t * _t * _t * (_toY + 3 * (_aY - _bY) - _fromY) + 3 * _t * _t * (_fromY - 2 * _aY + _bY) + 3 * _t * (_aY - _fromY) + _fromY;
		}
		
		// Curve information.
		/** @private */ private float _fromX = 0;
		/** @private */ private float _fromY = 0;
		/** @private */ private float _toX = 0;
		/** @private */ private float _toY = 0;
		/** @private */ private float _aX = 0;
		/** @private */ private float _aY = 0;
		/** @private */ private float _bX = 0;
		/** @private */ private float _bY = 0;
		/** @private */ private float _ttt;
		/** @private */ private float _tt;
	}
}