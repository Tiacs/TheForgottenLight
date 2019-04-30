﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Events;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;

namespace ForgottenLight.Entities {
    class Player : Entity, ICollidable {
        
        private AnimationPlayer animationPlayer;

        private Animation idleAnimation;
        private Animation attackAnimation;
        private Animation walkFrontAnimation;
        private Animation walkBackAnimation;
        private Animation walkRightAnimation;
        private Animation walkLeftAnimation;

        private AnimationState animationState = AnimationState.IDLE;
        private AnimationState prevAnimationSatet;

        private Orientation orientation = Orientation.FRONT;
        private Orientation prevOrientation;

        private const float speed = 200.0f;
        
        public BoxCollider Collider {
            get; private set;
        }

        private Input keyboardEventHandler;

        public bool Flipped {
            get; set;
        }

        public bool Collidable => true;

        public Player(Vector2 position, ContentManager content, Level level) : base(position, level) {

            this.Collider = new BoxCollider(22, 38, new Vector2(.5f, 1), Transform, level);

            this.Transform.Scale = Vector2.One * 1.5f;
            this.LoadContent(content);

            this.Transform.GizmosEnabled = true;
        }
        
        public Player(float x, float y, ContentManager content, Level level) : this(new Vector2(x, y), content, level) {
        }

        private void LoadContent(ContentManager content) {
            Texture2D atlas = content.Load<Texture2D>("sprites/player");

            // Load animations
            this.idleAnimation = new Animation(atlas, 38, 22, Vector2.Zero, 12, 0.1f, true);
            this.attackAnimation = new Animation(atlas, 32, 240, new Vector2(32, 0), 7, 0.1f, false);
            this.walkFrontAnimation = new Animation(atlas, 38, 22, new Vector2(44, 0), 4, 0.1f, true);
            this.walkBackAnimation = new Animation(atlas, 38, 22, new Vector2(66, 0), 4, 0.1f, true);
            this.walkRightAnimation = new Animation(atlas, 38, 22, new Vector2(88, 0), 2, 0.1f, true);
            this.walkLeftAnimation = new Animation(atlas, 38, 22, new Vector2(110, 0), 2, 0.1f, true);

            this.animationPlayer = new AnimationPlayer();

            this.animationPlayer.PlayAnimation(this.idleAnimation);
            
            keyboardEventHandler = Input.Instance;
            keyboardEventHandler.RegisterOnKeyDownEvent(Keys.X, new Input.KeyboardEvent(this.KeyXDown));
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            this.keyboardEventHandler.Update();

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
                                animationPlayer.PlayAnimation(idleAnimation);
                                break;
                            case Orientation.BACK:
                                animationPlayer.PlayAnimation(idleAnimation);
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
       
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.Position, this.Transform.Scale);
        }

        private enum AnimationState {
            IDLE, WALKING
        }

        private enum Orientation {
            FRONT, BACK, LEFT, RIGHT
        }
    }
}