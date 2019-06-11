/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Items;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;

namespace ForgottenLight.Entities {
    abstract class Container : Entity, IInteractable {

        public BoxCollider Collider {
            get; protected set;
        }

        public virtual bool Collidable => false;

        public virtual string Description => Strings.UI_MESSAGE_OPEN;

        private Level Level => (Level)Scene;

        public Item Item {
            get; set;
        } 
        
        public Container(Vector2 position, Vector2 scale, Scene level) : base(position, scale, level, 1) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
        }

        public void OnInteract(Entity entity) {
            if (entity is Player player) {
                if (Item != null) {
                    string[] descriptions = Item.Description.Split('\n'); // Split item description at \n and put them into multiple messages

                    Level.Hud.DialogBox.Enqueue(new UI.DialogMessage(string.Format(Strings.UI_FOUND_MESSAGE, Item.Name, descriptions[0]), false));
                    for (int i = 1; i < descriptions.Length; i++) {
                        Level.Hud.DialogBox.Enqueue(new UI.DialogMessage(descriptions[i]));
                    }

                    player.Inventory.AddItem(Item);
                    if (Item.Colectable) this.Item = null; // Remove item from container if collectable
                } else {
                    Level.Hud.DialogBox.Enqueue(new UI.DialogMessage(Strings.UI_EMPTY_MESSAGE, true));
                }
            }
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
        }

        public void OnCollision(ICollidable collidingEntity) {
        }
    }
}
