/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Entities;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;

namespace ForgottenLight.UI {
    class HUD : UserInterface {
        
        private Level level;

        private Label interactLabel;
        private DialogBox dialogBox;

        public DialogBox DialogBox => dialogBox;

        public HUD(float width, float height, Level level, ContentManager content) : base(width, height, content, level) {
            this.level = level;

            Initialize(content);
        }

        private void Initialize(ContentManager content) {
            this.interactLabel = new Label(MainFont, level) {
                Pivot = new Vector2(0, 1),
                Position = new Vector2(10, Height-10),
                Color = CustomColor.Beige,
                Scale = Vector2.One * 1.2f,
                Parent = this
            };

            this.dialogBox = new DialogBox(MainFont, content, Scene) {
                Position = new Vector2(Width / 2, Height - 50),
                Pivot = new Vector2(.5f, 1),
                Width = 400,
                Height = 100,
                Visible = false,
                Parent = this
            };
            
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) {
         
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            if(level.Player.Interactable != null) {
                interactLabel.Text = level.Player.Interactable.Description.ToUpper();
            } else {
                interactLabel.Text = "";
            }
        }
    }
}
