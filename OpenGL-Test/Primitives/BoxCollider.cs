using System;

using Microsoft.Xna.Framework;

using OpenGL_Test.Levels;

namespace OpenGL_Test.Primitives {
    class BoxCollider {

        private Transform transform;
        private Level level;

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

        public BoxCollider(int width, int height, Vector2 pivot, Transform transform, Level level) {
            this.Width = width;
            this.Height = height;
            this.Pivot = pivot;
            this.transform = transform;
            this.level = level;

            this.Debug = true;
        }

        public void Update(GameTime gameTime) {
            this.Rectangle = new Rectangle((int) (transform.Position.X - Width * Pivot.X * transform.Scale.X), 
                (int) (transform.Position.Y - Height * Pivot.Y * transform.Scale.Y), 
                (int) (Width * transform.Scale.X), 
                (int) (Height * transform.Scale.Y)
            );
            
            if(this.Debug) Gizmos.Instance.DrawGizmo(new BoxGizmo(Rectangle, 1, Color.Green));
        }

        public bool Intersects(BoxCollider boxCollider) {
            return Intersects(this, boxCollider);
        }

        internal void Update(object gameTime) {
            throw new NotImplementedException();
        }

        public static bool Intersects(BoxCollider b1, BoxCollider b2) {
            return b1.Rectangle.Intersects(b2.Rectangle);
        }

    }
}
