/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenLight.Animations {
    class AnimationPlayer {
        
        private Animation animation;
        public Animation Animation => this.animation;

        private int currentAnimationIndex;
        public int CurrentAnimationIndex => currentAnimationIndex;

        public Vector2 Origin => new Vector2(Animation.FrameWidth, Animation.FrameHeight) * Pivot;

        public Vector2 Pivot {
            get; set;
        }

        public bool IsAnimationDone => this.currentAnimationIndex >= this.Animation.FrameCount - 1;

        private float time;

        public AnimationPlayer(Vector2 pivot) {
            this.Pivot = pivot;
        }

        public void PlayAnimation(Animation animation, int animationOffset = 0) {
            this.animation = animation;
            this.currentAnimationIndex = animationOffset;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position, Vector2 scale, SpriteEffects spriteEffects = SpriteEffects.None) {
            if(animation.FrameCount == 1) { // check if only one frame -> if so no current frame index calculation required
                this.DrawFrameAtIndex(spriteBatch, position, scale, spriteEffects, 0);
                return;
            }

            // get current frame
            this.time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (time > Animation.FrameTime) {
                time -= Animation.FrameTime;

                if (Animation.IsLooping) {
                    this.currentAnimationIndex = (this.currentAnimationIndex + 1) % Animation.FrameCount;
                } else {
                    this.currentAnimationIndex = Math.Min(this.currentAnimationIndex + 1, Animation.FrameCount - 1);
                }
            }
            
            this.DrawFrameAtIndex(spriteBatch, position, scale, spriteEffects, currentAnimationIndex); // draw current animation frame
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None) {
            this.Draw(spriteBatch, gameTime, position, Vector2.One, spriteEffects);
        }

        private void DrawFrameAtIndex(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, SpriteEffects spriteEffects, int animationIndex) {
            Point origin = new Point((int)Animation.SpriteOrigin.X, (int)Animation.SpriteOrigin.Y + animationIndex * Animation.FrameHeight);
            Point size = new Point(Animation.FrameWidth, Animation.FrameHeight);
            Rectangle sourceFrame = new Rectangle(origin, size);

            spriteBatch.Draw(Animation.Texture, position, sourceFrame, Color.White, 0.0f, Origin, scale, spriteEffects, 0.0f);
        }
    }
}
