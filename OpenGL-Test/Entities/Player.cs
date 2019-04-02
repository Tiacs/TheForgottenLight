using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Animations;
using OpenGL_Test.Events;
using OpenGL_Test.Primitives;
using OpenGL_Test.Levels;

namespace OpenGL_Test.Entities {
    class Player : Entity, ICollidable {
        
        private AnimationPlayer animationPlayer;

        private Animation idleAnimation;
        private Animation attackAnimation;
        private Animation walkAnimation;

        private const float speed = 200.0f;
        
        private bool isWalking;

        public BoxCollider Collider {
            get; private set;
        }

        private Input keyboardEventHandler;

        public bool Flipped {
            get; set;
        }
        
        public Player(Vector2 position, ContentManager content, Level level) : base(position, level) {

            this.Collider = new BoxCollider(32, 32, new Vector2(.5f, 1), Transform, level);

            this.Transform.Scale = Vector2.One * 1f;
            this.LoadContent(content);

            this.Transform.GizmosEnabled = true;
        }
        
        public Player(float x, float y, ContentManager content, Level level) : this(new Vector2(x, y), content, level) {
        }

        private void LoadContent(ContentManager content) {
            Texture2D atlas = content.Load<Texture2D>("sprite_atlas");

            // Load animations
            this.idleAnimation = new Animation(atlas, 32, 32, Vector2.Zero, 2, 0.5f, true);
            this.attackAnimation = new Animation(atlas, 32, 240, new Vector2(32, 0), 7, 0.1f, false);
            this.walkAnimation = new Animation(atlas, 32, 32, new Vector2(32 + 240, 0), 3, 0.1f, true);

            this.animationPlayer = new AnimationPlayer();

            this.animationPlayer.PlayAnimation(this.idleAnimation);
            
            keyboardEventHandler = Input.Instance;
            keyboardEventHandler.RegisterOnKeyDownEvent(Keys.X, new Input.KeyboardEvent(this.KeyXDown));
            keyboardEventHandler.RegisterOnKeyDownEvent(Keys.Space, new Input.KeyboardEvent(this.KeySpaceDown));
            keyboardEventHandler.RegisterOnKeyUpEvent(Keys.Space, new Input.KeyboardEvent(this.KeySpaceUp));
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            this.keyboardEventHandler.Update();

            this.UpdateMovement(gameTime, keyboardState);

            if (keyboardState.IsKeyDown(Keys.R)) {
                this.Transform.Position = new Vector2(100, 100);
            }

            if (this.animationPlayer.Animation == this.attackAnimation && this.animationPlayer.IsAnimationDone) {
                this.animationPlayer.PlayAnimation(idleAnimation);
            }

            if (this.isWalking && this.animationPlayer.Animation != this.walkAnimation) {
                this.animationPlayer.PlayAnimation(walkAnimation);
            } else if (!this.isWalking && this.animationPlayer.Animation == this.walkAnimation) {
                this.animationPlayer.PlayAnimation(idleAnimation);
            }
        }

        private void UpdateMovement(GameTime gameTime, KeyboardState keyboardState) {

            // create movement direction vector
            Vector2 movement = new Vector2();

            this.isWalking = false;
            if (keyboardState.IsKeyDown(Keys.D)) {
                movement += Vector2.UnitX;
                this.Flipped = false;
                this.isWalking = true;
            }

            if (keyboardState.IsKeyDown(Keys.A)) {
                movement += -Vector2.UnitX;
                this.Flipped = true;
                this.isWalking = true;
            }

            if (keyboardState.IsKeyDown(Keys.S)) {
                movement += Vector2.UnitY;
                this.isWalking = true;
            }

            if (keyboardState.IsKeyDown(Keys.W)) {
                movement += -Vector2.UnitY;
                this.isWalking = true;
            }

            // Normalize vector to prevent faster vertical movement
            if(movement.Length() > 0) movement.Normalize();
            Gizmos.Instance.DrawGizmo(new LineGizmo(Transform.Position, Transform.Position + 35 * movement, 1, Color.Blue)); // draw normalized movement vector

            movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds; // change direction to movement vector

            // Movement X

            // add movement to current position
            Transform.Position += movement * Vector2.UnitX;

            // update collider to current position and check colliding -> if so reset position TODO: FIXME Position should not be reset
            this.Collider.Update(gameTime);
            if(movement.X != 0 && Entity.IsColliding(this)) { // if position has changed -> check colliding
                Transform.Position -= movement * Vector2.UnitX;
                
            }

            // Movement Y

            // add movement to current position
            Transform.Position += movement * Vector2.UnitY;

            // update collider to current position and check colliding -> if so reset position TODO: FIXME Position should not be reset
            this.Collider.Update(gameTime);
            if (movement.Y != 0 && Entity.IsColliding(this)) { // if position has changed -> check colliding
                Transform.Position -= movement * Vector2.UnitY;

            }
        }

        public void KeyXDown() {
            Console.WriteLine("Hello World!");
        }

        public void KeySpaceDown() {
            if ((this.animationPlayer.IsAnimationDone || this.animationPlayer.Animation != attackAnimation) && !this.isWalking) {
                this.animationPlayer.PlayAnimation(attackAnimation);
            }
        }

        public void KeySpaceUp() {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.Position, this.Transform.Scale, this.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }
    }
}
