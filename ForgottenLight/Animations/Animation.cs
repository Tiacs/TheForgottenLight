using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ForgottenLight.Animations {
    class Animation {
        private Texture2D texture;
        public Texture2D Texture => this.texture;

        private int frameHeight;
        public int FrameHeight => this.frameHeight;

        private int frameWidth;
        public int FrameWidth => this.frameWidth;

        private Vector2 spriteOrigin;
        public Vector2 SpriteOrigin => this.spriteOrigin;

        private int frameCount;
        public int FrameCount => this.frameCount;

        private float frameTime;
        public float FrameTime => this.frameTime;

        private bool isLooping;
        public bool IsLooping => this.isLooping;

        public Animation(Texture2D texture, int frameHeight, int frameWidth, Vector2 spriteOrigin, int frameCount, float frameTime, bool isLooping) {
            this.texture = texture;
            this.frameHeight = frameHeight;
            this.frameWidth = frameWidth;
            this.spriteOrigin = spriteOrigin;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }

        public Animation(Texture2D texture, int frameHeight, float frameTime, bool isLooping) : this(texture, frameHeight, texture.Width, Vector2.Zero, texture.Height / frameHeight, frameTime, isLooping) {

        }
    }
}
