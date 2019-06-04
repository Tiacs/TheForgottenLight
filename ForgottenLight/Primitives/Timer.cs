/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;

namespace ForgottenLight.Primitives {
    class Timer {

        private int current;
        private int time;
        
        public bool Done {
            get => current >= time;
        }

        public Timer(int time) {
            this.time = time;
        }

        public void Update(GameTime time) {
            if(!Done) {
                current += time.ElapsedGameTime.Milliseconds;
            }
        }
        
        public void Restart() {
            this.current = 0;
        }

    }
}
