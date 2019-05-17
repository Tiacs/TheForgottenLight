using System;
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

        public Scene Scene {
            get; private set;
        }

        private int depthIndex;
        public int DepthIndex => depthIndex;

        public Entity(Transform transform, Scene scene, int depthIndex = 0) {
            this.Transform = transform;
            this.Scene = scene;
            this.depthIndex = depthIndex;
        }

        public Entity(Vector2 position, Vector2 scale, Scene scene, int depthIndex = 0) : this(new Transform(position, scale), scene, depthIndex) {

        }

        public Entity(Vector2 position, Scene scene, int depthIndex = 0) : this(new Transform(position), scene, depthIndex) {

        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            this.Transform.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {

        }


        public static bool IsColliding(Entity e1) {
            if(!(e1 is ICollidable)) {
                return false;
            }

            ICollidable entity = (ICollidable) e1;
            if(!entity.Collidable) {
                return false;
            }

            bool colliding = false;

            Vector2 o = Vector2.Zero;
            foreach(Entity e2 in e1.Scene.Entities) {
                if (e2 is ICollidable && e1 != e2) {
                    ICollidable entity2 = (ICollidable) e2;
                    if(entity2.Collidable && entity.Collider.Intersects(entity2.Collider)) {
                        entity.OnCollision(entity2);
                        entity2.OnCollision(entity);
                        colliding = true;
                    }

                    // Edge cases if second entity is player -> also check interact collider
                    if (entity2 is Player && entity.Collider.Intersects(((Player) entity2).InteractCollider)) {
                        entity.OnCollision(entity2);
                        entity2.OnCollision(entity);
                        colliding = true;
                    }
                }
            }
            return colliding;
        }
        
        /// <summary>
        /// Get interactable entity for given entitiy.
        /// </summary>
        /// <param name="entity">Entity which a interectable should be found for</param>
        /// <returns>First interactable found colliding with entity. Returns null if not found or entity does not implement ICollidable.</returns>
        public static IInteractable GetInteractable(Player player) {
            foreach (Entity e2 in player.Scene.Entities) {
                if(e2 is IInteractable && player != e2) {
                    IInteractable entity2 = (IInteractable) e2;
                    if (player.InteractCollider.Intersects(entity2.Collider)) {
                        return entity2;
                    }
                }
            }

            return null;
        }

    }
}
