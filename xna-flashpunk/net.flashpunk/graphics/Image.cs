using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.flashpunk.graphics
{
    /// <summary>
    /// Performance-optimized non-animated image. Can be drawn to the screen with transformations.
    /// </summary>
    public class Image : Graphic
    {
        /// <summary>
        /// Rotation of the image, in degrees.
        /// </summary>
        public float angle = 0;

        /// <summary>
        /// Scale of the image, effects both x and y scale.
        /// </summary>
        public float scale = 1;

        /// <summary>
        /// X scale of the image.
        /// </summary>
        public float scaleX = 1;

        /// <summary>
        /// Y scale of the image.
        /// </summary>
        public float scaleY = 1;

        /// <summary>
        /// X origin of the image, determines transformation point.
        /// </summary>
        /// <value>
        /// The origin X.
        /// </value>
        public int originX
        {
            get { return (int)origin.X; }
            set { origin.X = value; }
        }

        /// <summary>
        /// Y origin of the image, determines transformation point.
        /// </summary>
        /// <value>
        /// The origin Y.
        /// </value>
        public int originY
        {
            get { return (int)origin.Y; }
            set { origin.Y = value; }
        }

        /// <summary>
        /// Optional blend mode to use when drawing this image.
        /// Use constants from the flash.display.BlendMode class.
        /// </summary>
        public BlendState blend;

        /// <summary>
        /// If the image should be drawn transformed with pixel smoothing.
        /// This will affect drawing performance, but look less pixelly.
        /// </summary>
        public bool smooth;

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
            : this((Texture2D)null, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="source">Source image.</param>
        public Image(Texture2D source)
            : this(source, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public Image(Embed source)
            : this(source.bitmapData, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="clipRect">The clip rect.</param>
        public Image(Embed source, Rectangle clipRect)
            : this(source.bitmapData, clipRect)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <param name="clipRect">Optional rectangle defining area of the source image to draw.</param>
        public Image(Texture2D source, Rectangle clipRect)
        {
            _source = source;
            if (_source == null) throw new Exception("Invalid source image.");
            _sourceRect = _source.Bounds;
            if (clipRect != Rectangle.Empty)
            {
                if (clipRect.Width == 0) clipRect.Width = _sourceRect.Width;
                if (clipRect.Height == 0) clipRect.Height = _sourceRect.Height;
                _sourceRect = clipRect;
            }
        }

        /// <summary>
        /// Renders the graphic to the screen buffer.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="point">The position to draw the graphic.</param>
        /// <param name="camera">The camera offset.</param>
        override public void render(SpriteBatch target, Vector2 point, Vector2 camera)
        {
            if (blend == null)
                target.Draw(source, point + position + origin - camera * scroll, clipRect, _tint, angle, origin, scale, spriteEffects, 0);
            else
            {
                try
                {
                    target.End();
                    target.Begin(SpriteSortMode.Deferred, blend);
                    target.Draw(source, point + position + origin + camera * scroll, clipRect, _tint, angle, origin, scale, spriteEffects, 0);
                    target.End();
                }
                finally
                {
                    target.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
                }
            }
        }

        /// <summary>
        /// Creates a new rectangle Image.
        /// </summary>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <returns>A new Image object.</returns>
        public static Image createRect(int width, int height)
        {
            return createRect(width, height, 0xFFFFFF);
        }

        /// <summary>
        /// Creates a new rectangle Image.
        /// </summary>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="color">Color of the rectangle.</param>
        /// <returns>A new Image object</returns>
        public static Image createRect(int width, int height, uint color)
        {
            Texture2D source = new Texture2D(FP.graphicsDeviceManager.GraphicsDevice, width, height);
            var data = new Color[width * height];
            var fill = new Color(color >> 16 & 0xFF, color >> 8 & 0xFF, color & 0xFF, 0xFF);
            for (int i = 0; i < data.Length; ++i)
                data[i] = fill;
            source.SetData<Color>(data);
            return new Image(source);
        }

        /// <summary>
        /// Creates a new circle Image.
        /// </summary>
        /// <param name="radius">Radius of the circle.</param>
        /// <returns>A new Circle object.</returns>
        public static Image createCircle(uint radius)
        {
            return createCircle(radius, 0xFFFFFF);
        }

        /// <summary>
        /// Creates a new circle Image.
        /// </summary>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="color">Color of the circle.</param>
        /// <returns>A new Circle object.</returns>
        public static Image createCircle(uint radius, uint color)
        {
            //FP.sprite.graphics.clear();
            //FP.sprite.graphics.beginFill(color);
            //FP.sprite.graphics.drawCircle(radius, radius, radius);
            Texture2D data = new Texture2D(FP.graphicsDeviceManager.GraphicsDevice, (int)(radius * 2), (int)(radius * 2));
            // TODO: Draw the circle
            return new Image(data);
        }

        /// <summary>
        /// Change the opacity of the Image, a value from 0 to 1.
        /// </summary>
        /// <value>
        /// The alpha.
        /// </value>
        public float alpha
        {
            get { return _alpha; }
            set { UpdateTint(_color, _alpha = value); }
        }

        /// <summary>
        /// The tinted color of the Image. Use 0xFFFFFF to draw the Image normally.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public int color
        {
            get { return _color; }
            set { UpdateTint(_color = value, _alpha); }
        }

        /// <summary>
        /// If you want to draw the Image horizontally flipped. This is
        /// faster than setting scaleX to -1 if your image isn't transformed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if flipped; otherwise, <c>false</c>.
        /// </value>
        public bool flipped
        {
            get { return spriteEffects == SpriteEffects.None; }
            set { spriteEffects = value ? SpriteEffects.FlipHorizontally : SpriteEffects.None; }
        }

        /// <summary>
        /// Centers the Image's originX/Y to its center.
        /// </summary>
        public void centerOrigin()
        {
            originX = clipRect.Width / 2;
            originY = clipRect.Height / 2;
        }

        /// <summary>
        /// Centers the Image's originX/Y to its center, and negates the offset by the same amount.
        /// </summary>
        public void centerOO()
        {
            x += originX;
            y += originY;
            centerOrigin();
            x -= originX;
            y -= originY;
        }

        /// <summary>
        /// Width of the image.
        /// </summary>
        public int width { get { return (int)clipRect.Width; } }

        /// <summary>
        /// Height of the image.
        /// </summary>
        public int height { get { return (int)clipRect.Height; } }

        /// <summary>
        /// The scaled width of the image.
        /// </summary>
        /// <value>
        /// The width of the scaled.
        /// </value>
        public uint scaledWidth { get { return (uint)(clipRect.Width * scaleX * scale); } }

        /// <summary>
        /// The scaled height of the image.
        /// </summary>
        /// <value>
        /// The height of the scaled.
        /// </value>
        public uint scaledHeight { get { return (uint)(clipRect.Height * scaleY * scale); } }

        /// <summary>
        /// Clipping rectangle for the image.
        /// </summary>
        public Rectangle clipRect { get { return _sourceRect; } }

        /// <summary>
        /// Source Texture2D image.
        /// </summary>
        protected Texture2D source { get { return _source; } }

        void UpdateTint(int color, float alpha)
        {
            _tint = new Color(color >> 16 & 0xFF, color >> 8 & 0xFF, color & 0xFF, (int)(alpha * 0xFF));
        }

        // Source and buffer information.
        protected Texture2D _source;
        protected Rectangle _sourceRect;
        protected Rectangle _bufferRect;

        // Color and alpha information.
        private float _alpha = 1;
        private int _color = 0xFFFFFF;
        protected Color _tint = Color.White;
        private Matrix _matrix = FP.matrix;

        // XNA properties
        private SpriteEffects spriteEffects;
        private Vector2 origin = Vector2.Zero;
    }
}
