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

        public Image(Scene scene) : base(scene) {

        }

        public Image(Texture2D texture, Scene scene) : base(scene) {
            this.Texture = texture;
        }

        public override void OnDraw(SpriteBatch sprite, GameTime gameTime) {
            sprite.Draw(Texture, AbsolutePosition, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
