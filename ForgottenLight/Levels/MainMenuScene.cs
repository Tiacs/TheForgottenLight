﻿/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Entities;
using ForgottenLight.UI;

namespace ForgottenLight.Levels {
    class MainMenuScene : Scene {

        private AnimationPlayer animationPlayer;
        private Animation playerAnimation;

        public MainMenuScene() : base(1920, 1080) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

            // Draw Player dummy
            animationPlayer.Draw(spriteBatch, gameTime, new Vector2(Width - 480, Height + Height / 2), Vector2.One * 40);
        }

        public override void Initialize(ContentManager contentManager, Game1 game) {
            base.Initialize(contentManager, game);

            game.IsMouseVisible = true;

            this.Interface = new MainMenu(Width, Height, contentManager, this);

            Texture2D atlas = contentManager.Load<Texture2D>(Strings.CONTENT_SPRITE_PLAYER);

            this.playerAnimation = new Animation(atlas, 38, 22, new Vector2(132, 0), 12, 0.25f, true);
            this.animationPlayer = new AnimationPlayer(new Vector2(.5f, 1));

            this.animationPlayer.PlayAnimation(playerAnimation);

            this.Lights.Add(new Light(new Vector2(1440, 562), contentManager, 20, this));

        }

        protected override void CreateWalls() {
            base.CreateWalls();
        }

        protected override void LoadContent(ContentManager contentManager) {
            
        }

        public override void NextScene() {
            this.LoadScene(new Level_Custom(Strings.CONTENT_LEVEL_FIRST));
        }
        
        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
        }
    }
}
