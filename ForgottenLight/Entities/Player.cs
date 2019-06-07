/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

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
        
        private const float speed = 100.0f;

        public Level Level => (Level)Scene;

        public bool IsDead {
            get;set;
        }
        
        public Inventory Inventory {
            get; set;
        }

        public BoxCollider Collider {
            get; private set;
        }

        public BoxCollider InteractCollider {
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

        public Player(Vector2 position, ContentManager content, Level level) : base(position, Vector2.One, level, 100) {

            this.Collider = new BoxCollider(22, 38/3, new Vector2(.5f, 1), Transform, level);

            this.InteractCollider = new BoxCollider(22, 38, new Vector2(.5f, 1), Transform, level);

            this.Inventory = new Inventory();
            this.LoadContent(content);

            this.Transform.GizmosEnabled = true;
        }
        
        public Player(float x, float y, ContentManager content, Level level) : this(new Vector2(x, y), content, level) {
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

            if(this.IsDead && !Level.Hud.DialogBox.IsDialogRunning) {
                Scene.ReloadScene();
            }

            InteractCollider.Update(gameTime);
        }

        private void InteractKeyDown() {
            if(Interactable != null) {
                Interact(Interactable);
            }
        }

        private void Interact(IInteractable interactable) {
            if (Level.Hud.DialogBox.IsDialogRunning) return; // If dialog is running, do not count interaction
            interactable.OnInteract(this);
        }
       
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.AbsolutePosition, this.Transform.AbsoluteScale);
        }

        public void OnCollision(ICollidable collidingEntity) {
            if (collidingEntity is Ghosts.Ghost && !this.IsDead) {
                this.Level.Hud.FadeOutBlack();
                this.IsDead = true;
                Level.Hud.DialogBox.Enqueue("You got caught by the ghosts!");
                Level.Hud.DialogBox.Enqueue("Maybe you have more luck next time...");
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
