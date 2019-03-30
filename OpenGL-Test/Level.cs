using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenGL_Test.Entities;
using OpenGL_Test.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Test {
    class Level {

        private ContentManager contentManager;

        private Texture2D levelBackground;

        public List<BoxCollider> Walls {
            get; private set;
        }

        public Player Player {
            get; private set;
        }

        public Ghost Ghost {
            get; private set;
        }
        
        public Level(ContentManager contentManager) {
            this.contentManager = contentManager;
            this.LoadContent(contentManager);
        }

        private void LoadContent(ContentManager contentManager) {
            this.levelBackground = contentManager.Load<Texture2D>("level_test");
        }

        public void Initialize() {
            this.Player = new Player(100, 100, contentManager, this);
            this.Ghost = new Ghost(300, 100, contentManager, this);

            this.CreateWalls();
        }

        private void CreateWalls() {
            this.Walls = new List<BoxCollider>();

            // outter walls
            Walls.Add(new BoxCollider(800, 25, Vector2.Zero, new Transform(Vector2.Zero), this)); // top
            Walls.Add(new BoxCollider(25, 480, Vector2.Zero, new Transform(Vector2.Zero), this)); // left
            Walls.Add(new BoxCollider(25, 480, Vector2.Zero, new Transform(new Vector2(775, 0)), this)); // right
            Walls.Add(new BoxCollider(800, 25, Vector2.Zero, new Transform(new Vector2(0, 455)), this)); // bottom

            // first room
            Walls.Add(new BoxCollider(32, 96, Vector2.Zero, new Transform(new Vector2(192, 0)), this)); // upper
            Walls.Add(new BoxCollider(32, 320, Vector2.Zero, new Transform(new Vector2(192, 160)), this)); // upper

            // second room
            Walls.Add(new BoxCollider(32, 272, Vector2.Zero, new Transform(new Vector2(320, 0)), this)); // left
            Walls.Add(new BoxCollider(128, 32, Vector2.Zero, new Transform(new Vector2(320, 240)), this));
            Walls.Add(new BoxCollider(224, 32, Vector2.Zero, new Transform(new Vector2(576, 239)), this)); // right
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            this.Walls.ForEach(wall => wall.Update(gameTime));

            this.Player.Update(gameTime, keyboardState, mouseState);
            this.Ghost.Update(gameTime, keyboardState, mouseState);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            spriteBatch.Draw(levelBackground, Vector2.Zero, Color.White);
            this.Player.Draw(spriteBatch, gameTime);
            this.Ghost.Draw(spriteBatch, gameTime);
        }

    }
}
