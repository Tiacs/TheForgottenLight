/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

namespace ForgottenLight.Levels.LevelLoader {
    class LevelWrapper {

        public string Name {
            get; set;
        } 

        public int Version {
            get; set;
        }

        public string NextLevel {
            get; set;
        }

        public string[] OpeningDialog {
            get; set;
        } = new string[0];
        
        public PlayerWrapper Player {
            get; set;
        }

        public EntityWrapper[] Entities {
            get; set;
        }

        public ItemWrapper[] Items {
            get; set;
        }

        public LightWrapper[] Lights {
            get; set;
        }

    }
}
