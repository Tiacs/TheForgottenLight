using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;
using ForgottenLight.Pathfinding;
using System.Collections.Generic;

namespace ForgottenLight.Entities {
    class Ghost : Entity, ICollidable {

        private Animation idleAnimation;

        private AnimationPlayer animationPlayer;

        private const float speed = 50f;

        private Queue<Waypoint> waypoints;

        private Waypoint waypoint;

        private Random random;

        public BoxCollider Collider {
            get; private set;
        }

        public bool Collidable => true;

        private Pathfinder pathfinder;

        private List<Vector2> path;

        private int currentPathNode;

        public Ghost(Vector2 position, ContentManager contentManager, Level level) : base(position, level) {
            
            this.LoadContent(contentManager);
            this.Collider = new BoxCollider(22, 25, new Vector2(.5f, 1), Transform, level);

            this.Transform.Scale = Vector2.One * 1.5f;
            this.Transform.GizmosEnabled = true;
            
            this.random = new Random();
            // this.waypoint = new Vector2(random.Next(0, 800), random.Next(0,400));
            this.waypoints = new Queue<Waypoint>();

            this.path = new List<Vector2>();
            this.pathfinder = new Pathfinder(3*16, 3*9, this);

            //this.waypoints.Enqueue(new Waypoint(100, 100));
            //this.waypoints.Enqueue(new Waypoint(100, 200));
            //this.waypoints.Enqueue(new Waypoint(100, 800));
        }

        public Ghost(float x, float y, ContentManager contentManager, Level level) : this(new Vector2(x,y), contentManager, level) {

        }

        private void LoadContent(ContentManager contentManager) {
            Texture2D atlas = contentManager.Load<Texture2D>("sprite_atlas");

            this.idleAnimation = new Animation(atlas, 25, 22, new Vector2(304,0), 2, 0.5f, true);

            this.animationPlayer = new AnimationPlayer(new Vector2(0.5f, 1));
            this.animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.AbsolutePosition, this.Transform.AbsoluteScale);
        }
        
        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
            //waypoints.Clear();
            //waypoints.Enqueue(new Waypoint(Level.Player.Transform.AbsoluePosition));
            /*
             * 1. Create new waypoint
             */
            if(waypoints.Count == 0) {
                this.waypoints.Enqueue(new Waypoint(random.Next(0, (int)Level.Width), random.Next(0, (int)Level.Height)));
            } 

            if(waypoints.Peek() != null) {
                Gizmos.Instance.DrawGizmo(new LineGizmo(Transform.AbsolutePosition, waypoints.Peek().Position, 2, Color.Orange));
            }

            /*
             * 2. If path done; create next it
             */
            if(path.Count <= 0) {
                // this.waypoint = waypoints.Dequeue();
                this.pathfinder.Update(gameTime);
                this.path = pathfinder.FindPath(this.Transform.AbsolutePosition, this.waypoints.Peek().Position);
                if(path.Count == 0) {
                    this.waypoints.Dequeue(); // remove current waypoint
                }

                /*if (this.path.Count == 0) { // if waypoint not reachable -> remove waypoint from list
                    waypoints.Dequeue();
                }*/

                currentPathNode = 0;
            }

            /*
             * 3. Movement if path is present
             */
            if (path.Count > 0 && currentPathNode < path.Count && waypoints.Count > 0) {
                for (int i = 0; i < path.Count - 1; i++) {
                    Gizmos.Instance.DrawGizmo(new LineGizmo(path[i], path[i + 1], 4, Color.Red));
                }

                // Calculate movement
                Vector2 movement = path[currentPathNode] - Transform.AbsolutePosition;
                movement.Normalize();
                movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;


                bool colliding = false;

                // Movement X

                // add movement to current position
                Transform.Position += movement * Vector2.UnitX;

                // update collider to current position and check colliding
                this.Collider.Update(gameTime);
                if (movement.X != 0 && (colliding |= Entity.IsColliding(this))) { // if position has changed -> check colliding
                    Transform.Position -= movement * Vector2.UnitX;

                }

                // Movement Y

                // add movement to current position
                Transform.Position += movement * Vector2.UnitY;

                // update collider to current position and check colliding
                this.Collider.Update(gameTime);
                if (movement.Y != 0 && (colliding |= Entity.IsColliding(this))) { // if position has changed -> check colliding
                    Transform.Position -= movement * Vector2.UnitY;

                }


                /*
                 * Current path node reached
                 */
                if ((path[currentPathNode] - Transform.AbsolutePosition).Length() <= 1) {
                    this.pathfinder.Update(gameTime);
                    List<Vector2> newPath = pathfinder.FindPath(path[currentPathNode], waypoints.Peek().Position);
                    if (newPath.Count > 0) {
                        path = newPath;
                        currentPathNode = 1;
                    } else {
                        currentPathNode++;
                    }

                    if (currentPathNode >= path.Count) {
                        currentPathNode = 1;
                        path.Clear();
                        waypoints.Dequeue(); // target reached
                    }
                }

                /*
                 * If long colliding
                 */
                if (colliding && waypoints.Count > 0) {
                    if(collideCounter++ > 50) { // if after n frames still colliding; create new waypoint
                        waypoints.Dequeue();
                        path.Clear();
                        currentPathNode = 0;
                    }
                } else {
                    collideCounter = 0;
                }
            }

            // final update of collider for future collision checks
            this.Collider.Update(gameTime);

        }

        int collideCounter = 0;
        
    }
}
