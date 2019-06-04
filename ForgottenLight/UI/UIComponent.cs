/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Levels;
using ForgottenLight.Primitives;

namespace ForgottenLight.UI {

    abstract class UIComponent {

        public Scene Scene {
            get; private set;
        }
        
        private bool visible = true;
        public bool Visible {
            get => Parent != null ? visible & Parent.Visible : visible;
            set => visible = value;
        }

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

        public Vector2 Pivot {
            get; set;
        } = Vector2.Zero;


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
        
        private MouseState prevMouseState;

        public UIComponent(Scene scene) : this(new Transform(Vector2.Zero), scene) {
            
        }
        
        public UIComponent(Transform transform, Scene scene) {
            this.Transform = transform;
            this.Scene = scene;

            this.Childs = new List<UIComponent>();
        }

        public UIComponent(Vector2 position, Vector2 scale, Scene scene) : this(new Transform(Vector2.Zero, Vector2.One) { Position = position, Scale = scale }, scene) {

        }

        public UIComponent(Vector2 position, Scene scene) : this(position, Vector2.One, scene) {

        }

        public abstract void OnDraw(SpriteBatch sprite, GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            if(Visible) { // Only draw if visible
                this.OnDraw(spriteBatch, gameTime);
                
                Gizmos.Instance.DrawGizmo(new BoxGizmo(AbsolutePosition, Width, Height, 1, Color.Gray));
                Gizmos.Instance.DrawGizmo(new CrossGizmo(AbsolutePosition, 5, 1, Color.Yellow));
            }

            Childs.ForEach(child => child.Draw(spriteBatch, gameTime));
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            Transform.Update();
            CheckEvents(gameTime, keyboardState, mouseState);

            Childs.ForEach(child => child.Update(gameTime, keyboardState, mouseState));
        }

        private void CheckEvents(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {

            if(InsideBounds(mouseState.Position)) {

                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton != prevMouseState.LeftButton) {
                    this.OnClick();
                }


                if (!InsideBounds(prevMouseState.Position)) {
                    this.OnMouseEnter();
                }

            } else {
                if (InsideBounds(prevMouseState.Position)) {
                    this.OnMouseLeave();
                }
            }

            prevMouseState = mouseState;

        }

        private bool InsideBounds(Point position) {
            Rectangle rect = new Rectangle(this.AbsolutePosition.ToPoint(), this.Bounds.ToPoint());
            return rect.Contains(Scene.ScreenToGamePosition(position.ToVector2()));
        }

        protected virtual void OnMouseEnter() {

        }

        protected virtual void OnMouseLeave() {

        }

        protected virtual void OnClick() {

        }
        
    }
}
