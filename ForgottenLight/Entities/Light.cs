﻿/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;

namespace ForgottenLight.Entities {
    class Light : Entity {

        private Animation texture;
        private AnimationPlayer animationPlayer;

        public Light(Vector2 position, ContentManager content, float radius, Scene level) : base(position, level) {
            Transform.Scale = radius * Vector2.One;
            this.texture = new Animation(content.Load<Texture2D>(Strings.CONTENT_SPRITE_LIGHT), 64, 64, Vector2.Zero, 1, 0, false);
            this.animationPlayer = new AnimationPlayer(new Vector2(.5f, .5f));
            this.animationPlayer.PlayAnimation(texture);
            Transform.GizmosEnabled = true;
        }

        public Light(float x, float y, ContentManager content, float radius, Scene level) : this(new Vector2(x, y), content, radius, level) {
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            animationPlayer.Draw(spriteBatch, gameTime, Transform.AbsolutePosition, Transform.AbsoluteScale);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
        }
    }
}
