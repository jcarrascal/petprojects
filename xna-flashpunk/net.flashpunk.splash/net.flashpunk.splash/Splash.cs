using System;
using flash.display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using net.flashpunk;
using net.flashpunk.graphics;
using net.flashpunk.tweens.misc;
using net.flashpunk.utils;

namespace net.flashpunk.splash
{
    /**
     * This object displays the FlashPunk splash screen.
     */
    public class Splash : Entity
    {
        /*
         * Embedded graphics.
         */
        class SPLASH_LINES : Embed { public SPLASH_LINES() : base("splash_background.jpg") { } }
        class SPLASH_COG : Embed { public SPLASH_COG() : base("splash_cog.png") { } }
        class SPLASH_LEFT : Embed { public SPLASH_LEFT() : base("splash_left.png") { } }
        class SPLASH_RIGHT : Embed { public SPLASH_RIGHT() : base("splash_right.png") { } }

        /*
         * Image objects.
         */
        public Graphiclist list;
        public Image lines;
        public Image cog = new Image(new SPLASH_COG());
        public Image leftText = new Image(new SPLASH_LEFT());
        public Image rightText = new Image(new SPLASH_RIGHT());
        public Image fade = Image.createRect(FP.width, FP.height, 0);

        /*
         * Tween information.
         */
        public NumTween tween;
        public NumTween fader;
        public int leftX;
        public int rightX;

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        public Splash()
            : this(0xFF3366, 0x202020, 0.5f, 2, 0.5f, 720)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public Splash(int color)
            : this(color, 0x202020, 0.5f, 2, 0.5f, 720)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        public Splash(int color, int bgColor)
            : this(color, bgColor, 0.5f, 2, 0.5f, 720)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        /// <param name="fadeTime">The fade time.</param>
        public Splash(int color, int bgColor, float fadeTime)
            : this(color, bgColor, fadeTime, 2, 0.5f, 720)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        /// <param name="fadeTime">The fade time.</param>
        /// <param name="spinTime">The spin time.</param>
        public Splash(int color, int bgColor, float fadeTime, float spinTime)
            : this(color, bgColor, fadeTime, spinTime, 0.5f, 720)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        /// <param name="fadeTime">The fade time.</param>
        /// <param name="spinTime">The spin time.</param>
        /// <param name="spinPause">The spin pause.</param>
        public Splash(int color, int bgColor, float fadeTime, float spinTime, float spinPause)
            : this(color, bgColor, fadeTime, spinTime, spinPause, 720)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Splash"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        /// <param name="fadeTime">The fade time.</param>
        /// <param name="spinTime">The spin time.</param>
        /// <param name="spinPause">The spin pause.</param>
        /// <param name="spins">The spins.</param>
        public Splash(int color, int bgColor, float fadeTime, float spinTime, float spinPause, float spins)
        {
            // Create the lines image.
            //Texture2D data = new Texture2D(FP.graphicsDeviceManager.GraphicsDevice, FP.width, FP.height); // TODO: Fill with 0x353535
            //Graphics g = FP.sprite.graphics;
            //g.clear();
            //g.beginGradientFill(GradientType.RADIAL, new int[] { 0, 0 }, new float[] { 1, 0 }, new byte[] { 0, 255 });
            //g.drawCircle(0, 0, 100);
            //FP.matrix = Matrix.CreateScale(FP.width / 200, FP.height / 200, 0);
            //FP.matrix.Translation = new Vector3(FP.width / 2, FP.height / 2, 0);
            //data.draw(FP.sprite, FP.matrix);
            //g.clear();
            //g.beginBitmapFill(new SPLASH_LINES().bitmapData);
            //g.drawRect(0, 0, FP.width, FP.height);
            //data.draw(FP.sprite);
            lines = new Image(new SPLASH_LINES());

            // Set the entity information.
            x = FP.width / 2;
            y = FP.height / 2;
            graphic = new Graphiclist(leftText, rightText, cog, lines, fade);

            // Set the screen information.
            FP.screen.color = bgColor;

            // Set the lines properties.
            lines.blend = BlendMode.SUBTRACT;
            lines.smooth = true;
            lines.centerOO();

            // Set the big cog properties.
            cog.visible = true;
            cog.color = color;
            cog.smooth = true;
            cog.originX = cog.width / 2;
            cog.originY = cog.height / 2;
            cog.x -= cog.originX;
            cog.y -= cog.originY;

            // Set the left text properties.
            leftText.color = color;
            leftText.smooth = true;
            leftText.originX = leftText.width;
            leftText.originY = leftText.height / 2;
            leftText.x -= leftText.originX + cog.width / 4 + 4;
            leftText.y -= leftText.originY;
            leftX = (int)leftText.x;

            // Set the right text properties.
            rightText.color = color;
            rightText.smooth = true;
            rightText.originY = rightText.height / 2;
            rightText.x += cog.width / 4;
            rightText.y -= rightText.originY;
            rightX = (int)rightText.x;

            // Set the fade cover properties.
            fade.x -= x;
            fade.y -= y;

            // Set the timing properties.
            _fadeTime = fadeTime;
            _spinTime = spinTime;
            _spinPause = spinPause;
            _spins = spins;

            // Add the tweens.
            tween = new NumTween(tweenEnd);
            fader = new NumTween(faderEnd);
            addTween(tween);
            addTween(fader);

            // Make invisible until you start it.
            visible = false;
        }

        /// <summary>
        /// Start the splash screen.
        /// </summary>
        public void start()
        {
            visible = true;
            fadeIn();
        }

        /// <summary>
        /// Start the splash screen.
        /// </summary>
        /// <param name="onCompleteFunction">The on complete function.</param>
        public void start(Action onCompleteFunction)
        {
            _onCompleteFunction = onCompleteFunction;
            start();
        }

        /// <summary>
        /// Start the splash screen.
        /// </summary>
        /// <param name="onCompleteWorld">The on complete world.</param>
        public void start(World onCompleteWorld)
        {
            _onCompleteWorld = onCompleteWorld;
            start();
        }

        /// <summary>
        /// Update the splash screen.
        /// </summary>
        override public void update()
        {
            // In case the phone has been rotated.
            x = FP.width / 2;
            y = FP.height / 2;

            // Text scaling/positioning.
            float t = 1 - tween.scale;
            leftText.x = leftX - t * FP.width / 2;
            rightText.x = rightX + t * FP.width / 2;
            leftText.scaleY = rightText.scaleY = tween.scale;
            leftText.alpha = rightText.alpha = Ease.cubeIn(tween.scale);

            // Cog rotation/positioning.
            cog.angle = tween.scale <= 1 ? tween.value : tween.value * 2;
            cog.scale = 2.5f - tween.scale * 2;
            cog.alpha = tween.scale;

            // Fade in/out alpha control.
            fade.alpha = fader.value;

            // Pause before fade out.
            if (_spinWait > 0)
            {
                _spinWait -= FP.isFixed ? 1 : FP.elapsed;
                if (_spinWait <= 0) fadeOut();
            }
        }

        /// <summary>
        /// When the fade tween completes.
        /// </summary>
        private void faderEnd()
        {
            if (fader.value == 0) tween.tween(_spins, 0, _spinTime, Ease.backOut);
            else splashEnd();
        }

        /// <summary>
        /// When the tween completes.
        /// </summary>
        private void tweenEnd()
        {
            if (_spinPause >= 0) _spinWait = _spinPause;
            else fadeOut();
        }

        /// <summary>
        /// When the splash screen has completed.
        /// </summary>
        private void splashEnd()
        {
            if (_onCompleteFunction == null && _onCompleteWorld == null) return;
            else if (_onCompleteFunction != null) _onCompleteFunction();
            else if (_onCompleteWorld != null) FP.world = _onCompleteWorld;
            else throw new Exception("The onComplete parameter must be a Function callback or World object.");
        }

        /// <summary>
        /// Fades the splash screen in.
        /// </summary>
        private void fadeIn()
        {
            fader.tween(1, 0, _fadeTime, Ease.cubeOut);
        }

        /// <summary>
        /// Fades the splash screen out.
        /// </summary>
        private void fadeOut()
        {
            fader.tween(0, 1, _fadeTime, Ease.cubeIn);
        }

        /**
         * Fade in/out time and logo spinning time.
         */
        private float _fadeTime;
        private float _spinTime;
        private float _spins;
        private float _spinPause;
        private float _spinWait = 0;
        private Action _onCompleteFunction;
        private World _onCompleteWorld;
    }
}
