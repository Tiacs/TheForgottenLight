/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using ForgottenLight.Primitives;

namespace ForgottenLight.Entities {
    interface ICollidable {
        BoxCollider Collider {
            get;
        }

        bool Collidable {
            get;
        }
        
        void OnCollision(ICollidable collidingEntity);
    }
}
