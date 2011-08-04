namespace net.flashpunk.tweens.misc
{
	using net.flashpunk.Tween;

	/**
	 * Tweens multiple numeric public properties of an object simultaneously.
	 */
	public class MultiVarTween : Tween
	{
		/**
		 * Constructor.
		 * @param	complete		Optional completion callback.
		 * @param	type			Tween type.
		 */
		public MultiVarTween(Function complete = null, uint type = 0)
		{
			base(0, type, complete);
		}
		
		/**
		 * Tweens multiple numeric public properties.
		 * @param	object		The object containing the properties.
		 * @param	values		An object containing key/value pairs of properties and target values.
		 * @param	duration	Duration of the tween.
		 * @param	ease		Optional easer function.
		 */
		public void tween(object object, object values, float duration, Function ease = null)
		{
			_object = object;
			_vars.length = 0;
			_start.length = 0;
			_range.length = 0;
			_target = duration;
			_ease = ease;
			for (string p in values)
			{
				if (!object.hasOwnProperty(p)) throw new Error("The object does not have the property\"" + p + "\", or it is not accessible.");
				a:* = _object[p] as float;
				if (a == null) throw new Error("The property \"" + p + "\" is not numeric.");
				_vars.push(p);
				_start.push(a);
				_range.push(values[p] - a);
			}
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update()
		{
			base.update();
			int i = _vars.length;
			while (i --) _object[_vars[i]] = _start[i] + _range[i] * _t;
		}

		// Tween information.
		/** @private */ private object _object;
		/** @private */ private Vector _vars.<string> = new Vector.<string>;
		/** @private */ private Vector _start.<float> = new Vector.<float>;
		/** @private */ private Vector _range.<float> = new Vector.<float>;
	}
}