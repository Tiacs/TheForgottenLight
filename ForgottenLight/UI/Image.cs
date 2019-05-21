/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
