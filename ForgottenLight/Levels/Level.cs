using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Levels {
    class Level : Scene {

        private Texture2D levelBackgroundTile;

        protected Light mouseLight;
        protected Light playerLight;

        public Level(ContentManager contentManager, float width, float height) : base(contentManager, width, height) {
        }

        public override void Initialize() {
            base.Initialize();
            
            this.mouseLight = new Light(0, 0, contentManager, 4, this);
            this.playerLight = new Light(0, 0, contentManager, 1, this);

            this.Lights.Add(mouseLight);
            this.Lights.Add(playerLight);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            mouseLight.Transform.Position = mouseState.Position.ToVector2();
            playerLight.Transform.Position = Player.Transform.Position - Vector2.UnitY * Player.Collider.Height; // TODO: Create ability to get height of entity
            playerLight.Transform.Scale = Player.Transform.Scale * 1.5f;
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
