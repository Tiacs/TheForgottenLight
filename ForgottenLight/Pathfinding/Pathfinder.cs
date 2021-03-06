﻿/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using ForgottenLight.Entities;
using ForgottenLight.Primitives;

namespace ForgottenLight.Pathfinding {
    class Pathfinder {
        
        public Entity Entity {
            get; private set;
        }

        private int horizontalDivision, verticalDivision;

        private PathNode[,] nodes;

        private bool initialized;
    
        public Pathfinder(int horizontalDivision, int verticalDivision, Entity entity) {
            this.horizontalDivision = horizontalDivision;
            this.verticalDivision = verticalDivision;
            this.Entity = entity;

            this.nodes = new PathNode[verticalDivision, horizontalDivision];
            this.CreateNodes();
        }

        private PathNode FindNearestNode(Vector2 position) {
            int x = (int) (position.X / Entity.Scene.Width * horizontalDivision);
            int y = (int)(position.Y / Entity.Scene.Height * verticalDivision);
            if (y < 0 || y >= nodes.GetLength(0) || x < 0 || x >= nodes.GetLength(1)) {
                return null;
            }

            PathNode node = nodes[y, x];
            if(node.F >= 0) {
                return node;
            }

            List<PathNode> neighbours = GetNeighbours(node);
            if (neighbours.Count == 0) return node;

            PathNode nearest = neighbours[0];
            for(int i = 1; i < neighbours.Count; i++) {
                if(neighbours[i].F >= 0 && (position - neighbours[i].Position).Length() < (position - nearest.Position).Length()) {
                    nearest = neighbours[i];                                                                              
                }
            }
            return nearest.F >= 0 ? nearest : node;
        }

        public List<Vector2> FindPath(Vector2 start, Vector2 end) {
            List<PathNode> nodes = FindPath(FindNearestNode(start), FindNearestNode(end));

            foreach (PathNode node in nodes) {
                if (node.Parent != null)
                    Gizmos.Instance.DrawGizmo(new LineGizmo(node.Parent.Position, node.Position, 4, Color.Orange));
            }

            List<Vector2> path = new List<Vector2>();
            if (nodes.Count <= 0) return path;
            
            PathNode current = nodes.Last();
            while(current != null) {
                path.Add(current.Position);
                current = current.Parent;
            }
            path.Reverse();
            return path;
        }
        
        
        public List<PathNode> FindPath(PathNode start, PathNode end) {
            if(start == null || end == null || start.F < 0 || end.F < 0 || !initialized) { // if start or end point are null or not movable
                return new List<PathNode>();
            }

            Heap<PathNode> open = new Heap<PathNode>(nodes.Length);
            List<PathNode> closed = new List<PathNode>();
            open.Add(start);
            PathNode current;
            while(open.Count > 0) {
                current = open.RemoveFirst();
                closed.Add(current);

                if(current == end) { // end reached -> path found
                    return closed;
                }

                foreach(PathNode neighbour in GetNeighbours(current)) {
                    if(neighbour.F < 0 || closed.Contains(neighbour)) {
                        continue;
                    }

                    int gCost = GetGCosts(current, neighbour);
                    if (gCost < neighbour.G  || !open.Contains(neighbour)) {
                        // update costs
                        neighbour.G = gCost;

                        neighbour.H = GetHCosts(neighbour, end);

                        // update parent
                        neighbour.Parent = current;

                        // add to open
                        if (!open.Contains(neighbour)) {
                            open.Add(neighbour);
                        }
                    }
                }
            }
            
            return new List<PathNode>(); // Return empty list if no path found
        }

        private int GetGCosts(PathNode node, PathNode neighbour) {
            if (Math.Abs(node.X) != Math.Abs(neighbour.X) && Math.Abs(node.Y) != Math.Abs(neighbour.Y)) { // diagonal
                return node.G + 14;
            } else {
                return node.G + 10;
            }
        }

        private int GetHCosts(PathNode node, PathNode end) {
            int costX = Math.Abs(end.X - node.X);
            int costY = Math.Abs(end.Y - node.Y);
            if(costX > costY) {
                return costX * 10 + costY * 14;
            }
            return costX * 10 + costY * 14;
        }

        private List<PathNode> GetNeighbours(PathNode node) {
            List<PathNode> neighbours = new List<PathNode>();

            for(int y = Math.Max(node.Y-1, 0); y <= Math.Min(node.Y + 1, nodes.GetLength(0)-1); y++) {
                for (int x = Math.Max(node.X - 1, 0); x <= Math.Min(node.X + 1, nodes.GetLength(1)-1); x++) {
                    if(nodes[y,x] != node && nodes[y,x].F >= 0) {
                        neighbours.Add(nodes[y, x]);
                    }
                }
            }
                
            return neighbours;
        }

        private void CreateNodes() {
            float cellWidth = Entity.Scene.Width / horizontalDivision;
            float cellHeight = Entity.Scene.Height / verticalDivision;

            for (int y = 0; y < nodes.GetLength(0); y++) {
                for(int x = 0; x < nodes.GetLength(1); x++) {
                    nodes[y, x] = new PathNode(cellWidth, cellHeight, 0, 0, new Vector2(x * cellWidth + cellWidth/2, y * cellHeight + cellHeight/2), x, y, this);
                }
            } 
        }
        
        public void Update(GameTime gameTime) {
            UpdateNodes(gameTime);
            this.initialized = true;
        }

        private void UpdateNodes(GameTime gameTime) {
            for (int y = 0; y < nodes.GetLength(0); y++) {
                for (int x = 0; x < nodes.GetLength(1); x++) {
                    nodes[y, x].Parent = null;
                    nodes[y, x].G = 0;
                    nodes[y, x].H = 0;

                    nodes[y, x].UpdateCollision(gameTime);
                }
            }
        }
        
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T> {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}
