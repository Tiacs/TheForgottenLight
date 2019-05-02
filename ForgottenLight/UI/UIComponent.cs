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
        
        public UIComponent(Transform transform) {
            this.Transform = transform;

            this.Childs = new List<UIComponent>();
        }

        public UIComponent(Vector2 position, Vector2 scale) : this(new Transform(Vector2.Zero, Vector2.One) { LocalPosition = position, LocalScale = scale }) {

        }

        public UIComponent(Vector2 position) : this(position, Vector2.One) {

        }
        
        public virtual void Draw(SpriteBatch spriteBatch) {
            Childs.ForEach(child => child.Draw(spriteBatch));
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
