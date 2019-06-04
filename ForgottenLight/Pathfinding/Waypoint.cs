/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;

namespace ForgottenLight.Pathfinding {
    class Waypoint {

        public Vector2 Position {
            get; set;
        }

        public Waypoint(Vector2 position) {
            this.Position = position;
        }

        public Waypoint(float x, float y) : this(new Vector2(x, y)) {

        }
        
    }
}
