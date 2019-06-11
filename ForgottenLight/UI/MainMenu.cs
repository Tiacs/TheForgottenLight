/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Levels;
using ForgottenLight.Primitives;

namespace ForgottenLight.UI {
    class MainMenu : UserInterface {

        private Image logo, wasdKeys, eKey;
        private ClickableLabel playButton, controlsButton, ExitButton;
        private Label copyrightLabel, mouseLabel, fhLabel;

        public MainMenu(float width, float height, ContentManager content, Scene scene) : base(width, height, content, scene) {
            Initialize(content);
        }

        private void Initialize(ContentManager content) {
            
            this.logo = new Image(Scene) {
                Texture = content.Load<Texture2D>(Strings.CONTENT_SPRITE_LOGO),
                Scale = Vector2.One,
                Position = new Vector2(10, 100),
                Parent = this
            };
            
            this.wasdKeys = new Image(Scene) {
                Texture = content.Load<Texture2D>(Strings.CONTENT_SPRITE_UI_ATLAS),
                Scale = Vector2.One * .5f,
                Position = new Vector2(350, 550),
                SpriteOrigin = new Vector2(800, 0),
                SpriteSize = new Vector2(785, 494),
                Pivot = new Vector2(.5f, 1),
                Visible = false,
                Parent = this
            };
            
            new Label(MainFont, Scene) {
                Position = new Vector2(wasdKeys.Width / 2 + 10, wasdKeys.Height + 25),
                Scale = Vector2.One * 1.5f,
                Text = Strings.UI_MENU_MOVEMENT,
                Color = CustomColor.White,
                Pivot = new Vector2(.5f, 0),
                Parent = wasdKeys
            };

            this.eKey = new Image(Scene) {
                Texture = content.Load<Texture2D>(Strings.CONTENT_SPRITE_UI_ATLAS),
                Scale = Vector2.One * .5f,
                Position = new Vector2(700, 550),
                SpriteOrigin = new Vector2(1585, 0),
                SpriteSize = new Vector2(257, 243),
                Pivot = new Vector2(.5f, 1),
                Visible = false,
                Parent = this
            };

            new Label(MainFont, Scene) {
                Position = new Vector2(eKey.Width / 2 + 10, eKey.Height + 5),
                Scale = Vector2.One * 1.5f,
                Text = Strings.UI_MENU_INTERACT,
                Color = CustomColor.White,
                Pivot = new Vector2(.5f, 0),
                Parent = eKey
            };

            this.mouseLabel = new Label(MainFont, Scene) {
                Position = new Vector2(475, 650),
                Scale = Vector2.One * 2f,
                Text = Strings.UI_MENU_MOUSE,
                Color = CustomColor.White,
                Pivot = new Vector2(.5f, 0),
                Visible = false,
                Parent = this
            };

            this.playButton = new ClickableLabel(MainFont, Scene) {
                Position = new Vector2(120, this.Height - 318),
                Text = Strings.UI_MENU_PLAY,
                Scale = Vector2.One * 2f,
                PrimaryColor = CustomColor.White,
                HoverColor = CustomColor.Blue,
                OnUse = OnPlayClicked,
                Parent = this
            };

            this.controlsButton= new ClickableLabel(MainFont, Scene) {
                Position = new Vector2(120, this.Height - 258),
                Text = Strings.UI_MENU_CONTROLS,
                Scale = Vector2.One * 2f,
                PrimaryColor = CustomColor.White,
                HoverColor = CustomColor.Blue,
                OnUse = OnControlsClicked,
                Parent = this
            };

            this.ExitButton = new ClickableLabel(MainFont, Scene) {
                Position = new Vector2(120, this.Height - 202),
                Text = Strings.UI_MENU_EXIT,
                Scale = Vector2.One * 2f,
                PrimaryColor = CustomColor.White,
                HoverColor = CustomColor.Blue,
                OnUse = OnExitClicked,
                Parent = this
            };
            
            this.fhLabel = new Label(MainFont, Scene) {
                Position = new Vector2(10, this.Height - 15),
                Scale = Vector2.One * 1.2f,
                Text = Strings.UI_MENU_MMT,
                Color = CustomColor.White,
                Parent = this
            };

            this.copyrightLabel = new Label(MainFont, Scene) {
                Position = new Vector2(10, this.Height - this.fhLabel.Height - 20),
                Scale = Vector2.One * 1.2f,
                Text = string.Format(Strings.UI_MENU_CREDITS, Scene.Version),
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
            wasdKeys.Visible = !wasdKeys.Visible;
            eKey.Visible = !eKey.Visible;
            mouseLabel.Visible = !mouseLabel.Visible;
        }

        private void OnExitClicked() {
            this.Scene.Exit();
        }
    }
}
