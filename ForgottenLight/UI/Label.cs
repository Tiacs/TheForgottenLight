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

        public Label(string text, Vector2 position, SpriteFont font, Color color) : base(position) {
            this.Text = text;
            this.Font = font;
            this.Color = color;
        }

        public Label(string text, Vector2 position, SpriteFont font) : this(text, position, font, Color.White) {

        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(Font, Text, Transform.Position, Color);
        }

        public override void OnClick() {
            Console.WriteLine("Clicked!");
        }

    }
}
