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

        public string Text {
            get; set;
        }

        public Color Color {
            get; set;
        }

        public SpriteFont Font {
            get; set;
        }
        
        public Vector2 Bounds => Font.MeasureString(Text) * Transform.Scale;

        public float Width => Bounds.X;
        public float Height => Bounds.Y;

        public Vector2 Position {
            get => Transform.LocalPosition;
            set => Transform.LocalPosition = value;
        }

        public Vector2 Scale {
            get => Transform.LocalScale;
            set => Transform.LocalScale = value;
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

            Gizmos.Instance.DrawGizmo(new BoxGizmo(Transform.Position, Width, Height, 1, Color.Orange));
            spriteBatch.DrawString(Font, Text, Transform.Position, Color, 0, Vector2.Zero, Transform.Scale, SpriteEffects.None, 0);
        }

        public override void OnClick() {
            Console.WriteLine("Clicked!");
        }

    }
}
