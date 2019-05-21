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
    class BoxGizmo : Gizmo {

        private float width;
        private float height;
        private int lineWidth;
        private Color color;

        public BoxGizmo(Vector2 position, float width, float height, int lineWidth, Color color) : base(position) {
            this.width = width;
            this.height = height;
            this.lineWidth = lineWidth;
            this.color = color;
        }

        public BoxGizmo(Rectangle rectangle, int lineWidth, Color color) : this(rectangle.Location.ToVector2(), rectangle.Width, rectangle.Height, lineWidth, color) {

        }

        public override void Draw(SpriteBatch spriteBatch) {
            LineGizmo.DrawLine(Position, Position + Vector2.UnitX * width, lineWidth, color, spriteBatch);
            LineGizmo.DrawLine(Position + Vector2.UnitX * width, Position + Vector2.UnitY * height + Vector2.UnitX * width, lineWidth, color, spriteBatch);
            LineGizmo.DrawLine(Position + Vector2.UnitY * height, Position + Vector2.UnitY * height + Vector2.UnitX * width, lineWidth, color, spriteBatch);
            LineGizmo.DrawLine(Position, Position + Vector2.UnitY * height, lineWidth, color, spriteBatch);
        }
    }
}
