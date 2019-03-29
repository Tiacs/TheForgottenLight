using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenGL_Test.Primitives {
    class LineGizmo : Gizmo {
        
        private Vector2 direction;
        private Color color;

        public LineGizmo(Vector2 start, Vector2 direction, Color color) : base(start) {
            this.direction = direction;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            DrawLine(this.Position, this.direction, 1, color, spriteBatch);
        }

        public static void DrawLine(Vector2 start, Vector2 end, int lineWidth, Color color, SpriteBatch spriteBatch) {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)System.Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(Gizmos.Instance.PlainColor,
                new Rectangle(
                    (int)start.X - lineWidth/2,
                    (int)start.Y,
                    (int)edge.Length(),
                    lineWidth/2),
                null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
