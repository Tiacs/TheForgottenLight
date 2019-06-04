/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenLight.Levels;

namespace ForgottenLight.UI {
    class Image : UIComponent {
        
        public Texture2D Texture {
            get; set;
        }

        public Vector2 SpriteOrigin {
            get; set;
        }

        public Vector2 SpriteSize {
            get; set;
        } = Vector2.Zero;

        protected override Vector2 Bounds => SpriteSize * Scale;

        public Image(Scene scene) : base(scene) {

        }

        public Image(Texture2D texture, Scene scene) : base(scene) {
            this.Texture = texture;
        }

        public override void OnDraw(SpriteBatch sprite, GameTime gameTime) {
            if(SpriteSize == Vector2.Zero) {
                sprite.Draw(Texture, AbsolutePosition, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
                return;
            }
            sprite.Draw(Texture, AbsolutePosition, new Rectangle(SpriteOrigin.ToPoint(), SpriteSize.ToPoint()), Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
