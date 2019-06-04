/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ForgottenLight.Levels;

namespace ForgottenLight.UI {
    abstract class UserInterface : UIComponent {

        public SpriteFont MainFont {
            get; protected set;
        }

        public UserInterface(float width, float height, ContentManager content, Scene scene) : base(Vector2.Zero, scene) {
            this.Width = width;
            this.Height = height;
            LoadContent(content);
        }

        private void LoadContent(ContentManager content) {
            this.MainFont = content.Load<SpriteFont>("fonts/Main");
        }

    }
}
