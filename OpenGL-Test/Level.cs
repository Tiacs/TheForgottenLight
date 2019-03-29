using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenGL_Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Test {
    class Level {

        private ContentManager contentManager;

        public Player Player {
            get; private set;
        }

        public Ghost Ghost {
            get; private set;
        }
        
        public Level(ContentManager contentManager) {
            this.contentManager = contentManager;
        }

        public void Initialize() {
            this.Player = new Player(100, 100, contentManager, this);
            this.Ghost = new Ghost(300, 100, contentManager, this);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            this.Player.Update(gameTime, keyboardState, mouseState);
            this.Ghost.Update(gameTime, keyboardState, mouseState);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Player.Draw(spriteBatch, gameTime);
            this.Ghost.Draw(spriteBatch, gameTime);
        }

    }
}
