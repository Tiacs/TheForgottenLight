/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

namespace ForgottenLight.Entities {
    interface IInteractable : ICollidable {

        string Description {
            get;
        }
        void OnInteract(Entity entity);

    }
}
