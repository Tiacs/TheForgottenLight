/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

 using System.Collections.Generic;

namespace ForgottenLight.Items {
    class Inventory {

        private List<Item> items;

        public int Count => items.Count;

        public Inventory() {
            this.items = new List<Item>();
        }

        public void AddItem(Item item) {
            this.items.Add(item);
        }

        public bool RemoveItem(Item item) {
            return items.Remove(item);
        }

        public Item GetItem(int index) {
            return this.items[index];
        }

        public bool ContainsItem(ItemCode itemCode) {
            foreach(Item item in items) {
                if(item.ID == itemCode) {
                    return true;
                }
            }
            return false;
        }

    }
}
