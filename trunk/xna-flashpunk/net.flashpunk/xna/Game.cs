using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace net.flashpunk.xna
{
    public class Game<E> : Microsoft.Xna.Framework.Game
        where E: Engine, new()
    {
        DisplayOrientation currentOrientation = DisplayOrientation.Default;
        BasicEffect basicEffect;

        public E Engine { get; protected set; }

        protected Game()
            : this("Content")
        {
        }

        protected Game(string contentRootDirectory)
        {
            FP.game = this;
            FP.graphicsDeviceManager = new GraphicsDeviceManager(this);
            FP.graphicsDeviceManager.SupportedOrientations = DisplayOrientation.Default | DisplayOrientation.Portrait |
                DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Content.RootDirectory = contentRootDirectory;
        }

        void SetupOrientationProjection()
        {
            var viewport = FP.graphicsDeviceManager.GraphicsDevice.Viewport;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                (0, viewport.Width,
                viewport.Height, 0,
                0, 1);
            FP.width = viewport.Width;
            FP.height = viewport.Height;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            basicEffect = new BasicEffect(FP.graphicsDeviceManager.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            FP.buffer = new SpriteBatch(GraphicsDevice);
            FP.content = Content;
            basicEffect.CurrentTechnique.Passes[0].Apply();
            Engine = new E();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentOrientation != Window.CurrentOrientation)
            {
                currentOrientation = Window.CurrentOrientation;
                SetupOrientationProjection();
            }

            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            basicEffect.CurrentTechnique.Passes[0].Apply();
            Engine.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(FP.screen._color);
            basicEffect.CurrentTechnique.Passes[0].Apply();
            FP.buffer.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            Engine.Draw(gameTime);
            FP.buffer.End();
        }
    }
}
