using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Entities;
using OpenGL_Test.Pathfinding;
using OpenGL_Test.Primitives;

namespace OpenGL_Test.Levels {
    class Level_Test : Level {

        private Texture2D levelBackground;

        private Pathfinder pathfinder;

        public Level_Test(ContentManager contentManager) : base(contentManager, 800, 480) {

        }

        protected override void LoadContent(ContentManager contentManager) {
            this.levelBackground = contentManager.Load<Texture2D>("level_test");
        }

        public override void Initialize() {
            base.Initialize();

            this.pathfinder = new Pathfinder(4*16,  4*9, this);

            this.Player = new Player(100, 100, contentManager, this);
            this.Ghost = new Ghost(250, 100, contentManager, pathfinder, this);

            this.Entities.Add(Player);
            this.Entities.Add(Ghost);
        }

        protected override void CreateWalls() {
            base.CreateWalls();

            // outter walls
            Entities.Add(new Wall(800, 25, 0, 0, this)); // top
            Entities.Add(new Wall(25, 480, 0, 0, this)); // left
            Entities.Add(new Wall(25, 480, 775, 0, this)); // right
            Entities.Add(new Wall(800, 25, 0, 455, this)); // bottom

            // first room
            Entities.Add(new Wall(32, 96, 192, 0, this)); // upper
            Entities.Add(new Wall(32, 320, 192, 160, this)); // lower

            // second room
            Entities.Add(new Wall(32, 272, 320, 0, this)); // left
            Entities.Add(new Wall(128, 32, 320, 240, this));
            Entities.Add(new Wall(224, 32, 576, 239, this)); // right
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            pathfinder.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            spriteBatch.Draw(levelBackground, Vector2.Zero, Color.White);

            base.Draw(spriteBatch, gameTime);
        }

    }
}
