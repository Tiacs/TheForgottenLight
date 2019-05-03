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
        private DialogBox dialogBox;

        public HUD(float width, float height, Player player, ContentManager content) : base(width, height, content) {
            this.player = player;

            Initialize();
        }

        private void Initialize() {
            this.interactLabel = new Label(MainFont) {
                Text = "Hello world!",
                Position = Vector2.Zero,
                Color = Color.White,
                Parent = this
            };

            this.dialogBox = new DialogBox(MainFont) {
                Position = new Vector2(100, 100),
                Parent = this
            };
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
