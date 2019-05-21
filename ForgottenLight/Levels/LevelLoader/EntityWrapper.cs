/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

namespace ForgottenLight.Levels.LevelLoader {
    class EntityWrapper {

        public Type EntityType {
            get; set;
        } = Type.NONE;

        public float X {
            get;set;
        }

        public float Y {
            get;set;
        }

        public float Width {
            get;set;
        }

        public float Height {
            get;set;
        }

        public WaypointWrapper[] Path {
            get; set;
        } = new WaypointWrapper[0];

        public int ItemIndex {
            get; set;
        } = -1;

        public bool RandomFill {
            get; set;
        } = false;

        public enum Type {
            NONE, NORMAL_GHOST, WALLWALKER_GHOST, HUNTER_GHOST, CUPBOARD, TABLE, DOOR, WALL, BOOKSHELF
        }

    }
}
