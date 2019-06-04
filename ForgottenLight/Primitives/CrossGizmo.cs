/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenLight.Primitives {
    class CrossGizmo : Gizmo {

        private float size;
        private int width;
        private Color color;

        public CrossGizmo(Vector2 position, float size, int width, Color color) : base(position) {
            this.size = size;
            this.width = width;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            LineGizmo.DrawLine(new Vector2(Position.X, Position.Y - size / 2), new Vector2(Position.X, Position.Y + size / 2), width, color, spriteBatch);
            LineGizmo.DrawLine(new Vector2(Position.X - size / 2, Position.Y), new Vector2(Position.X + size / 2, Position.Y), width, color, spriteBatch);
        }
    }
}
