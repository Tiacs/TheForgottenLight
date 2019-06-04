/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenLight.Primitives {
    abstract class Gizmo {

        public Vector2 Position {
            get; set;
        }

        public Gizmo(Vector2 position) {
            this.Position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
