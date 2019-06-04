/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Items;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;

namespace ForgottenLight.Entities {
    class Door : Entity, IInteractable {

        private AnimationPlayer animationPlayer;
        private Animation idleAnimation;

        public string Description { get; private set; } = "Press E to open door";

        public BoxCollider Collider { get; private set; }

        public bool Collidable => false;

        public Level Level => (Level)Scene;

        private bool Opened {
            get; set;
        }

        public Door(Vector2 position, ContentManager content, Level level) : base(position, Vector2.One * .75f, level, 1) {

            this.Collider = new BoxCollider(39, 63/2, new Vector2(.5f, 0), Transform, level);
            LoadContent(content);

            Transform.GizmosEnabled = true;
        }

        public Door(float x, float y, ContentManager content, Level level) : this(new Vector2(x, y), content, level) {
        }

        private void LoadContent(ContentManager content) {

            Texture2D atlas = content.Load<Texture2D>("sprites/sprite_atlas");

            idleAnimation = new Animation(atlas, 63, 39, new Vector2(66, 0), 1, 0, false);

            animationPlayer = new AnimationPlayer(new Vector2(0.5f, 0));
            animationPlayer.PlayAnimation(idleAnimation);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
            this.Collider.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            animationPlayer.Draw(spriteBatch, gameTime, Transform.AbsolutePosition, Transform.AbsoluteScale);
        }

        public void OnInteract(Entity entity) {
            if(!(entity is Player)) {
                return;
            }
            
            Player player = (Player)entity;
            if(Opened) {
                Scene.NextScene();

                return;
            }

            if (player.Inventory.ContainsItem(ItemCode.KEY)) {
                Level.Hud.DialogBox.Enqueue(new UI.DialogMessage("You opened the door!", false));
                Level.Hud.DialogBox.Enqueue(new UI.DialogMessage("Let's see what's inside!", false));
                Opened = true;

                this.Description = "Press E to get into next level!";

                return;
            }

            Level.Hud.DialogBox.Enqueue(new UI.DialogMessage("You must find the key first!", true));
        }

        public void OnCollision(ICollidable collidingEntity) {
        }
    }
}
