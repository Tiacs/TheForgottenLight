using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Levels;
using ForgottenLight.Pathfinding;

namespace ForgottenLight.Entities.Ghosts {
    class HunterGhost : Ghost {
        
        public HunterGhost(Vector2 position, ContentManager contentManager, Scene level) : base(position, contentManager, level) {

        }


        public HunterGhost(float x, float y, ContentManager contentManager, Scene level) : this(new Vector2(x, y), contentManager, level) {

        }

        protected override void LoadContent(ContentManager contentManager) {
            Texture2D atlas = contentManager.Load<Texture2D>("sprites/sprite_atlas");

            this.idleAnimation = new Animation(atlas, 25, 22, new Vector2(44, 0), 2, 0.5f, true);

            this.animationPlayer = new AnimationPlayer(new Vector2(0.5f, 1));
            this.animationPlayer.PlayAnimation(idleAnimation);
        }

        private Vector2 oldPosition;

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);

            if(Scene.Player.Transform.AbsolutePosition != oldPosition) {
                waypoints.Clear();
                waypoints.Enqueue(new Waypoint(Scene.Player.Transform.AbsolutePosition));
                oldPosition = Scene.Player.Transform.AbsolutePosition;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);

        }

        public override void OnCollision(ICollidable collidingEntity) {

        }
    }
}
