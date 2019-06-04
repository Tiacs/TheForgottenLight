/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;
using ForgottenLight.Pathfinding;

namespace ForgottenLight.Entities.Ghosts {
    abstract class Ghost : Entity, ICollidable {

        protected Animation idleAnimation;

        protected AnimationPlayer animationPlayer;

        private const float speed = 60f;

        protected Queue<Waypoint> waypoints;
        
        public BoxCollider Collider {
            get; private set;
        }

        public bool Collidable => true;

        public virtual bool NoClip => false;

        private Pathfinder pathfinder;

        private List<Vector2> path;

        private int currentPathNode;
        
        public Ghost(Vector2 position, ContentManager contentManager, Scene level, int depthIndex=50) : base(position, Vector2.One, level, depthIndex) {
            
            this.LoadContent(contentManager);
            this.Collider = new BoxCollider(18, 20, new Vector2(.5f, 1), Transform, level);
            
            this.Transform.GizmosEnabled = true;
            
            this.waypoints = new Queue<Waypoint>();

            this.path = new List<Vector2>();
            this.pathfinder = new Pathfinder(2*16, 2*9, this);

            //this.waypoints.Enqueue(new Waypoint(100, 100));
            //this.waypoints.Enqueue(new Waypoint(100, 200));
            //this.waypoints.Enqueue(new Waypoint(100, 800));

        }

        public Ghost(float x, float y, ContentManager contentManager, Scene level, int depthIndex = 50) : this(new Vector2(x,y), contentManager, level, depthIndex) {

        }

        protected abstract void LoadContent(ContentManager contentManager);

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

            this.animationPlayer.Draw(spriteBatch, gameTime, this.Transform.AbsolutePosition, this.Transform.AbsoluteScale);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) { // TODO: Pathfinding usage and waypoints processing needs refactor
            base.Update(gameTime, keyboardState, mouseState);
            
            if(waypoints.Count > 0) {
                Gizmos.Instance.DrawGizmo(new LineGizmo(Transform.AbsolutePosition, waypoints.Peek().Position, 2, Color.Orange));
            }

            /*
             * 2. If path done; create next it
             */
            if(path.Count <= 0 && waypoints.Count > 0) {
                if (!NoClip) {
                    // this.waypoint = waypoints.Dequeue();
                    this.pathfinder.Update(gameTime);
                    this.path = pathfinder.FindPath(this.Transform.AbsolutePosition, this.waypoints.Peek().Position);
                    if (path.Count == 0) {
                        this.waypoints.Dequeue(); // remove current waypoint
                    }
                } else {
                    this.path.Clear();
                    this.path.Add(this.waypoints.Peek().Position);
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
                if(movement.Length() > 0)
                    movement.Normalize();
                movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // add movement to current position
                Transform.Position += movement;

                /*
                 * Current path node reached
                 */
                if ((path[currentPathNode] - Transform.AbsolutePosition).Length() <= 1) {
                    if (!NoClip) {
                        pathfinder.Update(gameTime);
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
                    } else {
                        this.path.Clear(); // clear path and dequeue current waypoint
                        this.waypoints.Dequeue();
                    }
                }
            }

            // final update of collider for future collision checks
            this.Collider.Update(gameTime);
            Entity.IsColliding(this); // Check collission for events
            
        }

        public abstract void OnCollision(ICollidable collidingEntity);
        
    }
}
