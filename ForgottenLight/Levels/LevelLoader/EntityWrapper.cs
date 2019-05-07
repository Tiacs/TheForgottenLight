
namespace ForgottenLight.Levels.LevelLoader {
    class EntityWrapper {

        public Type EntityType {
            get;set;
        }

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
            get;set;
        }

        public int ItemIndex {
            get; set;
        } = -1;

        public bool RandomFill {
            get; set;
        } = false;

        public enum Type {
            NORMAL_GHOST, CUPBOARD, DOOR, WALL
        }

    }
}
