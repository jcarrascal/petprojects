using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.flashpunk
{
    public delegate void AssignFunction();

    /// <summary>
    /// Base class for all graphical types that can be drawn by Entity.
    /// </summary>
    public class Graphic
    {
        /// <summary>
        /// If the graphic should update.
        /// </summary>
        public bool active = false;

        /// <summary>
        /// 
        /// </summary>
        public bool visible = true;

        protected Vector2 position;

        /// <summary>
        /// X offset.
        /// </summary>
        public float x
        {
            get { return position.X; }
            set { position.X = value; }
        }

        /// <summary>
        /// Y offset.
        /// </summary>
        public float y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        protected Vector2 scroll = Vector2.One;

        /// <summary>
        /// X scrollfactor, effects how much the camera offsets the drawn graphic.
        /// Can be used for parallax effect, eg. Set to 0 to follow the camera,
        /// 0.5 to move at half-speed of the camera, or 1 (default) to stay still.
        /// </summary>
        public float scrollX
        {
            get { return scroll.X; }
            set { scroll.X = value; }
        }

        /// <summary>
        /// Y scrollfactor, effects how much the camera offsets the drawn graphic.
        /// Can be used for parallax effect, eg. Set to 0 to follow the camera,
        /// 0.5 to move at half-speed of the camera, or 1 (default) to stay still.
        /// </summary>
        public float scrollY
        {
            get { return scroll.Y; }
            set { scroll.Y = value; }
        }

        /// <summary>
        /// If the graphic should render at its position relative to its parent Entity's position.
        /// </summary>
        public bool relative = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graphic"/> class.
        /// </summary>
        public Graphic()
        {

        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void update()
        {

        }

        /// <summary>
        /// Renders the graphic to the screen buffer.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="point">The position to draw the graphic.</param>
        /// <param name="camera">The camera offset.</param>
        public virtual void render(SpriteBatch target, Vector2 point, Vector2 camera)
        {

        }

        /// <summary>
        /// Callback for when the graphic is assigned to an Entity.
        /// </summary>
        /// <value>The assign.</value>
        protected AssignFunction assign
        {
            get { return _assign; }
            set { _assign = value; }
        }

        // Graphic information.
        internal AssignFunction _assign;
        internal bool _scroll = true;
        protected Vector2 _point;
    }
}