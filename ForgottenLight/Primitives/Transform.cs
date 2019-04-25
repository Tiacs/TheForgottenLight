using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace ForgottenLight.Primitives {

    class Transform {

        private Vector2 position;
        public Vector2 Position {
            get => position;
            set {
                position = value;
                this.SetNeedsPositionUpdate();
            }
        }

        private Vector2 scale;
        public Vector2 Scale {
            get => scale;
            set {
                scale = value;
                this.SetNeedsPositionUpdate();
            }
        }

        private float rotation;
        public float Rotation {
            get => rotation;
            set {
                rotation = value;
                this.SetNeedsPositionUpdate();
            }
        }


        private Vector2 localPosition;
        public Vector2 LocalPosition {
            get => localPosition;
            set {
                localPosition = value;
            }
        }

        public Vector2 LocalScale {
            get; set;
        }

        private float localRotation;
        public float LocalRotation {
            get => localRotation;
            set {
                localRotation = value;
            }
        }


        private Transform parent;
        public Transform Parent {
            get => parent;
            set {
                if(parent != null) {
                    parent.childs.Remove(this);
                }

                parent = value;

                if(parent != null) {
                    parent.childs.Add(this);
                }

                this.SetNeedsPositionUpdate();
            }
        }

        public bool GizmosEnabled {
            get; set;
        }
        
        private List<Transform> childs;

        private bool needsPositionUpdate;
        
        public Transform(Vector2 position, Vector2 scale) {

            this.childs = new List<Transform>();

            this.Position = position;
            this.Scale = scale;

            this.needsPositionUpdate = true;

        }
        
        public Transform(Vector2 position) : this(position, Vector2.One) {

        }

        public void Update() {
            // TODO: Update local and global pos
            if (needsPositionUpdate) UpdatePosition();

            if(GizmosEnabled) {
                Gizmos.Instance.DrawGizmo(new CrossGizmo(this.position, 10, 1, Color.Red));
            }
        }
        
        private void SetNeedsPositionUpdate() {
            this.needsPositionUpdate = true;
            childs.ForEach(child => child.SetNeedsPositionUpdate());
        }

        private void UpdatePosition() {
            if(Parent != null) {
                this.position = Parent.Position + this.LocalPosition;
                this.scale = this.LocalScale * Parent.Scale;
                this.rotation = this.LocalRotation * Parent.Rotation;
            }
            this.needsPositionUpdate = false;
        }

    }
}
