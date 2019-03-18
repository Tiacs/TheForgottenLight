using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using OpenGL_Test.Animations;

namespace OpenGL_Test
{
    class Player
    {
        private Vector2 Position
        {
            get;set;
        }
        
        private AnimationPlayer animationPlayer;

        private Animation idleAnimation;
        private Animation attackAnimation;

        private float speed = 100.0f;

        private bool attackCooldown;
    
        public Player(Vector2 position, ContentManager content)
        {
            this.Position = position;

            this.LoadContent(content);
        }

        public bool Flipped
        {
            get;set;
        }

        public Player(float x, float y, ContentManager content) : this(new Vector2(x, y), content)
        {
        }

        private void LoadContent(ContentManager content)
        {
            this.idleAnimation = new Animation(content.Load<Texture2D>("player"), 32, 0.1f, true);
            this.attackAnimation = new Animation(content.Load<Texture2D>("player_attack"), 32, 0.1f, false);

            this.animationPlayer = new AnimationPlayer();

            this.animationPlayer.PlayAnimation(this.idleAnimation);
        }

        public void Udate(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
        {
            if(keyboardState.IsKeyDown(Keys.D))
            {
                this.Position += Vector2.UnitX * speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                this.Flipped = false;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Position += -Vector2.UnitX * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.Flipped = true;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.Position += Vector2.UnitY * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.Position += -Vector2.UnitY * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }


            if (keyboardState.IsKeyDown(Keys.Space) && this.animationPlayer.IsAnimationDone && !this.attackCooldown)
            {
                this.animationPlayer.PlayAnimation(attackAnimation);
                this.attackCooldown = true;
            }

            if(keyboardState.IsKeyUp(Keys.Space))
            {
                this.attackCooldown = false;
            }

            if (this.animationPlayer.Animation == this.attackAnimation && this.animationPlayer.IsAnimationDone)
            {
                this.animationPlayer.PlayAnimation(idleAnimation);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.animationPlayer.Draw(spriteBatch, gameTime, this.Position, this.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }
    }
}
