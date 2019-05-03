using ForgottenLight.Entities;
using ForgottenLight.Primitives;
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

        public DialogBox DialogBox => dialogBox;

        public HUD(float width, float height, Player player, ContentManager content) : base(width, height, content) {
            this.player = player;

            Initialize(content);
        }

        private void Initialize(ContentManager content) {
            this.interactLabel = new Label(MainFont) {
                Text = "Hello world!",
                Pivot = new Vector2(0, 1),
                Position = new Vector2(10, Height-10),
                Color = CustomColor.Beige,
                Scale = Vector2.One * 1.2f,
                Parent = this
            };

            this.dialogBox = new DialogBox(MainFont, content) {
                Position = new Vector2(Width / 2, Height - 100),
                Pivot = new Vector2(.5f, 1),
                Width = 400,
                Height = 100,
                Visible = false,
                Parent = this
            };

            //this.dialogBox.Messages.Enqueue(new DialogMessage("Hello World!", true));
            //this.dialogBox.Messages.Enqueue(new DialogMessage("My name is mystic guy!"));
            //this.dialogBox.Messages.Enqueue(new DialogMessage("I love u!"));
            this.dialogBox.Enqueue("This text hopefully is longer then 300px! I hope it will get into the next line! Or maybe antoher one! That would be great! Did I mention that I love this char!", false);

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) {
         
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
