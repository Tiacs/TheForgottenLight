using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Primitives;

namespace OpenGL_Test.Entities {
    abstract class Entity {

        public Transform Transform {
            get; set;
        }

        public Entity(Transform transform) {
            this.Transform = transform;
        }

        public Entity(Vector2 position, Vector2 scale) : this(new Transform(position, scale)) {

        }

        public Entity(Vector2 position) : this(new Transform(position)) {

        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            this.Transform.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {

        }

    }
}
