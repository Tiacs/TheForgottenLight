/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenLight.Primitives {
    class LineGizmo : Gizmo {
        
        private Vector2 end;
        private int lineWidth;
        private Color color;

        public LineGizmo(Vector2 start, Vector2 end, int lineWidth, Color color) : base(start) {
            this.end = end;
            this.lineWidth = lineWidth;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            DrawLine(Position, end, lineWidth, color, spriteBatch);
        }

        public static void DrawLine(Vector2 start, Vector2 end, int lineWidth, Color color, SpriteBatch spriteBatch) {
            Vector2 edge = end - start;
            float angle = (float) Math.Atan2(edge.Y, edge.X); // calculate angle to rotate line
            spriteBatch.Draw(Gizmos.Instance.PlainColor, new Rectangle((int) start.X, (int) start.Y, (int) edge.Length(), lineWidth), null, 
                color, // set specific color
                angle, // rotate
                new Vector2(0,0.5f), // set origin point inside of line (ratio; lineWidth = 1.0f -> lineWidth/2 = 0.5f)
                SpriteEffects.None, 0); 
        }
    }
}
