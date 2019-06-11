/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Events;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;

namespace ForgottenLight {
    /// <summary>
    /// This is the main type for the game.
    /// </summary>
    class Game1 : Game {

        public string Version => Strings.GAME_VERSION;
        public static bool Debugging = false;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        // Render Targets
        private RenderTarget2D mainTarget;
        private RenderTarget2D lightningTarget;
        private RenderTarget2D target;

        // Effects
        private Effect lightningEffect;
        
        private Scene level;

        private bool lightningEnabled = true;

        private bool fullScreenEnabled = true;

        private const int WINDOWED_WIDTH = 800;
        private const int WINDOWED_HEIGHT = 480;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = Strings.CONTENT_ROOT_DIRECTORY;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            this.LoadScene(new MainMenuScene());
            
            this.SetFullscreen(fullScreenEnabled);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            this.lightningEffect = this.Content.Load<Effect>(Strings.CONTENT_LIGHTNING_EFFECT);
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
            
            level.Update(gameTime, Keyboard.GetState(), Mouse.GetState());

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {

            // DRAW LIGHT MAP
            GraphicsDevice.SetRenderTarget(lightningTarget);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            
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

            lightningEffect.Parameters[Strings.SHADER_PARAM_LIGHTMASK].SetValue(lightningTarget); // set light mask to shader
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, effect: lightningEnabled ? lightningEffect : null);
            
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
            Input.Instance.RegisterOnKeyDownEvent(Keys.Escape, this.OnExitKeyPressed);
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
                graphics.PreferredBackBufferWidth = (int) WINDOWED_WIDTH;
                graphics.PreferredBackBufferHeight = (int) WINDOWED_HEIGHT;
            }
            graphics.ApplyChanges();
        }

        private void OnLightningKeyPressed() {
            if(!Debugging) {
                return;
            }
            this.lightningEnabled = !this.lightningEnabled; // toggle
        }

        private void OnExitKeyPressed() {
            if (!(this.level is MainMenuScene)) {
                this.LoadScene(new MainMenuScene());
            } else {
                Exit();
            }
        }

        public void LoadScene(Scene scene) {
            this.level = scene;

            Input.ClearInstance();
            Gizmos.ClearInstance();
            RegisterKeyEvents();

            this.mainTarget = new RenderTarget2D(GraphicsDevice, (int)level.Width, (int)level.Height);
            this.lightningTarget = new RenderTarget2D(GraphicsDevice, (int)level.Width, (int)level.Height);

            this.target = new RenderTarget2D(GraphicsDevice, (int)level.Width, (int)level.Height);

            this.level.Initialize(Content, this);
        }
        
        /// <summary>
        /// Converts screen position to position in game. (Is needed for multiple screen resolutions in fullscreen mode, when using mouse position.)
        /// </summary>
        /// <param name="screenPosition">Position on screen</param>
        /// <returns>Position in game</returns>
        public Vector2 ScreenToGamePosition(Vector2 screenPosition) {
            return screenPosition * new Vector2(((float)level.Width)/graphics.PreferredBackBufferWidth, ((float)level.Height)/graphics.PreferredBackBufferHeight);
        }

    }
}
