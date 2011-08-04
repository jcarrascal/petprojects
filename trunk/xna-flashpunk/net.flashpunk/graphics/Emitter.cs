namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.geom.ColorTransform;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.FP;
	using net.flashpunk.Graphic;
	using net.flashpunk.utils.Input;
	using net.flashpunk.utils.Key;
	
	/**
	 * Particle emitter used for emitting and rendering particle sprites.
	 * Good rendering performance with large amounts of particles.
	 */
	public class Emitter : Graphic
	{
		/**
		 * Constructor. Sets the source image to use for newly added particle types.
		 * @param	source			Source image.
		 * @param	frameWidth		Frame width.
		 * @param	frameHeight		Frame height.
		 */
		public Emitter(source:*, uint frameWidth = 0, uint frameHeight = 0) 
		{
			setSource(source, frameWidth, frameHeight);
			active = true;
		}
		
		/**
		 * Changes the source image to use for newly added particle types.
		 * @param	source			Source image.
		 * @param	frameWidth		Frame width.
		 * @param	frameHeight		Frame height.
		 */
		public void setSource(source:*, uint frameWidth = 0, uint frameHeight = 0)
		{
			if (source is Class) _source = FP.getBitmap(source);
			else if (source is Texture2D) _source = source;
			if (!_source) throw new Error("Invalid source image.");
			_width = _source.width;
			_height = _source.height;
			_frameWidth = frameWidth ? frameWidth : _width;
			_frameHeight = frameHeight ? frameHeight : _height;
			_frameCount = uint(_width / _frameWidth) * uint(_height / _frameHeight);
		}
		
		override public void update() 
		{
			// quit if there are no particles
			if (!_particle) return;
			
			// particle info
			float e = FP.fixed ? 1 : FP.elapsed,
				Particle p = _particle,
				Particle n, float t;
			
			// loop through the particles
			while (p)
			{
				// update time scale
				p._time += e;
				t = p._time / p._duration;
				
				// remove on time-out
				if (p._time >= p._duration)
				{
					if (p._next) p._next._prev = p._prev;
					if (p._prev) p._prev._next = p._next;
					else _particle = p._next;
					n = p._next;
					p._next = _cache;
					p._prev = null;
					_cache = p;
					p = n;
					_particleCount --;
					continue;
				}
				
				// get next particle
				p = p._next;
			}
		}
		
		/** @private Renders the particles. */
		override public void render(Texture2D target, Vector2 point, Vector2 camera) 
		{
			// quit if there are no particles
			if (!_particle) return;
			
			// get rendering position
			_point.x = point.x + x - camera.x * scrollX;
			_point.y = point.y + y - camera.y * scrollY;
			
			// particle info
			float t, float td,
				Particle p = _particle,
				ParticleType type,
				Rectangle rect;
			
			// loop through the particles
			while (p)
			{
				// get time scale
				t = p._time / p._duration;
				
				// get particle type
				type = p._type;
				rect = type._frame;
				
				// get position
				td = (type._ease == null) ? t : type._ease(t);
				_p.x = _point.x + p._x + p._moveX * td;
				_p.y = _point.y + p._y + p._moveY * td;
				
				// get frame
				rect.x = rect.width * type._frames[uint(td * type._frameCount)];
				rect.y = uint(rect.x / type._width) * rect.height;
				rect.x %= type._width;
				
				// draw particle
				if (type._buffer)
				{
					// get alpha
					_tint.alphaMultiplier = type._alpha + type._alphaRange * ((type._alphaEase == null) ? t : type._alphaEase(t));
					
					// get color
					td = (type._colorEase == null) ? t : type._colorEase(t);
					_tint.redMultiplier = type._red + type._redRange * td;
					_tint.greenMultiplier = type._green + type._greenRange * td;
					_tint.blueMultiplier  = type._blue + type._blueRange * td;
					type._buffer.fillRect(type._bufferRect, 0);
					type._buffer.copyPixels(type._source, rect, FP.zero);
					type._buffer.colorTransform(type._bufferRect, _tint);
					
					// draw particle
					target.copyPixels(type._buffer, type._bufferRect, _p, null, null, true);
				}
				else target.copyPixels(type._source, rect, _p, null, null, true);
				
				// get next particle
				p = p._next;
			}
		}
		
		/**
		 * Creates a new Particle type for this Emitter.
		 * @param	name		Name of the particle type.
		 * @param	frames		Array of frame indices for the particles to animate.
		 * @return	A new ParticleType object.
		 */
		public ParticleType newType(string name, Array frames = null)
		{
			if (_types[name]) throw new Error("Cannot add multiple particle types of the same name");
			return (_types[name] = new ParticleType(name, frames, _source, _frameWidth, _frameHeight));
		}
		
		/**
		 * Defines the motion range for a particle type.
		 * @param	name			The particle type.
		 * @param	angle			Launch Direction.
		 * @param	distance		Distance to travel.
		 * @param	duration		Particle duration.
		 * @param	angleRange		Random amount to add to the particle's direction.
		 * @param	distanceRange	Random amount to add to the particle's distance.
		 * @param	durationRange	Random amount to add to the particle's duration.
		 * @param	ease			Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setMotion(string name, float angle, float distance, float duration, float angleRange = 0, float distanceRange = 0, float durationRange = 0, Function ease = null)
		{
			return (_types[name] as ParticleType).setMotion(angle, distance, duration, angleRange, distanceRange, durationRange, ease);
		}
		
		/**
		 * Sets the alpha range of the particle type.
		 * @param	name		The particle type.
		 * @param	start		The starting alpha.
		 * @param	finish		The finish alpha.
		 * @param	ease		Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setAlpha(string name, float start = 1, float finish = 0, Function ease = null)
		{
			return (_types[name] as ParticleType).setAlpha(start, finish, ease);
		}
		
		/**
		 * Sets the color range of the particle type.
		 * @param	name		The particle type.
		 * @param	start		The starting color.
		 * @param	finish		The finish color.
		 * @param	ease		Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setColor(string name, uint start = 0xFFFFFF, uint finish = 0, Function ease = null)
		{
			return (_types[name] as ParticleType).setColor(start, finish, ease);
		}
		
		/**
		 * Emits a particle.
		 * @param	name		Particle type to emit.
		 * @param	x			X point to emit from.
		 * @param	y			Y point to emit from.
		 * @return
		 */
		public Particle emit(string name, float x, float y)
		{
			if (!_types[name]) throw new Error("Particle type \"" + name + "\" does not exist.");
			Particle p, ParticleType type = _types[name];
			
			if (_cache)
			{
				p = _cache;
				_cache = p._next;
			}
			else p = new Particle;
			p._next = _particle;
			p._prev = null;
			if (p._next) p._next._prev = p;
			
			p._type = type;
			p._time = 0;
			p._duration = type._duration + type._durationRange * FP.random;
			float a = type._angle + type._angleRange * FP.random,
				float d = type._distance + type._distanceRange * FP.random;
			p._moveX = Math.cos(a) * d;
			p._moveY = Math.sin(a) * d;
			p._x = x;
			p._y = y;
			_particleCount ++;
			return (_particle = p);
		}
		
		/**
		 * Amount of currently existing particles.
		 */
		public uint particleCount { get { return _particleCount; }
		
		// Particle infromation.
		/** @private */ private object _types = { };
		/** @private */ private Particle _particle;
		/** @private */ private Particle _cache;
		/** @private */ private uint _particleCount;
		
		// Source information.
		/** @private */ private Texture2D _source;
		/** @private */ private uint _width;
		/** @private */ private uint _height;
		/** @private */ private uint _frameWidth;
		/** @private */ private uint _frameHeight;
		/** @private */ private uint _frameCount;
		
		// Drawing information.
		/** @private */ private Vector2 _p = new Vector2;
		/** @private */ private ColorTransform _tint = new ColorTransform;
		/** @private */ private const float SIN = Math.PI / 2;
	}
}