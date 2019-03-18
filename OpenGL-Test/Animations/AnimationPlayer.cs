using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenGL_Test.Animations
{
    class AnimationPlayer
    {
        private Animation animation;
        public Animation Animation
        {
            get => this.animation;
        }

        private int currentAnimationIndex;
        public int CurrentAnimationIndex
        {
            get => currentAnimationIndex;
        }

        private float time;

        public Vector2 Origin
        {
            get => new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight);
        }

        public bool IsAnimationDone
        {
            get => this.currentAnimationIndex >= this.Animation.FrameCount - 1;
        }

        public void PlayAnimation(Animation animation, int animationOffset = 0)
        {
            this.animation = animation;
            this.currentAnimationIndex = animationOffset;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            this.time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            while(time > Animation.FrameTime)
            {
                time -= Animation.FrameTime;

                if(Animation.IsLooping)
                {
                    this.currentAnimationIndex = (this.currentAnimationIndex + 1) % Animation.FrameCount;
                }
                else
                {
                    this.currentAnimationIndex = Math.Min(this.currentAnimationIndex + 1, Animation.FrameCount - 1);
                }
            }

            Rectangle sourceFrame = new Rectangle(0, currentAnimationIndex * Animation.FrameHeight, Animation.FrameWidth, Animation.FrameHeight);
            spriteBatch.Draw(Animation.Texture, position, sourceFrame, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
        }
    }
}
