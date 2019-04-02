using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Animations;
using OpenGL_Test.Primitives;
using OpenGL_Test.Levels;

namespace OpenGL_Test.Entities {
    class Ghost : Entity, ICollidable {

        private Animation idleAnimation;

        private AnimationPlayer animationPlayer;

        private const float speed = 50f;

        private Vector2 waypoint;

        private Random random;

        public BoxCollider Collider {
            get; private set;
        }

        public Ghost(Vector2 position, ContentManager contentManager, Level level) : base(position, level) {

            this.LoadContent(contentManager);
            this.Collider = new BoxCollider(22, 25, new Vector2(.5f, 1), Transform, level);

            this.Transform.Scale = Vector2.One * 1.5f;
            this.Transform.GizmosEnabled = true;

            this.random = new Random();
            this.waypoint = new Vector2(random.Next(0, 800), random.Next(0,400));
        }

        public Ghost(float x, float y, ContentManager contentManager, Level level) : this(new Vector2(x,y), contentManager, level) {

        }

        private void LoadContent(ContentManager contentManager) {
            Texture2D atlas = contentManager.Load<Texture2D>("sprite_atlas");

            this.idleAnimation = new Animation(atlas, 25, 22, new Vector2(304,0), 2, 0.5f, true);

            this.animationPlayer = new AnimationPlayer();
            this.animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.Position, this.Transform.Scale);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            Gizmos.Instance.DrawGizmo(new LineGizmo(Transform.Position, waypoint, 2, Color.Orange));
            
            Vector2 movement = waypoint - Transform.Position;
            movement.Normalize();
            movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Transform.Position += movement;

            this.Collider.Update(gameTime);

            // Check collision
            bool collide = Entity.IsColliding(this);
            
            if(collide) {
                Transform.Position -= movement;
            }

            if (collide || (waypoint - Transform.Position).Length() <= 10) {
                this.waypoint = new Vector2(random.Next(0, 800), random.Next(0, 400));
            }
        }
    }
}
