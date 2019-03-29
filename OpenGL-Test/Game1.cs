using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Entities;
using OpenGL_Test.Primitives;
using System.Collections.Generic;

namespace OpenGL_Test {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Textures
        Texture2D friesTexture;
        Texture2D iceTexture;
        Texture2D fishTexture;
        Texture2D lightTexture;
        
        // Render Targets
        RenderTarget2D mainTarget;
        RenderTarget2D lightningTarget;

        // Effects
        Effect lightningEffect;

        // Light pos
        Vector2 mousePos;

        float t;

        // Game Objects
        Player player;

        private bool lightningEnabled = true;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            this.mainTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            this.lightningTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);

            base.Initialize();

            // Content will be loaded after base.Initialize() statement

            this.player = new Player(100, 100, Content);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.friesTexture = this.Content.Load<Texture2D>("fries_32");
            this.iceTexture = this.Content.Load<Texture2D>("icecream_32");
            this.fishTexture = this.Content.Load<Texture2D>("fish_32");
            this.lightTexture = this.Content.Load<Texture2D>("light");

            this.lightningEffect = this.Content.Load<Effect>("lightning");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            this.mousePos = new Vector2(mouseState.X, mouseState.Y);

            t += gameTime.ElapsedGameTime.Milliseconds * 0.005f;

            player.Update(gameTime, keyboardState, mouseState);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            // GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // DRAW LIGHT MAP
            GraphicsDevice.SetRenderTarget(lightningTarget);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            spriteBatch.Draw(lightTexture, new Rectangle((int)mousePos.X - 200 / 2, (int)mousePos.Y - 200 / 2, 200, 200), Color.White);
            spriteBatch.Draw(lightTexture, new Rectangle(50 - 400 / 2, 50 - 400 / 2, 400, 400), Color.White);
            spriteBatch.Draw(lightTexture, new Rectangle(400 - 200 / 2, 400 - 200 / 2, 200, 200), Color.White);
            spriteBatch.Draw(lightTexture, new Rectangle(700 - 200 / 2, 300 - 200 / 2, 200, 200), Color.White);

            spriteBatch.End();

            // DRAW MAIN SCENE

            GraphicsDevice.SetRenderTarget(mainTarget);
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            spriteBatch.Draw(this.fishTexture, new Vector2(100, 100), Color.White);
            spriteBatch.Draw(this.friesTexture, new Vector2(200, 300), Color.White);
            spriteBatch.Draw(this.iceTexture, new Vector2(400, 150), Color.White);

            player.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            // DRAW TO SCREEN USING LIGHTNING SHADER
            
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            lightningEffect.Parameters["lightMask"].SetValue(lightningTarget); // set light mask to shader
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, effect: lightningEnabled ? lightningEffect : null);

            // lightningEffect.Parameters["time"].SetValue(t);
            //lightningEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);

            spriteBatch.End();

            spriteBatch.Begin();

            // Draw all gizmos for this frame
            Gizmos.Instance.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
