/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Primitives;
using ForgottenLight.Levels;
using System;

namespace ForgottenLight.UI {
    class HUD : UserInterface {

        private const float DEATH_FADE_SPEED = 10f;
        private const float WON_FADE_SPEED = 4f;

        private Level level;

        private bool isFadingBlack = false;
        private bool isFadingWhite = false;
        private float blackScreenOpacity;
        
        private Label interactLabel;
        private Image blackScreen;
        private Image keyIcon;

        public DialogBox DialogBox {
            get; private set;
        }

        public bool IsKeyFound {
            get; set;
        }
        
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
            
            this.keyIcon = new Image(content.Load<Texture2D>("ui/ui_atlas"), Scene) {
                SpriteOrigin = new Vector2(1842, 0),
                SpriteSize = new Vector2(28, 12),
                Position = new Vector2(Width - 10, 10),
                Pivot = new Vector2(1, 0),
                Visible = false,
                Parent = this
            };

            this.blackScreen = new Image(Scene) {
                Position = Vector2.Zero,
                Scale = new Vector2(Width, Height),
                Color = new Color(0, 0, 0, 0),
                Visible = true,
                Parent = this
            };

            this.DialogBox = new DialogBox(MainFont, content, Scene) {
                Position = new Vector2(Width / 2, Height - 50),
                Pivot = new Vector2(.5f, 1),
                Width = 400,
                Height = 100,
                Visible = false,
                Parent = this
            };
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) {
            if(blackScreen.Texture == null) {
                blackScreen.Texture = LoadBlankColor(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            if(level.Player.Interactable != null) {
                interactLabel.Text = level.Player.Interactable.Description.ToUpper();
            } else {
                interactLabel.Text = "";
            }

            if(isFadingBlack) { // Fading out black on death
                this.PerformBlackFading(gameTime);
            } else if(isFadingWhite) { // Fading out black on game won
                this.PerformWhiteFading(gameTime);
            }

            if(Scene.Player.Inventory.ContainsItem(Items.ItemCode.KEY)) {
                this.keyIcon.Visible = true;
            }
        }

        private void PerformBlackFading(GameTime gameTime) {
            this.blackScreenOpacity = MathHelper.Min((float)blackScreenOpacity + DEATH_FADE_SPEED
                * (float)gameTime.ElapsedGameTime.TotalSeconds, 255f);

            double smooth = Math.Min(Math.Exp(blackScreenOpacity), 255); // make linear to exp fading (looks better)

            this.blackScreen.Color = new Color(0, 0, 0, (int)smooth);
        }

        private void PerformWhiteFading(GameTime gameTime) {
            this.blackScreenOpacity = MathHelper.Min((float)blackScreenOpacity + WON_FADE_SPEED
                * (float)gameTime.ElapsedGameTime.TotalSeconds, 255f);
            
            double smooth = Math.Min(Math.Exp(blackScreenOpacity), 255) / 255.0;  // make linear to exp fading (looks better)

            Color whiteColor = CustomColor.White;
            this.blackScreen.Color = new Color((int)(whiteColor.R * smooth), (int)(whiteColor.G * smooth),
                (int)(whiteColor.B * smooth), (int)(whiteColor.A * smooth));
        }

        public Texture2D LoadBlankColor(SpriteBatch spriteBatch) {
            Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White }); // fill the texture with white
            return t;
        }

        public void FadeOutBlack() {
            this.isFadingBlack = true;
            this.blackScreenOpacity = 0;
        }

        public void FadeOutWhite() {
            this.isFadingWhite = true;
            this.blackScreenOpacity = 0;
        }
        
    }
}
