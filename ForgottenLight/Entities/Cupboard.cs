﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Animations;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Entities {

    class Cupboard : Container {

        private Animation idleAnimation;
        private AnimationPlayer animationPlayer;

        public override string Description => "press e to open";

        public Cupboard(Vector2 position, ContentManager content, Level level) : base(position, Vector2.One, level) {

            this.Collider = new BoxCollider(32, 42, new Vector2(.5f, 1), Transform, Level);
            LoadContent(content);
        }

        public Cupboard(float x, float y, ContentManager content, Level level) : this(new Vector2(x, y), content, level) {
            
        }

        private void LoadContent(ContentManager content) {
            Texture2D atlas = content.Load<Texture2D>("sprite_atlas");

            idleAnimation = new Animation(atlas, 42, 32, new Vector2(365, 0), 1, 0, false);
            animationPlayer = new AnimationPlayer(new Vector2(.5f, 1));

            animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.animationPlayer.Draw(spriteBatch, gameTime, Transform.Position);
        }
        
        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
            Collider.Update(gameTime);
        }
        
    }
}