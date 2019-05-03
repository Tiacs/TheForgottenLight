using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Entities;
using ForgottenLight.Primitives;
using ForgottenLight.Levels;
using ForgottenLight.UI;
using ForgottenLight.Events;

namespace ForgottenLight.Levels {
    abstract class Level {

        protected ContentManager contentManager;

        public List<BoxCollider> Walls {
            get; protected set;
        }

        public List<Entity> Entities {
            get; protected set;
        }

        public List<Light> Lights {
            get; protected set;
        }

        public Player Player {
            get; protected set;
        }

        public Ghost Ghost {
            get; protected set;
        }

        public Door Door {
            get; protected set;
        }

        public float Width {
            get; protected set;
        }

        public float Height {
            get; protected set;
        }

        public HUD Interface {
            get; protected set;
        }
        
        public Level(ContentManager contentManager, float width, float height) {
            this.Width = width;
            this.Height = height;
            
            this.contentManager = contentManager;
            this.LoadContent(contentManager);

            this.Entities = new List<Entity>();
            this.Walls = new List<BoxCollider>();
            this.Lights = new List<Light>();
        }

        protected abstract void LoadContent(ContentManager contentManager);
        
        public virtual void Initialize() {
            this.CreateWalls();
        }

        protected virtual void CreateWalls() {
            
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            Input.Instance.Update();

            if(!Interface.DialogBox.IsDialogRunning) {
                this.Walls.ForEach(wall => wall.Update(gameTime));
                this.Entities.ForEach(entity => entity.Update(gameTime, keyboardState, mouseState));
                this.Lights.ForEach(light => light.Update(gameTime, keyboardState, mouseState));
            }
            this.Interface.Update(gameTime, keyboardState, mouseState);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Entities.ForEach(entity => entity.Draw(spriteBatch, gameTime));
        }

        public void DrawLights(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Lights.ForEach(light => light.Draw(spriteBatch, gameTime));
        }

        public void DrawUI(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Interface.Draw(spriteBatch, gameTime);
        }
    }
}
