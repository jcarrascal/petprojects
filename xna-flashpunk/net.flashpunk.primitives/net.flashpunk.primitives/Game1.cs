using System;
using flash.display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace net.flashpunk.primitives
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;
        DisplayOrientation currentOrientation = DisplayOrientation.Default;
        Graphics g;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SupportedOrientations = DisplayOrientation.Default | DisplayOrientation.Portrait | 
                DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
        }

        void SetupOrientationProjection()
        {
            var viewport = graphics.GraphicsDevice.Viewport;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                (0, viewport.Width,
                viewport.Height, 0,
                0, 1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            g = new Graphics(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (currentOrientation != Window.CurrentOrientation)
            {
                currentOrientation = Window.CurrentOrientation;
                SetupOrientationProjection();
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            basicEffect.CurrentTechnique.Passes[0].Apply();

            // TODO: Add your drawing code here
            base.Draw(gameTime);

            g.clear();
            g.lineStyle(1, 0xFF0000);
            g.drawCircle(45, 45, 40);

            g.clear();
            g.beginFill(0xFFFFFF);
            g.drawCircle(130, 45, 40);

            g.clear();
            g.lineStyle(1, 0xFF0000);
            g.beginFill(0xFFFFFF);
            g.drawCircle(215, 45, 40);

            g.clear();
            g.beginGradientFill(GradientType.LINEAR, new int[] { 0xFFFFFF, 0 }, new float[] { 1, 1 }, new byte[] { 0, 0xFF });
            g.drawCircle(300, 45, 40);

            g.clear();
            //g.beginBitmapFill(fillTexture);
            g.drawCircle(385, 45, 40);
        }
    }
}
