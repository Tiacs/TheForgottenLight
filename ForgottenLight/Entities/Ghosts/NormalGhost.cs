using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Levels;
using ForgottenLight.Pathfinding;

namespace ForgottenLight.Entities.Ghosts {
    class NormalGhost : Ghost {

        public List<Waypoint> Patrol {
            get;set;
        }

        private int currentPatrol;
        
        public NormalGhost(Vector2 position, ContentManager contentManager, Scene level) : base(position, contentManager, level) {
            this.Patrol = new List<Waypoint>();
        }


        public NormalGhost(float x, float y, ContentManager contentManager, Scene level) : this(new Vector2(x, y), contentManager, level) {

        }

        protected override void LoadContent(ContentManager contentManager) {
            Texture2D atlas = contentManager.Load<Texture2D>("sprites/sprite_atlas");

            this.idleAnimation = new Animation(atlas, 25, 22, new Vector2(0, 0), 2, 0.5f, true);

            this.animationPlayer = new AnimationPlayer(new Vector2(0.5f, 1));
            this.animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            //waypoints.Clear();
            //waypoints.Enqueue(new Waypoint(Level.Player.Transform.AbsolutePosition));

            if (waypoints.Count == 0 && Patrol.Count > 0) { // If no more waypoints in queue -> set next patrol point
                this.waypoints.Enqueue(Patrol[currentPatrol++ % Patrol.Count]);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

        }

        public override void OnCollision(ICollidable collidingEntity) {

        }
    }
}
