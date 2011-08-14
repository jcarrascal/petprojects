using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using net.flashpunk.graphics;

namespace net.flashpunk
{
    /**
     * Main game Entity class updated by World.
     */
    public class Entity : Tweener
    {
        /**
         * If the Entity should render.
         */
        public bool visible = true;

        /**
         * If the Entity should respond to collision checks.
         */
        public bool collidable = true;

        /**
         * X position of the Entity in the World.
         */
        public float x = 0;

        /**
         * Y position of the Entity in the World.
         */
        public float y = 0;

        /**
         * Width of the Entity's hitbox.
         */
        public int width;

        /**
         * Height of the Entity's hitbox.
         */
        public int height;

        /**
         * X origin of the Entity's hitbox.
         */
        public int originX;

        /**
         * Y origin of the Entity's hitbox.
         */
        public int originY;

        /**
         * The Texture2D target to draw the Entity to. Leave as null to render to the current screen buffer (default).
         */
        public SpriteBatch renderTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
            : this(0, 0, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="x">X position to place the Entity.</param>
        /// <param name="y">Y position to place the Entity.</param>
        public Entity(float x, float y)
            : this(x, y, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="x">X position to place the Entity.</param>
        /// <param name="y">Y position to place the Entity.</param>
        /// <param name="graphic">Graphic to assign to the Entity.</param>
        public Entity(float x, float y, Graphic graphic)
            : this(x, y, graphic, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="x">X position to place the Entity.</param>
        /// <param name="y">Y position to place the Entity.</param>
        /// <param name="graphic">Graphic to assign to the Entity.</param>
        /// <param name="mask">Mask to assign to the Entity.</param>
        public Entity(float x, float y, Graphic graphic, Mask mask)
        {
            this.x = x;
            this.y = y;
            if (graphic != null) this.graphic = graphic;
            if (mask != null) this.mask = mask;
            HITBOX.assignTo(this);
            _class = GetType();
        }

        /// <summary>
        /// Override this, called when the Entity is added to a World.
        /// </summary>
        public virtual void added()
        {

        }

        /// <summary>
        /// Override this, called when the Entity is removed from a World.
        /// </summary>
        public virtual void removed()
        {

        }

        /// <summary>
        /// Updates the Entity.
        /// </summary>
        override public void update()
        {

        }

        /// <summary>
        /// Renders the Entity. If you override this for special behaviour,
        /// remember to call base.render() to render the Entity's graphic.
        /// </summary>
        public virtual void render()
        {
            if (_graphic != null && _graphic.visible)
            {
                if (_graphic.relative)
                {
                    _point.X = x;
                    _point.Y = y;
                }
                else _point.X = _point.Y = 0;
                _camera.X = FP.camera.X;
                _camera.Y = FP.camera.Y;
                _graphic.render(renderTarget != null ? renderTarget : FP.buffer, _point, _camera);
            }
        }

        /// <summary>
        /// Checks for a collision against an Entity type.
        /// </summary>
        /// <param name="type">The Entity type to check for.</param>
        /// <param name="x">Virtual x position to place this Entity.</param>
        /// <param name="y">Virtual y position to place this Entity.</param>
        /// <returns>The first Entity collided with, or null if none were collided.</returns>
        public Entity collide(string type, float x, float y)
        {
            if (_world != null) return null;

            Entity e = _world._typeFirst[type];
            if (!collidable || e == null) return null;

            _x = this.x; _y = this.y;
            this.x = x; this.y = y;

            if (_mask == null)
            {
                while (e != null)
                {
                    if (x - originX + width > e.x - e.originX
                    && y - originY + height > e.y - e.originY
                    && x - originX < e.x - e.originX + e.width
                    && y - originY < e.y - e.originY + e.height
                    && e.collidable && e != this)
                    {
                        if (e._mask == null || e._mask.collide(HITBOX))
                        {
                            this.x = _x; this.y = _y;
                            return e;
                        }
                    }
                    e = e._typeNext;
                }
                this.x = _x; this.y = _y;
                return null;
            }

            while (e != null)
            {
                if (x - originX + width > e.x - e.originX
                && y - originY + height > e.y - e.originY
                && x - originX < e.x - e.originX + e.width
                && y - originY < e.y - e.originY + e.height
                && e.collidable && e != this)
                {
                    if (_mask.collide(e._mask != null ? e._mask : e.HITBOX))
                    {
                        this.x = _x; this.y = _y;
                        return e;
                    }
                }
                e = e._typeNext;
            }
            this.x = _x; this.y = _y;
            return null;
        }

        /**
         * Checks for collision against multiple Entity types.
         * @param	types		An Array or Vector of Entity types to check for.
         * @param	x			Virtual x position to place this Entity.
         * @param	y			Virtual y position to place this Entity.
         * @return	The first Entity collided with, or null if none were collided.
         */
        public Entity collideTypes(IEnumerable<string> types, float x, float y)
        {
            if (_world == null) return null;
            Entity e;
            foreach (string type in types)
            {
                if ((e = collide(type, x, y)) != null) return e;
            }
            return null;
        }

        /**
         * Checks if this Entity collides with a specific Entity.
         * @param	e		The Entity to collide against.
         * @param	x		Virtual x position to place this Entity.
         * @param	y		Virtual y position to place this Entity.
         * @return	The Entity if they overlap, or null if they don't.
         */
        public Entity collideWith(Entity e, float x, float y)
        {
            _x = this.x; _y = this.y;
            this.x = x; this.y = y;

            if (x - originX + width > e.x - e.originX
            && y - originY + height > e.y - e.originY
            && x - originX < e.x - e.originX + e.width
            && y - originY < e.y - e.originY + e.height
            && collidable && e.collidable)
            {
                if (_mask == null)
                {
                    if (e._mask == null || e._mask.collide(HITBOX))
                    {
                        this.x = _x; this.y = _y;
                        return e;
                    }
                    this.x = _x; this.y = _y;
                    return null;
                }
                if (_mask.collide(e._mask != null ? e._mask : e.HITBOX))
                {
                    this.x = _x; this.y = _y;
                    return e;
                }
            }
            this.x = _x; this.y = _y;
            return null;
        }

        /**
         * Checks if this Entity overlaps the specified rectangle.
         * @param	x			Virtual x position to place this Entity.
         * @param	y			Virtual y position to place this Entity.
         * @param	rX			X position of the rectangle.
         * @param	rY			Y position of the rectangle.
         * @param	rWidth		Width of the rectangle.
         * @param	rHeight		Height of the rectangle.
         * @return	If they overlap.
         */
        public bool collideRect(float x, float y, float rX, float rY, float rWidth, float rHeight)
        {
            if (x - originX + width >= rX && y - originY + height >= rY
            && x - originX <= rX + rWidth && y - originY <= rY + rHeight)
            {
                if (_mask == null) return true;
                _x = this.x; _y = this.y;
                this.x = x; this.y = y;
                FP.entity.x = rX;
                FP.entity.y = rY;
                FP.entity.width = (int)rWidth;
                FP.entity.height = (int)rHeight;
                if (_mask.collide(FP.entity.HITBOX))
                {
                    this.x = _x; this.y = _y;
                    return true;
                }
                this.x = _x; this.y = _y;
                return false;
            }
            return false;
        }

        /**
         * Checks if this Entity overlaps the specified position.
         * @param	x			Virtual x position to place this Entity.
         * @param	y			Virtual y position to place this Entity.
         * @param	pX			X position.
         * @param	pY			Y position.
         * @return	If the Entity intersects with the position.
         */
        public bool collidePoint(float x, float y, float pX, float pY)
        {
            if (pX >= x - originX && pY >= y - originY
            && pX < x - originX + width && pY < y - originY + height)
            {
                if (_mask == null) return true;
                _x = this.x; _y = this.y;
                this.x = x; this.y = y;
                FP.entity.x = pX;
                FP.entity.y = pY;
                FP.entity.width = 1;
                FP.entity.height = 1;
                if (_mask.collide(FP.entity.HITBOX))
                {
                    this.x = _x; this.y = _y;
                    return true;
                }
                this.x = _x; this.y = _y;
                return false;
            }
            return false;
        }

        /**
         * Populates an array with all collided Entities of a type.
         * @param	type		The Entity type to check for.
         * @param	x			Virtual x position to place this Entity.
         * @param	y			Virtual y position to place this Entity.
         * @param	array		The Array or Vector object to populate.
         * @return	The array, populated with all collided Entities.
         */
        public void collideInto(string type, float x, float y, List<Entity> array)
        {
            if (_world == null) return;

            Entity e = _world._typeFirst[type];
            if (!collidable || e == null) return;

            _x = this.x; _y = this.y;
            this.x = x; this.y = y;

            if (_mask == null)
            {
                while (e != null)
                {
                    if (x - originX + width > e.x - e.originX
                    && y - originY + height > e.y - e.originY
                    && x - originX < e.x - e.originX + e.width
                    && y - originY < e.y - e.originY + e.height
                    && e.collidable && e != this)
                    {
                        if (e._mask == null || e._mask.collide(HITBOX)) array.Add(e);
                    }
                    e = e._typeNext;
                }
                this.x = _x; this.y = _y;
                return;
            }

            while (e != null)
            {
                if (x - originX + width > e.x - e.originX
                && y - originY + height > e.y - e.originY
                && x - originX < e.x - e.originX + e.width
                && y - originY < e.y - e.originY + e.height
                && e.collidable && e != this)
                {
                    if (_mask.collide(e._mask != null ? e._mask : e.HITBOX)) array.Add(e);
                }
                e = e._typeNext;
            }
            this.x = _x; this.y = _y;
            return;
        }

        /**
         * Populates an array with all collided Entities of multiple types.
         * @param	types		An array of Entity types to check for.
         * @param	x			Virtual x position to place this Entity.
         * @param	y			Virtual y position to place this Entity.
         * @param	array		The Array or Vector object to populate.
         * @return	The array, populated with all collided Entities.
         */
        public void collideTypesInto(IEnumerable<string> types, float x, float y, List<Entity> array)
        {
            if (_world == null) return;
            foreach (string type in types) collideInto(type, x, y, array);
        }

        /**
         * If the Entity collides with the camera rectangle.
         */
        public bool onCamera
        {
            get
            {
                return collideRect(x, y, FP.camera.X, FP.camera.Y, FP.width, FP.height);
            }
        }

        /**
         * The World object this Entity has been added to.
         */
        public World world
        {
            get
            {
                return _world;
            }
        }

        /**
         * Half the Entity's width.
         */
        public float halfWidth { get { return width / 2; } }

        /**
         * Half the Entity's height.
         */
        public float halfHeight { get { return height / 2; } }

        /**
         * The center x position of the Entity's hitbox.
         */
        public float centerX { get { return x - originX + width / 2; } }

        /**
         * The center y position of the Entity's hitbox.
         */
        public float centerY { get { return y - originY + height / 2; } }

        /**
         * The leftmost position of the Entity's hitbox.
         */
        public float left { get { return x - originX; } }

        /**
         * The rightmost position of the Entity's hitbox.
         */
        public float right { get { return x - originX + width; } }

        /**
         * The topmost position of the Entity's hitbox.
         */
        public float top { get { return y - originY; } }

        /**
         * The bottommost position of the Entity's hitbox.
         */
        public float bottom { get { return y - originY + height; } }

        /**
         * The rendering layer of this Entity. Higher layers are rendered first.
         */
        public int layer
        {
            get { return _layer; }
            set
            {
                if (_layer == value) return;
                if (!_added)
                {
                    _layer = value;
                    return;
                }
                _world.removeRender(this);
                _layer = value;
                _world.addRender(this);
            }
        }

        /**
         * The collision type, used for collision checking.
         */
        public string type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                if (!_added)
                {
                    _type = value;
                    return;
                }
                if (!string.IsNullOrWhiteSpace(_type)) _world.removeType(this);
                _type = value;
                if (!string.IsNullOrWhiteSpace(value)) _world.addType(this);
            }
        }

        /**
         * An optional Mask component, used for specialized collision. If this is
         * not assigned, collision checks will use the Entity's hitbox by default.
         */
        public Mask mask
        {
            get { return _mask; }
            set
            {
                if (_mask == value) return;
                if (_mask != null) _mask.assignTo(null);
                _mask = value;
                if (value != null) _mask.assignTo(this);
            }
        }

        /**
         * Graphical component to render to the screen.
         */
        public Graphic graphic
        {
            get { return _graphic; }
            set
            {
                if (_graphic == value) return;
                _graphic = value;
                if (value != null && value._assign != null) value._assign();
            }
        }

        /**
         * Adds the graphic to the Entity via a Graphiclist.
         * @param	g		Graphic to add.
         */
        public Graphic addGraphic(Graphic g)
        {
            if (graphic is Graphiclist) (graphic as Graphiclist).add(g);
            else
            {
                Graphiclist list = new Graphiclist();
                if (graphic != null) list.add(graphic);
                list.add(g);
                graphic = list;
            }
            return g;
        }

        public void setHitbox(int width, int height)
        {
            setHitbox(width, height, originX, originY);
        }

        /**
         * Sets the Entity's hitbox properties.
         * @param	width		Width of the hitbox.
         * @param	height		Height of the hitbox.
         * @param	originX		X origin of the hitbox.
         * @param	originY		Y origin of the hitbox.
         */
        public void setHitbox(int width, int height, int originX, int originY)
        {
            this.width = width;
            this.height = height;
            this.originX = originX;
            this.originY = originY;
        }

        /**
         * Sets the Entity's hitbox to match that of the provided object.
         * @param	o		The object defining the hitbox (eg. an Image or Rectangle).
         */
        public void setHitboxTo(Rectangle r)
        {
            setHitbox(r.Width, r.Height, -r.X, -r.Y);
        }

        /**
         * Sets the Entity's hitbox to match that of the provided object.
         * @param	o		The object defining the hitbox (eg. an Image or Rectangle).
         */
        public void setHitboxTo(Texture2D t)
        {
            setHitboxTo(t.Bounds);
        }

        public void setOrigin()
        {
            setOrigin(0, 0);
        }

        /**
         * Sets the origin of the Entity.
         * @param	x		X origin.
         * @param	y		Y origin.
         */
        public void setOrigin(int x, int y)
        {
            originX = x;
            originY = y;
        }

        /**
         * Center's the Entity's origin (half width & height).
         */
        public void centerOrigin()
        {
            originX = width / 2;
            originY = height / 2;
        }

        public float distanceFrom(Entity e)
        {
            return distanceFrom(e, false);
        }

        /**
         * Calculates the distance from another Entity.
         * @param	e				The other Entity.
         * @param	useHitboxes		If hitboxes should be used to determine the distance. If not, the Entities' x/y positions are used.
         * @return	The distance.
         */
        public float distanceFrom(Entity e, bool useHitboxes)
        {
            if (!useHitboxes) return (float)Math.Sqrt((x - e.x) * (x - e.x) + (y - e.y) * (y - e.y));
            return FP.distanceRects(x - originX, y - originY, width, height, e.x - e.originX, e.y - e.originY, e.width, e.height);
        }

        public float distanceToPoint(float px, float py)
        {
            return distanceToPoint(px, py, false);
        }

        /**
         * Calculates the distance from this Entity to the point.
         * @param	px				X position.
         * @param	py				Y position.
         * @param	useHitboxes		If hitboxes should be used to determine the distance. If not, the Entities' x/y positions are used.
         * @return	The distance.
         */
        public float distanceToPoint(float px, float py, bool useHitbox)
        {
            if (!useHitbox) return (float)Math.Sqrt((x - px) * (x - px) + (y - py) * (y - py));
            return FP.distanceRectPoint(px, py, x - originX, y - originY, width, height);
        }

        /**
         * Calculates the distance from this Entity to the rectangle.
         * @param	rx			X position of the rectangle.
         * @param	ry			Y position of the rectangle.
         * @param	rwidth		Width of the rectangle.
         * @param	rheight		Height of the rectangle.
         * @return	The distance.
         */
        public float distanceToRect(float rx, float ry, float rwidth, float rheight)
        {
            return FP.distanceRects(rx, ry, rwidth, rheight, x - originX, y - originY, width, height);
        }

        public void moveBy(float x, float y)
        {
            moveBy(x, y, null, false);
        }

        public void moveBy(float x, float y, string solidType)
        {
            moveBy(x, y, solidType, false);
        }

        /**
         * Moves the Entity by the amount, retaining integer values for its x and y.
         * @param	x			Horizontal offset.
         * @param	y			Vertical offset.
         * @param	solidType	An optional collision type to stop flush against upon collision.
         * @param	sweep		If sweeping should be used (prevents fast-moving objects from going through solidType).
         */
        public void moveBy(float x, float y, string solidType, bool sweep)
        {
            _moveX += x;
            _moveY += y;
            x = (float)Math.Round(_moveX);
            y = (float)Math.Round(_moveY);
            _moveX -= x;
            _moveY -= y;
            if (!string.IsNullOrWhiteSpace(solidType))
            {
                int sign; Entity e;
                if (x != 0)
                {
                    if (collidable && (sweep || collide(solidType, this.x + x, this.y) != null))
                    {
                        sign = x > 0 ? 1 : -1;
                        while (x != 0)
                        {
                            if ((e = collide(solidType, this.x + sign, this.y)) != null)
                            {
                                moveCollideX(e);
                                break;
                            }
                            else
                            {
                                this.x += sign;
                                x -= sign;
                            }
                        }
                    }
                    else this.x += x;
                }
                if (y != 0)
                {
                    if (collidable && (sweep || collide(solidType, this.x, this.y + y) != null))
                    {
                        sign = y > 0 ? 1 : -1;
                        while (y != 0)
                        {
                            if ((e = collide(solidType, this.x, this.y + sign)) != null)
                            {
                                moveCollideY(e);
                                break;
                            }
                            else
                            {
                                this.y += sign;
                                y -= sign;
                            }
                        }
                    }
                    else this.y += y;
                }
            }
            else
            {
                this.x += x;
                this.y += y;
            }
        }

        public void moveTo(float x, float y)
        {
            moveTo(x, y, null, false);
        }

        public void moveTo(float x, float y, string solidType)
        {
            moveTo(x, y, solidType, false);
        }

        /**
         * Moves the Entity to the position, retaining integer values for its x and y.
         * @param	x			X position.
         * @param	y			Y position.
         * @param	solidType	An optional collision type to stop flush against upon collision.
         * @param	sweep		If sweeping should be used (prevents fast-moving objects from going through solidType).
         */
        public void moveTo(float x, float y, string solidType, bool sweep)
        {
            moveBy(x - this.x, y - this.y, solidType, sweep);
        }

        /**
         * Moves towards the target position, retaining integer values for its x and y.
         * @param	x			X target.
         * @param	y			Y target.
         * @param	amount		Amount to move.
         * @param	solidType	An optional collision type to stop flush against upon collision.
         * @param	sweep		If sweeping should be used (prevents fast-moving objects from going through solidType).
         */
        public void moveTowards(float x, float y, float amount, string solidType, bool sweep)
        {
            _point.X = x - this.x;
            _point.Y = y - this.y;
            if (_point != Vector2.Zero) _point.Normalize();
            _point *= amount;
            moveBy(_point.X, _point.Y, solidType, sweep);
        }

        /**
         * When you collide with an Entity on the x-axis with moveTo() or moveBy().
         * @param	e		The Entity you collided with.
         */
        public virtual void moveCollideX(Entity e)
        {

        }

        /**
         * When you collide with an Entity on the y-axis with moveTo() or moveBy().
         * @param	e		The Entity you collided with.
         */
        public virtual void moveCollideY(Entity e)
        {

        }

        public void clampHorizontal(float left, float right)
        {
            clampHorizontal(left, right, 0);
        }

        /**
         * Clamps the Entity's hitbox on the x-axis.
         * @param	left		Left bounds.
         * @param	right		Right bounds.
         * @param	padding		Optional padding on the clamp.
         */
        public void clampHorizontal(float left, float right, float padding)
        {
            if (x - originX < left + padding) x = left + originX + padding;
            if (x - originX + width > right - padding) x = right - width + originX - padding;
        }

        public void clampVertical(float top, float bottom)
        {
            clampVertical(top, bottom, 0);
        }

        /**
         * Clamps the Entity's hitbox on the y axis.
         * @param	top			Min bounds.
         * @param	bottom		Max bounds.
         * @param	padding		Optional padding on the clamp.
         */
        public void clampVertical(float top, float bottom, float padding)
        {
            if (y - originY < top + padding) y = top + originY + padding;
            if (y - originY + height > bottom - padding) y = bottom - height + originY - padding;
        }

        // Entity information.
        internal Type _class;
        internal World _world;
        internal bool _added;
        internal string _type = "";
        internal int _layer;
        internal Entity _updatePrev;
        internal Entity _updateNext;
        internal Entity _renderPrev;
        internal Entity _renderNext;
        internal Entity _typePrev;
        internal Entity _typeNext;
        internal Entity _recycleNext;

        // Collision information.
        private readonly Mask HITBOX = new Mask();
        private Mask _mask;
        private float _x;
        private float _y;
        private float _moveX = 0;
        private float _moveY = 0;

        // Rendering information.
        internal Graphic _graphic;
        private Vector2 _point = FP.point;
        private Vector2 _camera = FP.point2;
    }
}
