﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Primitives;
using ForgottenLight.Levels;

namespace ForgottenLight.Entities {
    abstract class Entity {

        public Transform Transform {
            get; set;
        }

        public Level Level {
            get; private set;
        }

        public Entity(Transform transform, Level level) {
            this.Transform = transform;
            this.Level = level;
        }

        public Entity(Vector2 position, Vector2 scale, Level level) : this(new Transform(position, scale), level) {

        }

        public Entity(Vector2 position, Level level) : this(new Transform(position), level) {

        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            this.Transform.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {

        }


        public static bool IsColliding(Entity e1) {
            if(!(e1 is ICollidable)) { // TODO: Maybe throw exception
                return false;
            }

            ICollidable entitiy = (ICollidable) e1;
            Vector2 o = Vector2.Zero;
            foreach(Entity e2 in e1.Level.Entities) {
                if (e2 is ICollidable && e1 != e2) {
                    ICollidable entity2 = (ICollidable) e2;
                    if(entitiy.Collider.Intersects(((ICollidable)e2).Collider)) {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
