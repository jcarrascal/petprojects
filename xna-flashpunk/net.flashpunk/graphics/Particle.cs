namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	
	/**
	 * Used by the Emitter class to track an existing Particle.
	 */
	public class Particle 
	{
		/**
		 * Constructor.
		 */
		public Particle() 
		{
			
		}
		
		// Particle information.
		/** @private */ internal ParticleType _type;
		/** @private */ internal float _time;
		/** @private */ internal float _duration;
		
		// Motion information.
		/** @private */ internal float _x;
		/** @private */ internal float _y;
		/** @private */ internal float _moveX;
		/** @private */ internal float _moveY;
		
		// List information.
		/** @private */ internal Particle _prev;
		/** @private */ internal Particle _next;
	}
}