using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Events;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;
using ForgottenLight.Items;

namespace ForgottenLight.Entities {
    class Player : Entity, ICollidable {
        
        private AnimationPlayer animationPlayer;

        private Animation idleFrontAnimation;
        private Animation idleBackAnimation;
        private Animation walkFrontAnimation;
        private Animation walkBackAnimation;
        private Animation walkRightAnimation;
        private Animation walkLeftAnimation;

        private AnimationState animationState = AnimationState.IDLE;
        private AnimationState prevAnimationSatet;

        private Orientation orientation = Orientation.FRONT;
        private Orientation prevOrientation;

        private const float speed = 200.0f;

        public bool IsDead {
            get;set;
        }

        public Item Item {
            get; set;
        }
        
        public BoxCollider Collider {
            get; private set;
        }

        private Input keyboardEventHandler;

        public bool Flipped {
            get; set;
        }

        public IInteractable Interactable {
            get; private set;
        }

        public bool Collidable => true;

        public Player(Vector2 position, ContentManager content, Scene level) : base(position, level, 100) {

            this.Collider = new BoxCollider(22, 38, new Vector2(.5f, 1), Transform, level);

            this.Transform.Scale = Vector2.One * 1.5f;
            this.LoadContent(content);

            this.Transform.GizmosEnabled = true;
        }
        
        public Player(float x, float y, ContentManager content, Scene level) : this(new Vector2(x, y), content, level) {
        }

        private void LoadContent(ContentManager content) {
            Texture2D atlas = content.Load<Texture2D>("sprites/player");

            // Load animations
            this.idleFrontAnimation = new Animation(atlas, 38, 22, Vector2.Zero, 12, 0.1f, true);
            this.idleBackAnimation = new Animation(atlas, 38, 22, new Vector2(22, 0), 12, 0.1f, true);
            this.walkFrontAnimation = new Animation(atlas, 38, 22, new Vector2(44, 0), 4, 0.1f, true);
            this.walkBackAnimation = new Animation(atlas, 38, 22, new Vector2(66, 0), 4, 0.1f, true);
            this.walkRightAnimation = new Animation(atlas, 38, 22, new Vector2(88, 0), 2, 0.1f, true);
            this.walkLeftAnimation = new Animation(atlas, 38, 22, new Vector2(110, 0), 2, 0.1f, true);

            this.animationPlayer = new AnimationPlayer(new Vector2(0.5f, 1));

            this.animationPlayer.PlayAnimation(this.idleFrontAnimation);
            
            keyboardEventHandler = Input.Instance;
            keyboardEventHandler.RegisterOnKeyDownEvent(Keys.E, new Input.KeyboardEvent(this.InteractKeyDown));
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
            
            this.UpdateMovement(gameTime, keyboardState);

            if (keyboardState.IsKeyDown(Keys.R)) {
                this.Transform.Position = new Vector2(100, 100);
            }
            
            if(animationState != prevAnimationSatet || orientation != prevOrientation) {
                switch (animationState) {
                    case AnimationState.IDLE:
                        switch (orientation) {
                            case Orientation.LEFT:
                            case Orientation.RIGHT:
                            case Orientation.FRONT:
                                animationPlayer.PlayAnimation(idleFrontAnimation);
                                break;
                            case Orientation.BACK:
                                animationPlayer.PlayAnimation(idleBackAnimation);
                                break;
                        }
                        break;
                    case AnimationState.WALKING:
                        switch (orientation) {
                            case Orientation.FRONT:
                                animationPlayer.PlayAnimation(walkFrontAnimation);
                                break;
                            case Orientation.BACK:
                                animationPlayer.PlayAnimation(walkBackAnimation);
                                break;
                            case Orientation.LEFT:
                                animationPlayer.PlayAnimation(walkLeftAnimation);
                                break;
                            case Orientation.RIGHT:
                                animationPlayer.PlayAnimation(walkRightAnimation);
                                break;
                        }
                        break;
                }
            }
            prevAnimationSatet = animationState;
            prevOrientation = orientation;

            this.Interactable = Entity.GetInteractable(this);
            if (Interactable != null) { // interactable found
                // Do something if interactable is present                
            }
        }

        private void UpdateMovement(GameTime gameTime, KeyboardState keyboardState) {

            // create movement direction vector
            Vector2 movement = new Vector2();
            this.animationState = AnimationState.IDLE;
            
            if (keyboardState.IsKeyDown(Keys.S)) {
                movement += Vector2.UnitY;
                this.animationState = AnimationState.WALKING;
                this.orientation = Orientation.FRONT;
            }

            if (keyboardState.IsKeyDown(Keys.W)) {
                movement += -Vector2.UnitY;
                this.animationState = AnimationState.WALKING;
                this.orientation = Orientation.BACK;
            }

            if (keyboardState.IsKeyDown(Keys.D)) {
                movement += Vector2.UnitX;
                this.animationState = AnimationState.WALKING;
                this.orientation = Orientation.RIGHT;
            }

            if (keyboardState.IsKeyDown(Keys.A)) {
                movement += -Vector2.UnitX;
                this.animationState = AnimationState.WALKING;
                this.orientation = Orientation.LEFT;
            }

            // Normalize vector to prevent faster vertical movement
            if (movement.Length() > 0) movement.Normalize();
            Gizmos.Instance.DrawGizmo(new LineGizmo(Transform.AbsolutePosition, Transform.AbsolutePosition+ 35 * movement, 1, Color.Blue)); // draw normalized movement vector

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

            if(this.IsDead && !Level.Interface.DialogBox.IsDialogRunning) {
                Level.ReloadScene();
            }

        }

        private void InteractKeyDown() {
            if(Interactable != null) {
                Interact(Interactable);
            }
        }

        private void Interact(IInteractable interactable) {
            if (Level.Interface.DialogBox.IsDialogRunning) return; // If dialog is running, do not count interaction
            interactable.OnInteract(this);
        }
       
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.AbsolutePosition, this.Transform.AbsoluteScale);
        }

        public void OnCollision(ICollidable collidingEntity) {
            if (collidingEntity is Ghosts.Ghost && !this.IsDead) {
                this.IsDead = true;
                Level.Interface.DialogBox.Enqueue("You got caught by the ghosts!");
            }
        }

        private enum AnimationState {
            IDLE, WALKING
        }

        private enum Orientation {
            FRONT, BACK, LEFT, RIGHT
        }
    }
}
