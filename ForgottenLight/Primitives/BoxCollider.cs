﻿/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;

using ForgottenLight.Levels;

namespace ForgottenLight.Primitives {
    class BoxCollider {

        private Transform transform;
        private Scene level;

        public Rectangle Rectangle {
            get; private set;
        }

        public int Width {
            get;set;
        }

        public int Height {
            get;set;
        }

        public Vector2 Pivot {
            get;set;
        }

        public bool Debug {
            get; set;
        }

        public BoxCollider(int width, int height, Vector2 pivot, Transform transform, Scene level) {
            this.Width = width;
            this.Height = height;
            this.Pivot = pivot;
            this.transform = transform;
            this.level = level;

            this.Debug = true;
        }

        public void Update(GameTime gameTime) {
            this.Rectangle = new Rectangle((int) (transform.AbsolutePosition.X - Width * Pivot.X * transform.AbsoluteScale.X), 
                (int) (transform.AbsolutePosition.Y - Height * Pivot.Y * transform.AbsoluteScale.Y), 
                (int) (Width * transform.AbsoluteScale.X), 
                (int) (Height * transform.AbsoluteScale.Y)
            );
            
            if(this.Debug) Gizmos.Instance.DrawGizmo(new BoxGizmo(Rectangle, 1, Color.Green));
        }

        public bool Intersects(BoxCollider boxCollider) {
            return Intersects(this, boxCollider);
        }
        
        public static bool Intersects(BoxCollider b1, BoxCollider b2) {
            return b1.Rectangle.Intersects(b2.Rectangle);
        }

    }
}
