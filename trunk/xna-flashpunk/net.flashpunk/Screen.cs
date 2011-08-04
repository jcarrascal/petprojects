using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace net.flashpunk
{
    /**
     * Container for the main screen buffer. Can be used to transform the screen.
     */
    public class Screen
    {
        /**
         * Constructor.
         */
        public Screen()
        {
            // create screen buffers
            _width = FP.width;
            _height = FP.height;
            update();
        }

        /**
         * Swaps screen buffers.
         */
        public void swap()
        {
            _current = 1 - _current;
        }

        /**
         * Refreshes the screen.
         */
        public void refresh()
        {
            // refreshes the screen
        }

        /**
         * Redraws the screen.
         */
        public void redraw()
        {
            // refresh the buffers
        }

        /** @private Re-applies transformation matrix. */
        public void update()
        {
            //_matrix.b = _matrix.c = 0;
            //_matrix.a = _scaleX * _scale;
            //_matrix.d = _scaleY * _scale;
            //_matrix.tx = -_originX * _matrix.a;
            //_matrix.ty = -_originY * _matrix.d;
            //if (_angle != 0) _matrix.rotate(_angle);
            //_matrix.tx += _originX * _scaleX * _scale + _x;
            //_matrix.ty += _originY * _scaleX * _scale + _y;
            //_sprite.transform.matrix = _matrix;
        }

        /**
         * Refresh color of the screen.
         */
        public int color
        {
            get { return (int)_color.R << 16 | (int)_color.G << 8 | (int)_color.B; }
            set { _color = new Color(value >> 16 & 0xFF, value >> 8 & 0xFF, value & 0xFF, 0xFF); }
        }

        /**
         * X offset of the screen.
         */
        public int x
        {
            get { return _x; }
            set
            {
                if (_x == value) return;
                _x = value;
                update();
            }
        }

        /**
         * Y offset of the screen.
         */
        public int y
        {
            get { return _y; }
            set
            {
                if (_y == value) return;
                _y = value;
                update();
            }
        }

        /**
         * X origin of transformations.
         */
        public int originX
        {
            get { return _originX; }
            set
            {
                if (_originX == value) return;
                _originX = value;
                update();
            }
        }

        /**
         * Y origin of transformations.
         */
        public int originY
        {
            get { return _originY; }
            set
            {
                if (_originY == value) return;
                _originY = value;
                update();
            }
        }

        /**
         * X scale of the screen.
         */
        public float scaleX
        {
            get { return _scaleX; }
            set
            {
                if (_scaleX == value) return;
                _scaleX = value;
                update();
            }
        }

        /**
         * Y scale of the screen.
         */
        public float scaleY
        {
            get { return _scaleY; }
            set
            {
                if (_scaleY == value) return;
                _scaleY = value;
                update();
            }
        }

        /**
         * Scale factor of the screen. Final scale is scaleX * scale by scaleY * scale, so
         * you can use this factor to scale the screen both horizontally and vertically.
         */
        public float scale
        {
            get { return _scale; }
            set
            {
                if (_scale == value) return;
                _scale = value;
                update();
            }
        }

        /**
         * Rotation of the screen, in degrees.
         */
        public float angle
        {
            get { return _angle * FP.DEG; }
            set
            {
                if (_angle == value * FP.RAD) return;
                _angle = value * FP.RAD;
                update();
            }
        }

        /**
         * Whether screen smoothing should be used or not.
         */
        public bool smoothing { get; set; }

        /**
         * Width of the screen.
         */
        public int width { get { return _width; } }

        /**
         * Height of the screen.
         */
        public int height { get { return _height; } }

        /**
         * X position of the mouse on the screen.
         */
        public int mouseX { get { return (int)((FP.mouseX - _x) / (_scaleX * _scale)); } }

        /**
         * Y position of the mouse on the screen.
         */
        public int mouseY { get { return (int)((FP.mouseY - _y) / (_scaleY * _scale)); } }

        ///**
        // * Captures the current screen as an Image object.
        // * @return	A new Image object.
        // */
        //public Texture2D capture()
        //{
        //    return new Image(_bitmap[_current].bitmapData.clone());
        //}

        // Screen infromation.
        private int _current = 0;
        //private Matrix _matrix;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _originX;
        private int _originY;
        private float _scaleX = 1;
        private float _scaleY = 1;
        private float _scale = 1;
        private float _angle = 0;
        internal Color _color = new Color(0x20, 0x20, 0x20);
    }
}