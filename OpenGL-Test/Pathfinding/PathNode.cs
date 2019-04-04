﻿using Microsoft.Xna.Framework;
using OpenGL_Test.Entities;
using OpenGL_Test.Levels;
using OpenGL_Test.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Test.Pathfinding {
    class PathNode : IComparable<PathNode> {

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
        
        public PathNode(float width, float height, int h, int g, Vector2 position, int x, int y, Pathfinder pathFinder) {
            this.H = h;
            this.G = g;
            this.Position = position;
            this.X = x;
            this.Y = y;
            this.Pathfinder = pathFinder;

            this.Collider = new BoxCollider((int)width, (int)height, new Vector2(0.5f, 0.5f), new Transform(position), pathFinder.Level);
            this.Collider.Debug = false;
        }

        public void UpdateCollision(GameTime gameTime) {
            this.Collided = false;
            this.Collider.Update(gameTime);
            foreach (Entity e2 in Pathfinder.Level.Entities) {
                if (e2 is ICollidable) {
                    ICollidable entity2 = (ICollidable)e2;
                    if (Collider.Intersects(((ICollidable)e2).Collider)) {
                        this.Collided = true;
                    }
                }
            }
        }

        public int CompareTo(PathNode secondNode) {
            return this.F.CompareTo(secondNode.F);
        }
    }
}