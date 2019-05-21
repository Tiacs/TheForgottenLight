/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Entities {
    interface IInteractable : ICollidable {

        string Description { get; }
        void OnInteract(Entity entity);

    }
}
