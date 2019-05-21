/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace ForgottenLight.Primitives {

    class Transform {

        private Vector2 localPosition;
        public Vector2 Position {
            get => localPosition;
            set {
                localPosition = value;
                this.SetNeedsAbsoluteUpdate();
            }
        }

        private Vector2 localScale;
        public Vector2 Scale {
            get => localScale;
            set {
                localScale = value;
                this.SetNeedsAbsoluteUpdate();
            }
        }

        private float localRotation;
        public float Rotation {
            get => localRotation;
            set {
                localRotation = value;
                this.SetNeedsAbsoluteUpdate();
            }
        }


        private Vector2 absolutePosition;
        public Vector2 AbsolutePosition => GetAndUpdate(ref absolutePosition);

        private Vector2 absoluteScale;
        public Vector2 AbsoluteScale => GetAndUpdate(ref absoluteScale);

        private float absoluteRotation;
        public float AbsoluteRotation => GetAndUpdate(ref absoluteRotation);


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

                this.SetNeedsAbsoluteUpdate();
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
            if(GizmosEnabled) {
                Gizmos.Instance.DrawGizmo(new CrossGizmo(this.AbsolutePosition, 10, 1, Color.Red));
            }
        }
        
        private void SetNeedsAbsoluteUpdate() {
            this.needsPositionUpdate = true;
            childs.ForEach(child => child.SetNeedsAbsoluteUpdate());
        }

        private T GetAndUpdate<T>(ref T field) {
            if(needsPositionUpdate) {
                UpdateAbsolute();
            }
            return field;
        }

        private void UpdateAbsolute() {
            if(Parent != null) {
                this.absolutePosition = Parent.AbsolutePosition + this.localPosition;
                this.absoluteScale = Parent.AbsoluteScale * this.localScale;
                this.absoluteRotation = Parent.AbsoluteRotation * this.localRotation;
            } else {
                this.absolutePosition = localPosition;
                this.absoluteScale = localScale;
                this.absoluteRotation = localRotation;
            }
            this.needsPositionUpdate = false;
        }

    }
}
