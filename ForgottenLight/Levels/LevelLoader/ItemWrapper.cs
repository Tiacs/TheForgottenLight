using ForgottenLight.Items;

namespace ForgottenLight.Levels.LevelLoader {
    class ItemWrapper {

        public ItemCode ItemCode {
            get; set;
        } = ItemCode.NONE;

        public string Name {
            get; set;
        } = "";

        public string Description {
            get; set;
        } = "";

        public bool RandomLoot {
            get; set;
        } = true;

        public int CountLimit {
            get; set;
        } = -1;

        public bool Collectable {
            get; set;
        } = true;
    }
}
