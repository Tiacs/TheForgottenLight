/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

namespace ForgottenLight.Items {
    class Item {

        public string Name {
            get; set;
        }
        
        public string Description {
            get; set;
        }

        public ItemCode ID {
            get; set;
        } = ItemCode.NONE;

        public bool RandomLoot {
            get; set;
        } = true;

        public int CountLimit {
            get; set;
        } = -1;

        public bool Colectable {
            get; set;
        } = true;
    }

    public enum ItemCode {
        NONE, KEY
    }
}
