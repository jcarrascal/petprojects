namespace net.flashpunk.tweens.misc
{
	using net.flashpunk.Tween;
	
	/**
	 * Tweens a numeric public property of an object.
	 */
	public class VarTween : Tween
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public VarTween(Function complete = null, uint type = 0) 
		{
			base(0, type, complete);
		}
		
		/**
		 * Tweens a numeric public property.
		 * @param	object		The object containing the property.
		 * @param	property	The name of the property (eg. "x").
		 * @param	to			Value to tween to.
		 * @param	duration	Duration of the tween.
		 * @param	ease		Optional easer function.
		 */
		public void tween(object object, string property, float to, float duration, Function ease = null)
		{
			_object = object;
			_property = property;
			_ease = ease;
			if (!object.hasOwnProperty(property)) throw new Error("The object does not have the property\"" + property + "\", or it is not accessible.");
			a:* = _object[property] as float;
			if (a == null) throw new Error("The property \"" + property + "\" is not numeric.");
			_start = _object[property];
			_range = to - _start;
			_target = duration;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			_object[_property] = _start + _range * _t;
		}
		
		// Tween information.
		/** @private */ private object _object;
		/** @private */ private string _property;
		/** @private */ private float _start;
		/** @private */ private float _range;
	}
}