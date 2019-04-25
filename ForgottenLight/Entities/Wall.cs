using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Levels;
using OpenGL_Test.Primitives;

namespace OpenGL_Test.Entities {
    class Wall : Entity, ICollidable {

        public BoxCollider Collider {
            get; private set;
        }

        public int Width {
            get; private set;
        }

        public int Height {
            get; private set;
        }

        public Wall(int width, int height, Transform transform, Level level) : base(transform, level) {
            this.Width = width;
            this.Height = height;

            this.Collider = new BoxCollider(Width, Height, Vector2.Zero, Transform, Level);
        }

        public Wall(int width, int height, Vector2 position, Level level) : this(width, height, new Transform(position), level) {

        }

        public Wall(int width, int height, float x, float y, Level level) : this(width, height, new Transform(new Vector2(x,y)), level) {

        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            Collider.Update(gameTime);
        }

    }
}
