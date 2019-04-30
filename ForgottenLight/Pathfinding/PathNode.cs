﻿using Microsoft.Xna.Framework;
using ForgottenLight.Entities;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.Pathfinding {
    class PathNode : IHeapItem<PathNode> {

        public Vector2 Position {
            get; private set;
        }

        public int H {
            get; set;
        }

        public int G {
            get; set;
        }

        public int F {
            get => Collided ? -1 : this.H + this.G;
        }

        public bool Collided {
            get;set;
        }

        public Color DebugColor {
            get => F >= 0 ? Color.Red : Color.Black;
        }
    
        public BoxCollider Collider {
            get; private set;
        }

        public Pathfinder Pathfinder {
            get; private set;
        }

        public int X {
            get; private set;
        }

        public int Y {
            get; private set;
        }

        public PathNode Parent {
            get; set;
        }
        public int HeapIndex {
            get;set;
        }

        public PathNode(float width, float height, int h, int g, Vector2 position, int x, int y, Pathfinder pathFinder) {
            this.H = h;
            this.G = g;
            this.Position = position;
            this.X = x;
            this.Y = y;
            this.Pathfinder = pathFinder;

            if(Pathfinder.Entity is ICollidable) {
                ICollidable entity = (ICollidable)Pathfinder.Entity;
                this.Collider = new BoxCollider((int)(width + entity.Collider.Width * 2), (int)(height + entity.Collider.Height), new Vector2(0.5f, 1f), new Transform(position), pathFinder.Entity.Level);
            } else {
                this.Collider = new BoxCollider((int)width, (int)height, new Vector2(0.5f, 0.5f), new Transform(position), pathFinder.Entity.Level);
            }

            this.Collider.Debug = false;
        }

        public void UpdateCollision(GameTime gameTime) {
            this.Collided = false;
            this.Collider.Update(gameTime);
            foreach (Entity e2 in Pathfinder.Entity.Level.Entities) {
                if (e2 != Pathfinder.Entity && e2 is ICollidable && !(e2 is Player)) {
                    ICollidable entity2 = (ICollidable)e2;
                    if (Collider.Intersects(((ICollidable)e2).Collider)) {
                        this.Collided = true;
                    }
                }
            }
        }

        public int CompareTo(PathNode secondNode) {
            int compare = this.F.CompareTo(secondNode.F);
            if(compare == 0) {
                compare = this.H.CompareTo(secondNode.H);
            }
            return -compare;
        }
    }
}