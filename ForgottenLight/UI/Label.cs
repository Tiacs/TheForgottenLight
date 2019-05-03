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

        private string text;
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
        
        public Vector2 Bounds => Font.MeasureString(FormattedText) * Transform.Scale;

        public float Width => Bounds.X;
        public float Height => Bounds.Y;

        public float MaxWidth {
            get; set;
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
        
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            // Gizmos.Instance.DrawGizmo(new BoxGizmo(Transform.AbsolutePosition, MaxWidth, Height, 1, Color.Gray)); // Draw MaxWdith box
            Gizmos.Instance.DrawGizmo(new BoxGizmo(Transform.AbsolutePosition, Width, Height, 1, Color.Orange)); // Draw actual text box

            spriteBatch.DrawString(Font, FormattedText, Transform.AbsolutePosition, Color, 0, Vector2.Zero, Transform.Scale, SpriteEffects.None, 0);
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
            foreach (char c in text) {
                Vector2 bounds = Font.MeasureString(result + c);
                if(bounds.X > this.MaxWidth) {
                    result += "\n";
                }
                result += c;
            }
            return result;
        }

        public override void OnClick() {
            Console.WriteLine("Clicked!");
        }

    }
}
