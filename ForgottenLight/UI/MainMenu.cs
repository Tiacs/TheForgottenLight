using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Animations;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.UI {
    class MainMenu : UserInterface {

        private Image logo;
        private ClickableLabel playButton, controlsButton, ExitButton;
        private Label copyrightLabel;

        public MainMenu(float width, float height, ContentManager content, Scene scene) : base(width, height, content, scene) {
            Initialize(content);
        }

        private void Initialize(ContentManager content) {
            
            this.logo = new Image(Scene) {
                Texture = content.Load<Texture2D>("logo"),
                Scale = Vector2.One,
                Position = new Vector2(10, 50),
                Parent = this
            };

            Console.WriteLine(this.logo.Texture.Bounds.Size.ToVector2() * logo.Scale);

            this.playButton = new ClickableLabel(MainFont, Scene) {
                Position = new Vector2(50, this.Height - 140),
                Text = "Play",
                Scale = Vector2.One * 1.2f,
                PrimaryColor = CustomColor.White,
                HoverColor = CustomColor.Blue,
                OnUse = OnPlayClicked,
                Parent = this
            };

            this.controlsButton= new ClickableLabel(MainFont, Scene) {
                Position = new Vector2(50, this.Height - 115),
                Text = "Controls",
                Scale = Vector2.One * 1.2f,
                PrimaryColor = CustomColor.White,
                HoverColor = CustomColor.Blue,
                OnUse = OnControlsClicked,
                Parent = this
            };

            this.ExitButton = new ClickableLabel(MainFont, Scene) {
                Position = new Vector2(50, this.Height - 90),
                Text = "Exit",
                Scale = Vector2.One * 1.2f,
                PrimaryColor = CustomColor.White,
                HoverColor = CustomColor.Blue,
                OnUse = OnExitClicked,
                Parent = this
            };

            this.copyrightLabel = new Label(MainFont, Scene) {
                Position = new Vector2(10, this.Height - 15),
                Scale = Vector2.One,
                Text = "\u00A9 Fabian Friedl 2019", // \u00A9 == (c)
                Color = CustomColor.White,
                Parent = this
            };
        }

        public override void OnDraw(SpriteBatch sprite, GameTime gameTime) {
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
        }

        private void OnPlayClicked() {
            this.Scene.NextScene();
        }

        private void OnControlsClicked() {

        }

        private void OnExitClicked() {
            this.Scene.Exit();
        }
    }
}
