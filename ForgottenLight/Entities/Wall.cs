using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Levels;
using ForgottenLight.Primitives;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ForgottenLight.Entities {
    class Wall : Entity, ICollidable {

        private static Texture2D color;

        public BoxCollider Collider {
            get; private set;
        }

        public int Width {
            get; private set;
        }

        public int Height {
            get; private set;
        }

        public bool Collidable => true;

        public Wall(int width, int height, Transform transform, Level level) : base(transform, level) {
            this.Width = width;
            this.Height = height;

            this.Collider = new BoxCollider(Width, Height, Vector2.Zero, Transform, Level);
        }

        public Wall(int width, int height, Vector2 position, Level level) : this(width, height, new Transform(position), level) {

        }

        public Wall(int width, int height, float x, float y, Level level) : this(width, height, new Transform(new Vector2(x,y)), level) {

        }
        

        private void LoadColor(SpriteBatch spriteBatch) {
            color = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            color.SetData<Color>(new Color[] { CustomColor.DarkBlue });// fill the texture with white
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            Collider.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

            if (color == null) LoadColor(spriteBatch);
            spriteBatch.Draw(color, Transform.Position, new Rectangle(0, 0, (int)Width, (int)Height), Color.White);

        }

        public void OnCollision(ICollidable collidingEntity) {
        }
    }
}
