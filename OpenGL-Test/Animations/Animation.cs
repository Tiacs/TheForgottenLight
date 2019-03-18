using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace OpenGL_Test.Animations
{
    class Animation
    {
        private Texture2D texture;
        public Texture2D Texture
        {
            get => this.texture;
        }

        private int frameHeight;
        public int FrameHeight
        {
            get => this.frameHeight;
        }

        public int FrameWidth
        {
            get => this.texture.Width;
        }

        public int FrameCount
        {
            get => this.texture.Height / this.FrameHeight;
        }

        private float frameTime;
        public float FrameTime
        {
            get => this.frameTime;
        }

        private bool isLooping;
        public bool IsLooping
        {
            get => this.isLooping;
        }

        public Animation(Texture2D texture, int frameHeight, float frameTime, bool isLooping)
        {
            this.texture = texture;
            this.frameHeight = frameHeight;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }
    }
}
