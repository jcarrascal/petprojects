using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace net.flashpunk
{
    /// <summary>
    /// Updated by Engine, main game container that holds all currently active Entities.
    /// Useful for organization, eg. "Menu", "Level1", etc.
    /// </summary>
    public class World : Tweener
    {
        /// <summary>
        /// If the render() loop is performed.
        /// </summary>
        public bool visible = true;

        /// <summary>
        /// Vector2 used to determine drawing offset in the render loop.
        /// </summary>
        public Vector2 camera;

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World()
        {

        }

        /// <summary>
        /// Override this; called when World is switch to, and set to the currently active world.
        /// </summary>
        public void begin()
        {

        }

        /// <summary>
        /// Override this; called when World is changed, and the active world is no longer this.
        /// </summary>
        public void end()
        {

        }

        /// <summary>
        /// Performed by the game loop, updates all contained Entities.
        /// If you override this to give your World update code, remember
        /// to call base.update() or your Entities will not be updated.
        /// </summary>
        override public void update()
        {
            // update the entities
            Entity e = _updateFirst;
            while (e != null)
            {
                if (e.active)
                {
                    if (e._tween != null) e.updateTweens();
                    e.update();
                }
                if (e._graphic != null && e._graphic.active) e._graphic.update();
                e = e._updateNext;
            }
        }

        /// <summary>
        /// Performed by the game loop, renders all contained Entities.
        /// If you override this to give your World render code, remember
        /// to call base.render() or your Entities will not be rendered.
        /// </summary>
        public void render()
        {
            // render the entities in order of depth
            Entity e;
            int i = _layerList.Count;
            while (i-- > 0)
            {
                e = _renderLast[_layerList[i]];
                while (e != null)
                {
                    if (e.visible) e.render();
                    e = e._renderPrev;
                }
            }
        }

        /// <summary>
        /// X position of the mouse in the World.
        /// </summary>
        public int mouseX
        {
            get
            {
                return (int)(FP.screen.mouseX + FP.camera.X);
            }
        }

        /// <summary>
        /// Y position of the mouse in the world.
        /// </summary>
        public int mouseY
        {
            get
            {
                return (int)(FP.screen.mouseY + FP.camera.Y);
            }
        }

        /// <summary>
        /// Adds the Entity to the World at the end of the frame.
        /// </summary>
        /// <param name="e">Entity object you want to add.</param>
        /// <returns>The added Entity object</returns>
        public Entity add(Entity e)
        {
            if (e._world != null) return e;
            _add.Add(e);
            e._world = this;
            return e;
        }

        /// <summary>
        /// Removes the Entity from the World at the end of the frame.
        /// </summary>
        /// <param name="e">Entity object you want to remove.</param>
        /// <returns>The removed Entity object.</returns>
        public Entity remove(Entity e)
        {
            if (e._world != this) return e;
            _remove.Add(e);
            e._world = null;
            return e;
        }

        /// <summary>
        /// Removes all Entities from the World at the end of the frame.
        /// </summary>
        public void removeAll()
        {
            Entity e = _updateFirst;
            while (e != null)
            {
                _remove.Add(e);
                e._world = null;
                e = e._updateNext;
            }
        }

        /// <summary>
        /// Adds multiple Entities to the world.
        /// </summary>
        /// <param name="list">Several Entities (as arguments) or an Array/Vector of Entities.</param>
        public void addList(params Entity[] list)
        {
            foreach (Entity e in list) add(e);
        }

        /// <summary>
        /// Removes multiple Entities from the world.
        /// </summary>
        /// <param name="list">Several Entities (as arguments) or an Array/Vector of Entities.</param>
        public void removeList(params Entity[] list)
        {
            foreach (Entity e in list) remove(e);
        }

        /// <summary>
        /// Adds an Entity to the World with the Graphic object.
        /// </summary>
        /// <param name="graphic">Graphic to assign the Entity.</param>
        /// <returns>The Entity that was added.</returns>
        public Entity addGraphic(Graphic graphic)
        {
            return addGraphic(graphic, 0, 0, 0);
        }

        /// <summary>
        /// Adds an Entity to the World with the Graphic object.
        /// </summary>
        /// <param name="graphic">Graphic to assign the Entity.</param>
        /// <param name="layer">Layer of the Entity.</param>
        /// <returns>The Entity that was added.</returns>
        public Entity addGraphic(Graphic graphic, int layer)
        {
            return addGraphic(graphic, layer, 0, 0);
        }

        /// <summary>
        /// Adds an Entity to the World with the Graphic object.
        /// </summary>
        /// <param name="graphic">Graphic to assign the Entity.</param>
        /// <param name="layer">Layer of the Entity.</param>
        /// <param name="x">X position of the Entity.</param>
        /// <param name="y">Y position of the Entity.</param>
        /// <returns>The Entity that was added.</returns>
        public Entity addGraphic(Graphic graphic, int layer, int x, int y)
        {
            Entity e = new Entity(x, y, graphic);
            if (layer != 0) e.layer = layer;
            e.active = false;
            return add(e);
        }

        /// <summary>
        /// Adds an Entity to the World with the Mask object.
        /// </summary>
        /// <param name="mask">Mask to assign the Entity.</param>
        /// <param name="type">Collision type of the Entity.</param>
        /// <returns>The Entity that was added.</returns>
        public Entity addMask(Mask mask, string type)
        {
            return addMask(mask, type, 0, 0);
        }

        /// <summary>
        /// Adds an Entity to the World with the Mask object.
        /// </summary>
        /// <param name="mask">Mask to assign the Entity.</param>
        /// <param name="type">Collision type of the Entity.</param>
        /// <param name="x">X position of the Entity.</param>
        /// <param name="y">Y position of the Entity.</param>
        /// <returns>The Entity that was added.</returns>
        public Entity addMask(Mask mask, string type, int x, int y)
        {
            Entity e = new Entity(x, y, null, mask);
            if (!string.IsNullOrWhiteSpace(type)) e.type = type;
            e.active = e.visible = false;
            return add(e);
        }

        /// <summary>
        /// Returns a new Entity, or a stored recycled Entity if one exists.
        /// </summary>
        /// <param name="classType">The Class of the Entity you want to add.</param>
        /// <returns>The new Entity object.</returns>
        public Entity create(Type classType)
        {
            return create(classType, true);
        }

        /// <summary>
        /// Returns a new Entity, or a stored recycled Entity if one exists.
        /// </summary>
        /// <param name="classType">The Class of the Entity you want to add.</param>
        /// <param name="addToWorld">if set to <c>true</c> add it to the World immediately.</param>
        /// <returns>The new Entity object.</returns>
        public Entity create(Type classType, bool addToWorld)
        {
            Entity e = _recycled[classType];
            if (e != null)
            {
                _recycled[classType] = e._recycleNext;
                e._recycleNext = null;
            }
            else e = (Entity)Activator.CreateInstance(classType);
            if (addToWorld) return add(e);
            return e;
        }

        /// <summary>
        /// Removes the Entity from the World at the end of the frame and recycles it.
        /// The recycled Entity can then be fetched again by calling the create() function.
        /// </summary>
        /// <param name="e">The Entity to recycle.</param>
        /// <returns>The recycled Entity.</returns>
        public Entity recycle(Entity e)
        {
            if (e._world != this) return e;
            e._recycleNext = _recycled[e._class];
            _recycled[e._class] = e;
            return remove(e);
        }

        /// <summary>
        /// Clears stored reycled Entities of the Class type.
        /// </summary>
        /// <param name="classType">The Class type to clear.</param>
        public void clearRecycled(Type classType)
        {
            Entity e = _recycled[classType];
            Entity n;
            while (e != null)
            {
                n = e._recycleNext;
                e._recycleNext = null;
                e = n;
            }
            _recycled.Remove(classType);
        }

        /// <summary>
        /// Clears stored recycled Entities of all Class types.
        /// </summary>
        public void clearRecycledAll()
        {
            foreach (Type classType in _recycled.Keys) clearRecycled(classType);
        }

        /// <summary>
        /// Brings the Entity to the front of its contained layer.
        /// </summary>
        /// <param name="e">The Entity to shift.</param>
        /// <returns>If the Entity changed position.</returns>
        public bool bringToFront(Entity e)
        {
            if (e._world != this || e._renderPrev == null) return false;
            // pull from list
            e._renderPrev._renderNext = e._renderNext;
            if (e._renderNext != null) e._renderNext._renderPrev = e._renderPrev;
            else _renderLast[e._layer] = e._renderPrev;
            // place at the start
            e._renderNext = _renderFirst[e._layer];
            e._renderNext._renderPrev = e;
            _renderFirst[e._layer] = e;
            e._renderPrev = null;
            return true;
        }

        /// <summary>
        /// Sends the Entity to the back of its contained layer.
        /// </summary>
        /// <param name="e">The Entity to shift.</param>
        /// <returns>If the Entity changed position.</returns>
        public bool sendToBack(Entity e)
        {
            if (e._world != this || e._renderNext == null) return false;
            // pull from list
            e._renderNext._renderPrev = e._renderPrev;
            if (e._renderPrev != null) e._renderPrev._renderNext = e._renderNext;
            else _renderFirst[e._layer] = e._renderNext;
            // place at the end
            e._renderPrev = _renderLast[e._layer];
            e._renderPrev._renderNext = e;
            _renderLast[e._layer] = e;
            e._renderNext = null;
            return true;
        }

        /// <summary>
        /// Shifts the Entity one place towards the front of its contained layer.
        /// </summary>
        /// <param name="e">The Entity to shift.</param>
        /// <returns>If the Entity changed position.</returns>
        public bool bringForward(Entity e)
        {
            if (e._world != this || e._renderPrev == null) return false;
            // pull from list
            e._renderPrev._renderNext = e._renderNext;
            if (e._renderNext != null) e._renderNext._renderPrev = e._renderPrev;
            else _renderLast[e._layer] = e._renderPrev;
            // shift towards the front
            e._renderNext = e._renderPrev;
            e._renderPrev = e._renderPrev._renderPrev;
            e._renderNext._renderPrev = e;
            if (e._renderPrev != null) e._renderPrev._renderNext = e;
            else _renderFirst[e._layer] = e;
            return true;
        }

        /// <summary>
        /// Shifts the Entity one place towards the back of its contained layer.
        /// </summary>
        /// <param name="e">The Entity to shift.</param>
        /// <returns>If the Entity changed position.</returns>
        public bool sendBackward(Entity e)
        {
            if (e._world != this || e._renderNext == null) return false;
            // pull from list
            e._renderNext._renderPrev = e._renderPrev;
            if (e._renderPrev != null) e._renderPrev._renderNext = e._renderNext;
            else _renderFirst[e._layer] = e._renderNext;
            // shift towards the back
            e._renderPrev = e._renderNext;
            e._renderNext = e._renderNext._renderNext;
            e._renderPrev._renderNext = e;
            if (e._renderNext != null) e._renderNext._renderPrev = e;
            else _renderLast[e._layer] = e;
            return true;
        }

        /// <summary>
        /// If the Entity as at the front of its layer.
        /// </summary>
        /// <param name="e">The Entity to check.</param>
        /// <returns>
        ///   <c>true</c> if is at the front of is layer; otherwise, <c>false</c>.
        /// </returns>
        public bool isAtFront(Entity e)
        {
            return e._renderPrev == null;
        }

        /// <summary>
        /// If the Entity as at the back of its layer.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>
        ///   <c>true</c> if is at the back of it's layer; otherwise, <c>false</c>.
        /// </returns>
        public bool isAtBack(Entity e)
        {
            return e._renderNext == null;
        }

        /// <summary>
        /// Returns the first Entity that collides with the rectangular area.
        /// </summary>
        /// <param name="type">The Entity type to check for.</param>
        /// <param name="rX">X position of the rectangle.</param>
        /// <param name="rY">Y position of the rectangle.</param>
        /// <param name="rWidth">Width of the rectangle.</param>
        /// <param name="rHeight">Height of the rectangle.</param>
        /// <returns>The first Entity to collide, or null if none collide.</returns>
        public Entity collideRect(string type, float rX, float rY, float rWidth, float rHeight)
        {
            Entity e = _typeFirst[type];
            while (e != null)
            {
                if (e.collideRect(e.x, e.y, rX, rY, rWidth, rHeight)) return e;
                e = e._typeNext;
            }
            return null;
        }

        /// <summary>
        /// Returns the first Entity found that collides with the position.
        /// </summary>
        /// <param name="type">The Entity type to check for.</param>
        /// <param name="pX">X position.</param>
        /// <param name="pY">Y position.</param>
        /// <returns>The collided Entity, or null if none collide.</returns>
        public Entity collidePoint(string type, float pX, float pY)
        {
            Entity e = _typeFirst[type];
            while (e != null)
            {
                if (e.collidePoint(e.x, e.y, pX, pY)) return e;
                e = e._typeNext;
            }
            return null;
        }

        /// <summary>
        /// Returns the first Entity found that collides with the line.
        /// </summary>
        /// <param name="type">The Entity type to check for.</param>
        /// <param name="fromX">Start x of the line.</param>
        /// <param name="fromY">Start y of the line.</param>
        /// <param name="toX">End x of the line.</param>
        /// <param name="toY">End y of the line.</param>
        /// <returns>Returns the first Entity found that collides with the line.</returns>
        public Entity collideLine(string type, int fromX, int fromY, int toX, int toY)
        {
            return collideLine(type, fromX, fromY, toX, toY, 1, Vector2.Zero);
        }

        /// <summary>
        /// Returns the first Entity found that collides with the line.
        /// </summary>
        /// <param name="type">The Entity type to check for.</param>
        /// <param name="fromX">Start x of the line.</param>
        /// <param name="fromY">Start y of the line.</param>
        /// <param name="toX">End x of the line.</param>
        /// <param name="toY">End y of the line.</param>
        /// <param name="precision">The precision.</param>
        /// <returns>Returns the first Entity found that collides with the line.</returns>
        public Entity collideLine(string type, int fromX, int fromY, int toX, int toY, uint precision)
        {
            return collideLine(type, fromX, fromY, toX, toY, precision, Vector2.Zero);
        }

        /// <summary>
        /// Returns the first Entity found that collides with the line.
        /// </summary>
        /// <param name="type">The Entity type to check for.</param>
        /// <param name="fromX">Start x of the line.</param>
        /// <param name="fromY">Start y of the line.</param>
        /// <param name="toX">End x of the line.</param>
        /// <param name="toY">End y of the line.</param>
        /// <param name="precision">The precision.</param>
        /// <param name="p">The point.</param>
        /// <returns>Returns the first Entity found that collides with the line.</returns>
        public Entity collideLine(string type, int fromX, int fromY, int toX, int toY, uint precision, Vector2 p)
        {
            // If the distance is less than precision, do the short sweep.
            if (precision < 1) precision = 1;
            if (FP.distance(fromX, fromY, toX, toY) < precision)
            {
                if (p != null)
                {
                    if (fromX == toX && fromY == toY)
                    {
                        p.X = toX; p.Y = toY;
                        return collidePoint(type, toX, toY);
                    }
                    return collideLine(type, fromX, fromY, toX, toY, 1, p);
                }
                else return collidePoint(type, fromX, toY);
            }

            // Get information about the line we're about to raycast.
            int xDelta = Math.Abs(toX - fromX);
            int yDelta = Math.Abs(toY - fromY);
            float xSign = toX > fromX ? precision : -precision;
            float ySign = toY > fromY ? precision : -precision;
            float x = fromX; float y = fromY; Entity e;

            // Do a raycast from the start to the end point.
            if (xDelta > yDelta)
            {
                ySign *= yDelta / xDelta;
                if (xSign > 0)
                {
                    while (x < toX)
                    {
                        if ((e = collidePoint(type, x, y)) != null)
                        {
                            if (p == null) return e;
                            if (precision < 2)
                            {
                                p.X = x - xSign; p.Y = y - ySign;
                                return e;
                            }
                            return collideLine(type, (int)(x - xSign), (int)(y - ySign), toX, toY, 1, p);
                        }
                        x += xSign; y += ySign;
                    }
                }
                else
                {
                    while (x > toX)
                    {
                        if ((e = collidePoint(type, x, y)) != null)
                        {
                            if (p == null) return e;
                            if (precision < 2)
                            {
                                p.X = x - xSign; p.Y = y - ySign;
                                return e;
                            }
                            return collideLine(type, (int)(x - xSign), (int)(y - ySign), toX, toY, 1, p);
                        }
                        x += xSign; y += ySign;
                    }
                }
            }
            else
            {
                xSign *= xDelta / yDelta;
                if (ySign > 0)
                {
                    while (y < toY)
                    {
                        if ((e = collidePoint(type, x, y)) != null)
                        {
                            if (p == null) return e;
                            if (precision < 2)
                            {
                                p.X = x - xSign; p.Y = y - ySign;
                                return e;
                            }
                            return collideLine(type, (int)(x - xSign), (int)(y - ySign), toX, toY, 1, p);
                        }
                        x += xSign; y += ySign;
                    }
                }
                else
                {
                    while (y > toY)
                    {
                        if ((e = collidePoint(type, x, y)) != null)
                        {
                            if (p == null) return e;
                            if (precision < 2)
                            {
                                p.X = x - xSign; p.Y = y - ySign;
                                return e;
                            }
                            return collideLine(type, (int)(x - xSign), (int)(y - ySign), toX, toY, 1, p);
                        }
                        x += xSign; y += ySign;
                    }
                }
            }

            // Check the last position.
            if (precision > 1)
            {
                if (p == null) return collidePoint(type, toX, toY);
                if (collidePoint(type, toX, toY) != null) return collideLine(type, (int)(x - xSign), (int)(y - ySign), toX, toY, 1, p);
            }

            // No collision, return the end point.
            if (p != null)
            {
                p.X = toX;
                p.Y = toY;
            }
            return null;
        }

        /**
         * Populates an array with all Entities that collide with the rectangle. This
         * does not empty the array, that responsibility is left to the user.
         * @param	type		The Entity type to check for.
         * @param	rX			X position of the rectangle.
         * @param	rY			Y position of the rectangle.
         * @param	rWidth		Width of the rectangle.
         * @param	rHeight		Height of the rectangle.
         * @param	into		The Array or Vector to populate with collided Entities.
         */
        public void collideRectInto(string type, float rX, float rY, float rWidth, float rHeight, List<Entity> into)
        {
            Entity e = _typeFirst[type];
            while (e != null)
            {
                if (e.collideRect(e.x, e.y, rX, rY, rWidth, rHeight)) into.Add(e);
                e = e._typeNext;
            }
        }

        /**
         * Populates an array with all Entities that collide with the position. This
         * does not empty the array, that responsibility is left to the user.
         * @param	type		The Entity type to check for.
         * @param	pX			X position.
         * @param	pY			Y position.
         * @param	into		The Array or Vector to populate with collided Entities.
         * @return	The provided Array.
         */
        public void collidePointInto(string type, float pX, float pY, List<Entity> into)
        {
            Entity e = _typeFirst[type];
            while (e != null)
            {
                if (e.collidePoint(e.x, e.y, pX, pY)) into.Add(e);
                e = e._typeNext;
            }
        }

        /**
         * Finds the Entity nearest to the rectangle.
         * @param	type		The Entity type to check for.
         * @param	x			X position of the rectangle.
         * @param	y			Y position of the rectangle.
         * @param	width		Width of the rectangle.
         * @param	height		Height of the rectangle.
         * @return	The nearest Entity to the rectangle.
         */
        public Entity nearestToRect(string type, float x, float y, float width, float height)
        {
            Entity n = _typeFirst[type];
            float nearDist = float.MaxValue;
            Entity near = null; float dist;
            while (n != null)
            {
                dist = squareRects(x, y, width, height, n.x - n.originX, n.y - n.originY, n.width, n.height);
                if (dist < nearDist)
                {
                    nearDist = dist;
                    near = n;
                }
                n = n._typeNext;
            }
            return near;
        }

        public Entity nearestToEntity(string type, Entity e)
        {
            return nearestToEntity(type, e, false);
        }

        /**
         * Finds the Entity nearest to another.
         * @param	type		The Entity type to check for.
         * @param	e			The Entity to find the nearest to.
         * @param	useHitboxes	If the Entities' hitboxes should be used to determine the distance. If false, their x/y coordinates are used.
         * @return	The nearest Entity to e.
         */
        public Entity nearestToEntity(string type, Entity e, bool useHitboxes)
        {
            if (useHitboxes) return nearestToRect(type, e.x - e.originX, e.y - e.originY, e.width, e.height);
            Entity n = _typeFirst[type];
            float nearDist = float.MaxValue;
            Entity near = null; float dist;
            float x = e.x - e.originX;
            float y = e.y - e.originY;
            while (n != null)
            {
                dist = (x - n.x) * (x - n.x) + (y - n.y) * (y - n.y);
                if (dist < nearDist)
                {
                    nearDist = dist;
                    near = n;
                }
                n = n._typeNext;
            }
            return near;
        }

        public Entity nearestToPoint(string type, float x, float y)
        {
            return nearestToPoint(type, x, y, false);
        }

        /**
         * Finds the Entity nearest to the position.
         * @param	type		The Entity type to check for.
         * @param	x			X position.
         * @param	y			Y position.
         * @param	useHitboxes	If the Entities' hitboxes should be used to determine the distance. If false, their x/y coordinates are used.
         * @return	The nearest Entity to the position.
         */
        public Entity nearestToPoint(string type, float x, float y, bool useHitboxes)
        {
            Entity n = _typeFirst[type];
            float nearDist = float.MaxValue;
            Entity near = null; float dist;
            if (useHitboxes)
            {
                while (n != null)
                {
                    dist = squarePointRect(x, y, n.x - n.originX, n.y - n.originY, n.width, n.height);
                    if (dist < nearDist)
                    {
                        nearDist = dist;
                        near = n;
                    }
                    n = n._typeNext;
                }
                return near;
            }
            while (n != null)
            {
                dist = (x - n.x) * (x - n.x) + (y - n.y) * (y - n.y);
                if (dist < nearDist)
                {
                    nearDist = dist;
                    near = n;
                }
                n = n._typeNext;
            }
            return near;
        }

        /**
         * How many Entities are in the World.
         */
        public uint count { get { return _count; } }

        /**
         * Returns the amount of Entities of the type are in the World.
         * @param	type		The type (or Class type) to count.
         * @return	How many Entities of type exist in the World.
         */
        public uint typeCount(string type)
        {
            return _typeCount[type];
        }

        /**
         * Returns the amount of Entities of the Class are in the World.
         * @param	c		The Class type to count.
         * @return	How many Entities of Class exist in the World.
         */
        public uint classCount(Type c)
        {
            return _classCount[c];
        }

        /**
         * Returns the amount of Entities are on the layer in the World.
         * @param	layer		The layer to count Entities on.
         * @return	How many Entities are on the layer.
         */
        public uint layerCount(int layer)
        {
            return _layerCount[layer];
        }

        /**
         * The first Entity in the World.
         */
        public Entity first { get { return _updateFirst; } }

        /**
         * How many Entity layers the World has.
         */
        public uint layers { get { return (uint)_layerList.Count; } }

        /**
         * The first Entity of the type.
         * @param	type		The type to check.
         * @return	The Entity.
         */
        public Entity typeFirst(string type)
        {
            if (_updateFirst == null) return null;
            return _typeFirst[type] as Entity;
        }

        /**
         * The first Entity of the Class.
         * @param	c		The Class type to check.
         * @return	The Entity.
         */
        public Entity classFirst(Type c)
        {
            if (_updateFirst == null) return null;
            Entity e = _updateFirst;
            while (e != null)
            {
                if (e._class.IsAssignableFrom(c)) return e;
                e = e._updateNext;
            }
            return null;
        }

        /**
         * The first Entity on the Layer.
         * @param	layer		The layer to check.
         * @return	The Entity.
         */
        public Entity layerFirst(int layer)
        {
            if (_updateFirst == null) return null;
            return _renderFirst[layer] as Entity;
        }

        /**
         * The last Entity on the Layer.
         * @param	layer		The layer to check.
         * @return	The Entity.
         */
        public Entity layerLast(int layer)
        {
            if (_updateFirst == null) return null;
            return _renderLast[layer] as Entity;
        }

        /**
         * The Entity that will be rendered first by the World.
         */
        public Entity farthest
        {
            get
            {
                if (_updateFirst == null) return null;
                return _renderLast[_layerList[_layerList.Count - 1]] as Entity;
            }
        }

        /**
         * The Entity that will be rendered last by the world.
         */
        public Entity nearest
        {
            get
            {
                if (_updateFirst == null) return null;
                return _renderFirst[_layerList[0]] as Entity;
            }
        }

        /**
         * The layer that will be rendered first by the World.
         */
        public int layerFarthest
        {
            get
            {
                if (_updateFirst == null) return 0;
                return _layerList[_layerList.Count - 1];
            }
        }

        /**
         * The layer that will be rendered last by the World.
         */
        public int layerNearest
        {
            get
            {
                if (_updateFirst == null) return 0;
                return _layerList[0];
            }
        }

        /**
         * How many different types have been added to the World.
         */
        public uint uniqueTypes
        {
            get
            {
                uint i = 0;
                foreach (string type in _typeCount.Keys) i++;
                return i;
            }
        }

        /**
         * Pushes all Entities in the World of the type into the Array or Vector.
         * @param	type		The type to check.
         * @param	into		The Array or Vector to populate.
         * @return	The same array, populated.
         */
        public void getType(string type, List<Entity> into)
        {
            Entity e = _typeFirst[type];
            while (e != null)
            {
                into.Add(e);
                e = e._typeNext;
            }
        }

        /**
         * Pushes all Entities in the World of the Class into the Array or Vector.
         * @param	c			The Class type to check.
         * @param	into		The Array or Vector to populate.
         * @return	The same array, populated.
         */
        public void getClass(Type c, List<Entity> into)
        {
            Entity e = _updateFirst;
            while (e != null)
            {
                if (e._class.IsAssignableFrom(c)) into.Add(e);
                e = e._updateNext;
            }
        }

        /**
         * Pushes all Entities in the World on the layer into the Array or Vector.
         * @param	layer		The layer to check.
         * @param	into		The Array or Vector to populate.
         * @return	The same array, populated.
         */
        public void getLayer(int layer, List<Entity> into)
        {
            Entity e = _renderLast[layer];
            while (e != null)
            {
                into.Add(e);
                e = e._updatePrev;
            }
        }

        /**
         * Pushes all Entities in the World into the array.
         * @param	into		The Array or Vector to populate.
         * @return	The same array, populated.
         */
        public void getAll(List<Entity> into)
        {
            Entity e = _updateFirst;
            while (e != null)
            {
                into.Add(e);
                e = e._updateNext;
            }
        }

        /**
         * Updates the add/remove lists at the end of the frame.
         */
        public void updateLists()
        {
            // remove entities
            if (_remove.Count > 0)
            {
                foreach (Entity e in _remove)
                {
                    if (e._added != true && _add.Contains(e))
                    {
                        _add.Remove(e);
                        continue;
                    }
                    e._added = false;
                    e.removed();
                    removeUpdate(e);
                    removeRender(e);
                    if (!string.IsNullOrWhiteSpace(e._type)) removeType(e);
                    if (e.autoClear && e._tween != null) e.clearTweens();
                }
                _remove.Clear();
            }

            // add entities
            if (_add.Count > 0)
            {
                foreach (Entity e in _add)
                {
                    e._added = true;
                    addUpdate(e);
                    addRender(e);
                    if (!string.IsNullOrWhiteSpace(e._type)) addType(e);
                    e.added();
                }
                _add.Clear();
            }

            // sort the depth list
            if (_layerSort)
            {
                if (_layerList.Count > 1) _layerList.Sort();
                _layerSort = false;
            }
        }

        /** @private Adds Entity to the update list. */
        private void addUpdate(Entity e)
        {
            // add to update list
            if (_updateFirst != null)
            {
                _updateFirst._updatePrev = e;
                e._updateNext = _updateFirst;
            }
            else e._updateNext = null;
            e._updatePrev = null;
            _updateFirst = e;
            _count++;
            if (_classCount.ContainsKey(e._class)) _classCount[e._class] = 0;
            _classCount[e._class] = (_classCount.ContainsKey(e._class) ? _classCount[e._class] : 0) + 1;
        }

        /** @private Removes Entity from the update list. */
        private void removeUpdate(Entity e)
        {
            // remove from the update list
            if (_updateFirst == e) _updateFirst = e._updateNext;
            if (e._updateNext != null) e._updateNext._updatePrev = e._updatePrev;
            if (e._updatePrev != null) e._updatePrev._updateNext = e._updateNext;
            e._updateNext = e._updatePrev = null;

            _count--;
            _classCount[e._class]--;
        }

        /** @private Adds Entity to the render list. */
        internal void addRender(Entity e)
        {
            Entity f = _renderFirst.Get(e._layer);
            if (f != null)
            {
                // Append entity to existing layer.
                e._renderNext = f;
                f._renderPrev = e;
                _layerCount[e._layer]++;
            }
            else
            {
                // Create new layer with entity.
                _renderLast[e._layer] = e;
                _layerList.Add(e._layer);
                _layerSort = true;
                e._renderNext = null;
                _layerCount[e._layer] = 1;
            }
            _renderFirst[e._layer] = e;
            e._renderPrev = null;
        }

        /** @private Removes Entity from the render list. */
        internal void removeRender(Entity e)
        {
            if (e._renderNext != null) e._renderNext._renderPrev = e._renderPrev;
            else _renderLast[e._layer] = e._renderPrev;
            if (e._renderPrev != null) e._renderPrev._renderNext = e._renderNext;
            else
            {
                // Remove this entity from the layer.
                _renderFirst[e._layer] = e._renderNext;
                if (e._renderNext == null)
                {
                    // Remove the layer from the layer list if this was the last entity.
                    if (_layerList.Count > 1)
                    {
                        _layerList.Remove(e._layer);
                        _layerSort = true;
                    }
                }
            }
            _layerCount[e._layer]--;
            e._renderNext = e._renderPrev = null;
        }

        /** @private Adds Entity to the type list. */
        internal void addType(Entity e)
        {
            // add to type list
            if (_typeFirst.ContainsKey(e._type))
            {
                _typeFirst[e._type]._typePrev = e;
                e._typeNext = _typeFirst[e._type];
                _typeCount[e._type]++;
            }
            else
            {
                e._typeNext = null;
                _typeCount[e._type] = 1;
            }
            e._typePrev = null;
            _typeFirst[e._type] = e;
        }

        /** @private Removes Entity from the type list. */
        internal void removeType(Entity e)
        {
            // remove from the type list
            if (_typeFirst[e._type] == e) _typeFirst[e._type] = e._typeNext;
            if (e._typeNext != null) e._typeNext._typePrev = e._typePrev;
            if (e._typePrev != null) e._typePrev._typeNext = e._typeNext;
            e._typeNext = e._typePrev = null;
            _typeCount[e._type]--;
        }

        /** @private Calculates the squared distance between two rectangles. */
        private static float squareRects(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
        {
            if (x1 < x2 + w2 && x2 < x1 + w1)
            {
                if (y1 < y2 + h2 && y2 < y1 + h1) return 0;
                if (y1 > y2) return (y1 - (y2 + h2)) * (y1 - (y2 + h2));
                return (y2 - (y1 + h1)) * (y2 - (y1 + h1));
            }
            if (y1 < y2 + h2 && y2 < y1 + h1)
            {
                if (x1 > x2) return (x1 - (x2 + w2)) * (x1 - (x2 + w2));
                return (x2 - (x1 + w1)) * (x2 - (x1 + w1));
            }
            if (x1 > x2)
            {
                if (y1 > y2) return squarePoints(x1, y1, (x2 + w2), (y2 + h2));
                return squarePoints(x1, y1 + h1, x2 + w2, y2);
            }
            if (y1 > y2) return squarePoints(x1 + w1, y1, x2, y2 + h2);
            return squarePoints(x1 + w1, y1 + h1, x2, y2);
        }

        /** @private Calculates the squared distance between two points. */
        private static float squarePoints(float x1, float y1, float x2, float y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        /** @private Calculates the squared distance between a rectangle and a point. */
        private static float squarePointRect(float px, float py, float rx, float ry, float rw, float rh)
        {
            if (px >= rx && px <= rx + rw)
            {
                if (py >= ry && py <= ry + rh) return 0;
                if (py > ry) return (py - (ry + rh)) * (py - (ry + rh));
                return (ry - py) * (ry - py);
            }
            if (py >= ry && py <= ry + rh)
            {
                if (px > rx) return (px - (rx + rw)) * (px - (rx + rw));
                return (rx - px) * (rx - px);
            }
            if (px > rx)
            {
                if (py > ry) return squarePoints(px, py, rx + rw, ry + rh);
                return squarePoints(px, py, rx + rw, ry);
            }
            if (py > ry) return squarePoints(px, py, rx, ry + rh);
            return squarePoints(px, py, rx, ry);
        }

        // Adding and removal.
        private List<Entity> _add = new List<Entity>();
        private List<Entity> _remove = new List<Entity>();

        // Update information.
        private Entity _updateFirst;
        private uint _count;

        // Render information.
        private Dictionary<int, Entity> _renderFirst = new Dictionary<int, Entity>();
        private Dictionary<int, Entity> _renderLast = new Dictionary<int, Entity>();
        private List<int> _layerList = new List<int>();
        private Dictionary<int, uint> _layerCount = new Dictionary<int, uint>();
        private bool _layerSort;

        private Dictionary<Type, uint> _classCount = new Dictionary<Type, uint>();
        internal Dictionary<string, Entity> _typeFirst = new Dictionary<string, Entity>();
        private Dictionary<string, uint> _typeCount = new Dictionary<string, uint>();
        private Dictionary<Type, Entity> _recycled = new Dictionary<Type, Entity>();
    }
}
