using ForgottenLight.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.UI {
    class Label : UIComponent {

        // static font scaling because of font overscaling in content pipeline; used because pixel font is not made for preferred font scaling
        private const float FONT_SCALE = .5f;

        private string text = "";
        public string Text {
            get => text;
            set {
                if(value != text) {
                    text = value;
                    formattedNeedsUpdate = true;
                }
            }
        }

        private bool formattedNeedsUpdate = true;

        private string formattedText;
        private string FormattedText {
            get {
                if(formattedNeedsUpdate) {
                    UpdateFormattedText();
                }
                return formattedText;
            }
            set => formattedText = value;
        }

        public Color Color {
            get; set;
        } = Color.Black;

        public SpriteFont Font {
            get; set;
        }
        
        protected override Vector2 Bounds => Font.MeasureString(FormattedText) * Transform.Scale * FONT_SCALE;

        public override float Width => Bounds.X;
        public override float Height => Bounds.Y;

        private float maxWidth;
        public float MaxWidth {
            get => maxWidth <= 0 ? Parent.Width : maxWidth; // If no maxWidth set -> maxWidth is Parent.Width
            set => maxWidth = value;
        }

        public Label(SpriteFont font) {
            this.Font = font;
        }

        public Label(string text, Vector2 position, SpriteFont font, Color color) : base(position) {
            this.Text = text;
            this.Font = font;
            this.Color = color;
        }

        public Label(string text, Vector2 position, SpriteFont font) : this(text, position, font, Color.White) {

        }
        
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) {
            // Gizmos.Instance.DrawGizmo(new BoxGizmo(Transform.AbsolutePosition, MaxWidth, Height, 1, Color.Gray)); // Draw MaxWdith box
            //Gizmos.Instance.DrawGizmo(new BoxGizmo(AbsolutePosition, Width, Height, 1, Color.Orange)); // Draw actual text box

            spriteBatch.DrawString(Font, FormattedText, AbsolutePosition, Color, 0, Vector2.Zero, Transform.Scale * FONT_SCALE, SpriteEffects.None, 0);
        }

        private void UpdateFormattedText() {
            this.FormattedText = FormatText(Text);
            this.formattedNeedsUpdate = false;
        }

        private string FormatText(string text) {
            if(MaxWidth <= 0) {
                return text;
            }
            
            string result = "";
            foreach (string c in text.Split(' ')) {
                Vector2 bounds = Font.MeasureString(result + c + " ") * Transform.Scale * FONT_SCALE;
                if(bounds.X > this.MaxWidth) {
                    result += "\n";
                }
                result += c + " ";
            }
            return result;
        }

        public override void OnClick() {
            Console.WriteLine("Clicked!");
        }

    }
}
