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

        public bool Collidable => false;

        public virtual string Description => "press e to open";

        public Item Item {
            get; set;
        } 
        
        public Container(Vector2 position, Vector2 scale, Level level) : base(position, scale, level) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
        }

        public void OnInteract(Entity entity) {
            if(entity is Player) {
                Player player = (Player)entity;

                if (Item != null) {
                    Console.WriteLine("You found '{0}'! {1}", Item.Name, Item.Description);
                    Level.Interface.DialogBox.Enqueue(new UI.DialogMessage(string.Format("You found {0}! {1}", Item.Name, Item.Description), false));
                    player.Item = Item;
                    this.Item = null;
                } else {
                    Console.WriteLine("Storage is empty!");
                    Level.Interface.DialogBox.Enqueue(new UI.DialogMessage("This storage is empty!", true));
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
