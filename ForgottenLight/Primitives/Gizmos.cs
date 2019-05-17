using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForgottenLight.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.Primitives {
    class Gizmos {

        private bool gizmosEnabled = false;

        private static Gizmos instance;
        public static Gizmos Instance {
            get {
                if(instance == null) {
                    instance = new Gizmos();
                }
                return instance;
            }
        }

        public static void ClearInstance() {
            instance = null;
        }

        public Texture2D PlainColor {
            get; private set;
        }

        private Queue<Gizmo> list;
        
        private Gizmos() {
            list = new Queue<Gizmo>();
            Input.Instance.RegisterOnKeyDownEvent(Microsoft.Xna.Framework.Input.Keys.F1, new Input.KeyboardEvent(ToggleGizmosPressed));
        }

        private void ToggleGizmosPressed() {
            this.gizmosEnabled = !gizmosEnabled;
        }

        public void LoadColor(SpriteBatch spriteBatch) {
            Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
            this.PlainColor = t;
        }

        public void DrawGizmo(Gizmo gizmo) {
            if (!gizmosEnabled) {
                return;
            }
            this.list.Enqueue(gizmo);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if(!gizmosEnabled) {
                return;
            }
            if (PlainColor == null) LoadColor(spriteBatch);

            while(list.Count > 0) {
                list.Dequeue().Draw(spriteBatch);
            }
        }

    }
}
