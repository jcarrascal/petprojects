using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.flashpunk.graphics
{
    /**
     * A Graphic that can contain multiple Graphics of one or various types.
     * Useful for drawing sprites with multiple different parts, etc.
     */
    public class Graphiclist : Graphic
    {
        /**
         * Constructor.
         * @param	...graphic		Graphic objects to add to the list.
         */
        public Graphiclist(params Graphic[] graphic)
        {
            foreach (Graphic g in graphic) add(g);
        }

        /** @private Updates the graphics in the list. */
        override public void update()
        {
            foreach (Graphic g in _graphics)
            {
                if (g.active) g.update();
            }
        }

        /** @private Renders the Graphics in the list. */
        override public void render(SpriteBatch target, Vector2 point, Vector2 camera)
        {
            point.X += x;
            point.Y += y;
            camera.X *= scrollX;
            camera.Y *= scrollY;
            foreach (Graphic g in _graphics)
            {
                if (g.visible)
                {
                    if (g.relative)
                    {
                        _point.X = point.X;
                        _point.Y = point.Y;
                    }
                    else _point.X = _point.Y = 0;
                    _camera.X = camera.X;
                    _camera.Y = camera.Y;
                    g.render(target, _point, _camera);
                }
            }
        }

        /**
         * Adds the Graphic to the list.
         * @param	graphic		The Graphic to add.
         * @return	The added Graphic.
         */
        public Graphic add(Graphic graphic)
        {
            _graphics.Add(graphic);
            if (!active) active = graphic.active;
            return graphic;
        }

        /**
         * Removes the Graphic from the list.
         * @param	graphic		The Graphic to remove.
         * @return	The removed Graphic.
         */
        public Graphic remove(Graphic graphic)
        {
            if (_graphics.Contains(graphic)) return graphic;
            _temp.Clear();
            foreach (Graphic g in _graphics)
            {
                if (g != graphic) _temp.Add(g);
            }
            List<Graphic> temp = _graphics;
            _graphics = _temp;
            _temp = temp;
            updateCheck();
            return graphic;
        }

        /**
         * Removes the Graphic from the position in the list.
         * @param	index		Index to remove.
         */
        public void removeAt(uint index)
        {
            if (_graphics.Count == 0) return;
            remove(_graphics[(int)index % _graphics.Count]);
            updateCheck();
        }

        /**
         * Removes all Graphics from the list.
         */
        public void removeAll()
        {
            _graphics.Clear(); _temp.Clear();
            active = false;
        }

        /**
         * All Graphics in this list.
         */
        public List<Graphic> children { get { return _graphics; } }

        /**
         * Amount of Graphics in this list.
         */
        public uint count { get { return (uint)_graphics.Count; } }

        /**
         * Check if the Graphiclist should update.
         */
        private void updateCheck()
        {
            active = false;
            foreach (Graphic g in _graphics)
            {
                if (g.active)
                {
                    active = true;
                    return;
                }
            }
        }

        // List information.
        private List<Graphic> _graphics = new List<Graphic>();
        private List<Graphic> _temp = new List<Graphic>();
        private Vector2 _camera = new Vector2();
    }
}
