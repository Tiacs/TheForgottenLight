using ForgottenLight.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.UI {

    abstract class UIComponent {

        public Transform Transform {
            get; set;
        }

        public Vector2 Position {
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public Vector2 AbsolutePosition {
            get {
                if (Parent != null) { // If componente has parent, take parent pivot into consideration
                    return Transform.AbsolutePosition - Pivot * Bounds - Parent.Pivot * Parent.Bounds;
                }
                return Transform.AbsolutePosition - Pivot * Bounds;
            }
        }

        public Vector2 Scale {
            get => Transform.Scale;
            set => Transform.Scale = value;
        }

        protected virtual Vector2 Bounds {
            get; set;
        }

        public virtual float Width {
            get => Bounds.X;
            set => Bounds = Vector2.UnitY * Bounds + Vector2.UnitX * value;
        }

        public virtual float Height {
            get => Bounds.Y;
            set => Bounds = Vector2.UnitX * Bounds + Vector2.UnitY * value;
        }

        public Vector2 Pivot {
            get; set;
        } = Vector2.Zero;

        private UIComponent parent;
        public UIComponent Parent {
            get {
                return parent;
            }
            set {
                if(parent != null) { // chick if element already has a parent, if so remove this element from parent
                    parent.Childs.Remove(this);
                }

                this.parent = value;

                if(parent != null) {
                    this.parent.Childs.Add(this);
                }

                // Update Transform
                Transform.Parent = value.Transform;
            }
        }

        public List<UIComponent> Childs {
            get; private set;
        }

        public UIComponent() {
            this.Transform = new Transform(Vector2.Zero);

            this.Childs = new List<UIComponent>();
        }
        
        public UIComponent(Transform transform) {
            this.Transform = transform;

            this.Childs = new List<UIComponent>();
        }

        public UIComponent(Vector2 position, Vector2 scale) : this(new Transform(Vector2.Zero, Vector2.One) { Position = position, Scale = scale }) {

        }

        public UIComponent(Vector2 position) : this(position, Vector2.One) {

        }
        
        public virtual void Draw(SpriteBatch spriteBatch) {
            Childs.ForEach(child => child.Draw(spriteBatch));
            Gizmos.Instance.DrawGizmo(new BoxGizmo(AbsolutePosition, Width, Height, 1, Color.Gray));
            Gizmos.Instance.DrawGizmo(new CrossGizmo(AbsolutePosition, 5, 1, Color.Yellow));
        }

        private ButtonState prevMouseLeftButton;

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            Transform.Update();
            Childs.ForEach(child => child.Update(gameTime, keyboardState, mouseState));
            CheckEvents(gameTime, keyboardState, mouseState);
        }

        private void CheckEvents(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            if (mouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton != prevMouseLeftButton) {
                this.OnClick();
            }
            prevMouseLeftButton = mouseState.LeftButton;

        }

        public virtual void OnClick() {

        }
        
    }
}
