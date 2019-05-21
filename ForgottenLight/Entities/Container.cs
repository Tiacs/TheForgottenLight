/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Items;
using ForgottenLight.Levels;
using ForgottenLight.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Entities {
    abstract class Container : Entity, IInteractable {

        public BoxCollider Collider {
            get; protected set;
        }

        public virtual bool Collidable => false;

        public virtual string Description => "press e to open";

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
            if(entity is Player) {
                Player player = (Player)entity;

                if (Item != null) {
                    string[] descriptions = Item.Description.Split('\n'); // Split item description at \n and put them into multiple messages

                    Level.Hud.DialogBox.Enqueue(new UI.DialogMessage(string.Format("You found {0}!\n{1}", Item.Name, descriptions[0]), false));
                    for(int i = 1; i < descriptions.Length; i++) {
                        Level.Hud.DialogBox.Enqueue(new UI.DialogMessage(descriptions[i]));
                    }

                    player.Inventory.AddItem(Item);
                    if(Item.Colectable) this.Item = null; // Remove item from container if collectable
                } else {
                    Level.Hud.DialogBox.Enqueue(new UI.DialogMessage("This storage is empty!", true));
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
