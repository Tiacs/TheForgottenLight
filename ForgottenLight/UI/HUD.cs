using ForgottenLight.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.UI {
    class HUD : UserInterface {
        
        private Player player;

        private Label interactLabel;

        public HUD(ContentManager content, Player player) : base(content) {
            this.player = player;

            this.Transform.LocalPosition = new Vector2(10, 10);

            Initialize();
        }

        private void Initialize() {
            this.interactLabel = new Label("", Vector2.Zero, MainFont) {
                Scale = Vector2.One * 5f
            };
            // this.interactLabel.Transform.LocalScale = Vector2.One * 2;
            this.interactLabel.Parent = this;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            if(player.Interactable != null) {
                interactLabel.Text = player.Interactable.Description;
            } else {
                interactLabel.Text = "";
            }
        }
    }
}
