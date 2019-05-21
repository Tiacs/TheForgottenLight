/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Entities;
using ForgottenLight.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Levels {
    class Level : Scene {

        public HUD Hud => (HUD) Interface;
        private Texture2D levelBackgroundTile;

        protected Light mouseLight;
        protected Light playerLight;
        
        public Level(float width, float height) : base(width, height) {
        }

        public override void Initialize(ContentManager contentManager, Game1 game) {
            base.Initialize(contentManager, game);

            game.IsMouseVisible = false;

            this.Interface = new HUD(Width, Height, this, contentManager);

            this.mouseLight = new Light(0, 0, contentManager, 2, this);
            this.playerLight = new Light(0, 0, contentManager, 1, this);

            this.Lights.Add(mouseLight);
            this.Lights.Add(playerLight);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            if (!Hud.DialogBox.IsDialogRunning) { // block mouse light position, if dialog is running
                mouseLight.Transform.Position = ScreenToGamePosition(mouseState.Position.ToVector2());
            }
            playerLight.Transform.Position = Player.Transform.Position - 3 * Vector2.UnitY * Player.Collider.Height/2; // TODO: Create ability to get height of entity
            playerLight.Transform.Scale = Player.Transform.Scale * 1.5f;

            this.IsPaused = Hud.DialogBox.IsDialogRunning;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            DrawBricks(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }

        protected override void CreateWalls() {
            base.CreateWalls();
        }

        protected override void LoadContent(ContentManager contentManager) {
            this.levelBackgroundTile = contentManager.Load<Texture2D>("sprites/bricks");
        }

        private void DrawBricks(SpriteBatch spriteBatch, GameTime gameTime) {
            for (int y = 0; y < Height; y += levelBackgroundTile.Height) {
                for (int x = 0; x < Width; x += levelBackgroundTile.Width) {
                    spriteBatch.Draw(levelBackgroundTile, new Rectangle(x, y, levelBackgroundTile.Width, levelBackgroundTile.Height), Color.White);
                }
            }
        }
    }
}
