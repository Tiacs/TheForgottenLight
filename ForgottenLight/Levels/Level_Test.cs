/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Entities;
using ForgottenLight.Entities.Ghosts;
using ForgottenLight.Items;

namespace ForgottenLight.Levels {
    class Level_Test : Level {

        private Door door;

        public Level_Test() : base(800, 480) {

        }

        protected override void LoadContent(ContentManager contentManager) {
            base.LoadContent(contentManager);
        }

        public override void Initialize(ContentManager contentManager, Game1 game) {
            base.Initialize(contentManager, game);


            Item item = new Item {
                Name = "Key",
                Description = "This Key could be useful for something.",
                ID = ItemCode.KEY
            };
            
            this.Player = new Player(100, 100, contentManager, this);
            
            this.door = new Door(275, 10, contentManager, this);

            Cupboard cupboard = new Cupboard(700, 100, contentManager, this);
            cupboard.Item = item;
            this.Entities.Add(cupboard);


            this.Entities.Add(door);
            this.Entities.Add(Player);

            this.Entities.Add(new NormalGhost(250, 100, contentManager, this));
            this.Entities.Add(new NormalGhost(400, 100, contentManager, this));
            this.Entities.Add(new NormalGhost(480, 100, contentManager, this));
            this.Entities.Add(new NormalGhost(400, 200, contentManager, this));
            //this.Entities.Add(new Ghost(400, 350, contentManager, this));
            //this.Entities.Add(new Ghost(400, 150, contentManager, this));
            //this.Entities.Add(new Ghost(250, 200, contentManager, this));
            //this.Entities.Add(new Ghost(250, 250, contentManager, this));
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
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
        }

    }
}
