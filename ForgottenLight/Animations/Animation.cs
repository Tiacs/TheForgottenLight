/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ForgottenLight.Animations {
    class Animation {

        public Texture2D Texture {
            get; private set;
        }

        public int FrameHeight {
            get; private set;
        }

        public int FrameWidth {
            get; private set;
        }

        public Vector2 SpriteOrigin {
            get; private set;
        }

        public int FrameCount {
            get; private set;
        }

        public float FrameTime {
            get; private set;
        }

        public bool IsLooping {
            get; private set;
        }

        public Animation(Texture2D texture, int frameHeight, int frameWidth, Vector2 spriteOrigin, int frameCount, float frameTime, bool isLooping) {
            this.Texture = texture;
            this.FrameHeight = frameHeight;
            this.FrameWidth = frameWidth;
            this.SpriteOrigin = spriteOrigin;
            this.FrameCount = frameCount;
            this.FrameTime = frameTime;
            this.IsLooping = isLooping;
        }

        public Animation(Texture2D texture, int frameHeight, float frameTime, bool isLooping) : this(texture, frameHeight, texture.Width, Vector2.Zero, texture.Height / frameHeight, frameTime, isLooping) {

        }
    }
}
