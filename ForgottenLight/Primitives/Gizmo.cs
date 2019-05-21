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
