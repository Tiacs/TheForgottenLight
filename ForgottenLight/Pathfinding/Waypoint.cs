using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
