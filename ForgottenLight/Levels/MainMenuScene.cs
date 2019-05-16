using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Animations;
using ForgottenLight.Entities;
using ForgottenLight.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Levels {
    class MainMenuScene : Scene {

        private AnimationPlayer animationPlayer;
        private Animation playerAnimation;

        public MainMenuScene() : base(800, 480) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

            // Draw Player dummy
            animationPlayer.Draw(spriteBatch, gameTime, new Vector2(Width - 200, Height + Height / 2), Vector2.One * 20);
        }

        public override void Initialize(ContentManager contentManager, Game1 game) {
            base.Initialize(contentManager, game);

            game.IsMouseVisible = true;

            this.Interface = new MainMenu(Width, Height, contentManager, this);

            Texture2D atlas = contentManager.Load<Texture2D>("sprites/player");

            this.playerAnimation = new Animation(atlas, 38, 22, new Vector2(132, 0), 12, 0.25f, true);
            this.animationPlayer = new AnimationPlayer(new Vector2(.5f, 1));

            this.animationPlayer.PlayAnimation(playerAnimation);

            this.Lights.Add(new Light(new Vector2(600, 250), contentManager, 10, this));

        }

        protected override void CreateWalls() {
            base.CreateWalls();
        }

        protected override void LoadContent(ContentManager contentManager) {
            
        }

        public override void NextScene() {
            this.LoadScene(new Level_Custom("level_1"));
        }
        
        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
        }
    }
}
