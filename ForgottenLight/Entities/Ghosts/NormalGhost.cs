using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Levels;
using ForgottenLight.Pathfinding;
using System;

namespace ForgottenLight.Entities.Ghosts {
    class NormalGhost : Ghost {

        private List<Waypoint> patrol = new List<Waypoint>();
        private int currentPatrol;

        public NormalGhost(Vector2 position, ContentManager contentManager, Level level) : base(position, contentManager, level) {
            this.patrol.Add(new Waypoint(position.X, position.Y));
            this.patrol.Add(new Waypoint(position.X, position.Y + 100));
        }


        public NormalGhost(float x, float y, ContentManager contentManager, Level level) : this(new Vector2(x, y), contentManager, level) {

        }

        protected override void LoadContent(ContentManager contentManager) {
            Texture2D atlas = contentManager.Load<Texture2D>("sprite_atlas");

            this.idleAnimation = new Animation(atlas, 25, 22, new Vector2(304, 0), 2, 0.5f, true);

            this.animationPlayer = new AnimationPlayer(new Vector2(0.5f, 1));
            this.animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            //waypoints.Clear();
            //waypoints.Enqueue(new Waypoint(Level.Player.Transform.AbsolutePosition));

            if (waypoints.Count == 0) { // If no more waypoints in queue -> set next patrol point
                this.waypoints.Enqueue(patrol[currentPatrol++ % patrol.Count]);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

        }

        public override void OnCollision(ICollidable collidingEntity) {
            if(collidingEntity is Player && !((Player)collidingEntity).IsDead) {
                ((Player)collidingEntity).IsDead = true;
                Level.Interface.DialogBox.Enqueue("You got caught by the ghosts!");
            }
        }
    }
}
