using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Entities;
using ForgottenLight.Events;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;

namespace ForgottenLight {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1 : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Textures
        private Texture2D lightTexture;
        
        // Render Targets
        private RenderTarget2D mainTarget;
        private RenderTarget2D lightningTarget;
        private RenderTarget2D target;

        // Effects
        private Effect lightningEffect;

        // Light pos
        private Vector2 mousePos;

        private float t;
        
        private Scene level;

        private bool lightningEnabled = true;

        private bool fullScreenEnabled = false;

        private const int WIDTH = 800;
        private const int HEIGHT = 480;

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

            this.SetFullscreen(fullScreenEnabled);

            this.mainTarget = new RenderTarget2D(GraphicsDevice, WIDTH, HEIGHT);
            this.lightningTarget = new RenderTarget2D(GraphicsDevice, WIDTH, HEIGHT);

            this.target = new RenderTarget2D(GraphicsDevice, WIDTH, HEIGHT);

            base.Initialize();

            // Content will be loaded after base.Initialize() statement

            // register lightning key event
            RegisterKeyEvents();

            // initialize level
            // this.level = new Level_Test(Content);
            //this.LoadScene(new Level_Custom("level_1"));
            this.LoadScene(new MainMenuScene());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
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
            if((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) && !(this.level is MainMenuScene)) {
                this.LoadScene(new MainMenuScene());
            }

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            this.mousePos = new Vector2(mouseState.X, mouseState.Y);

            t += gameTime.ElapsedGameTime.Milliseconds * 0.005f;

            level.Update(gameTime, keyboardState, mouseState);

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

            //spriteBatch.Draw(lightTexture, new Rectangle((int)mousePos.X - 200 / 2, (int)mousePos.Y - 200 / 2, 200, 200), Color.White);
            //spriteBatch.Draw(lightTexture, new Rectangle(50 - 400 / 2, 50 - 400 / 2, 400, 400), Color.White);
            //spriteBatch.Draw(lightTexture, new Rectangle(400 - 200 / 2, 400 - 200 / 2, 200, 200), Color.White);
            //spriteBatch.Draw(lightTexture, new Rectangle(700 - 200 / 2, 300 - 200 / 2, 200, 200), Color.White);

            level.DrawLights(spriteBatch, gameTime);

            spriteBatch.End();

            // DRAW MAIN SCENE

            GraphicsDevice.SetRenderTarget(mainTarget);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            
            level.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            // DRAW TO SCREEN USING LIGHTNING SHADER
            
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(Color.Black);

            lightningEffect.Parameters["lightMask"].SetValue(lightningTarget); // set light mask to shader
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, effect: lightningEnabled ? lightningEffect : null);
            
            // lightningEffect.Parameters["time"].SetValue(t);
            //lightningEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);

            spriteBatch.End();

            // Draw UI
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            level.DrawUI(spriteBatch, gameTime);

            spriteBatch.End();

            spriteBatch.Begin();
            
            // Draw all gizmos for this frame
            Gizmos.Instance.Draw(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(target, new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void RegisterKeyEvents() {
            Input.Instance.RegisterOnKeyDownEvent(Keys.F2, this.OnLightningKeyPressed);
            Input.Instance.RegisterOnKeyDownEvent(Keys.F11, this.ToggleFullscreen);
        }

        private void ToggleFullscreen() {
            this.fullScreenEnabled = !fullScreenEnabled;
            SetFullscreen(this.fullScreenEnabled);
        }

        private void SetFullscreen(bool fullScreenEnabled) {
            graphics.IsFullScreen = fullScreenEnabled;
            if (fullScreenEnabled) {
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            } else {
                graphics.PreferredBackBufferWidth = WIDTH;
                graphics.PreferredBackBufferHeight = HEIGHT;
            }
            graphics.ApplyChanges();
        }

        private void OnLightningKeyPressed() {
            this.lightningEnabled = !this.lightningEnabled; // toggle
        }

        public void LoadScene(Scene scene) {
            this.level = scene;

            Input.ClearInstance();
            Gizmos.ClearInstance();
            RegisterKeyEvents();

            this.level.Initialize(Content, this);
        }
        
        /// <summary>
        /// Converts screen position to position in game. (Is needed for multiple screen resolutions in fullscreen mode, when using mouse position.)
        /// </summary>
        /// <param name="screenPosition">Position on screen</param>
        /// <returns>Position in game</returns>
        public Vector2 ScreenToGamePosition(Vector2 screenPosition) {
            return screenPosition * new Vector2(((float)WIDTH)/graphics.PreferredBackBufferWidth, ((float)HEIGHT)/graphics.PreferredBackBufferHeight);
        }

    }
}
