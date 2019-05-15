using System;
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
    class Table : Container {

        private Animation idleAnimation;
        private AnimationPlayer animationPlayer;

        public override string Description => "Press E to search";
        public override bool Collidable => true;

        public Table(Vector2 position, ContentManager content, Scene level) : base(position, Vector2.One, level) {
            this.Collider = new BoxCollider(64, 25, new Vector2(.5f, 1), Transform, Level);
            LoadContent(content);
            Transform.GizmosEnabled = true;
        }

        public Table(float x, float y, ContentManager content, Scene level) : this(new Vector2(x, y), content, level) {

        }

        private void LoadContent(ContentManager content) {
            Texture2D atlas = content.Load<Texture2D>("sprites/sprite_atlas");

            idleAnimation = new Animation(atlas, 25, 64, new Vector2(32, 63), 1, 0, false);
            animationPlayer = new AnimationPlayer(new Vector2(.5f, 1));

            animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            this.animationPlayer.Draw(spriteBatch, gameTime, Transform.AbsolutePosition, Transform.AbsoluteScale);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
            Collider.Update(gameTime);
        }
    }
}
