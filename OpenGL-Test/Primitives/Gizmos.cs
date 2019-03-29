using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenGL_Test.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Test.Primitives {
    class Gizmos {

        private bool gizmosEnabled = true;

        private static Gizmos instance;
        public static Gizmos Instance {
            get {
                if(instance == null) {
                    instance = new Gizmos();
                }
                return instance;
            }
        }

        public Texture2D PlainColor {
            get; private set;
        }

        private List<Gizmo> list;
        
        private Gizmos() {
            list = new List<Gizmo>();
            Input.Instance.RegisterOnKeyDownEvent(Microsoft.Xna.Framework.Input.Keys.F1, new Input.KeyboardEvent(toggleGizmosPressed));
        }

        private void toggleGizmosPressed() {
            this.gizmosEnabled = !gizmosEnabled;
        }

        public void LoadColor(SpriteBatch spriteBatch) {
            Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
            this.PlainColor = t;
        }

        public void DrawGizmo(Gizmo gizmo) {
            this.list.Add(gizmo);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if(!gizmosEnabled) {
                return;
            }
            if (PlainColor == null) LoadColor(spriteBatch);
            list.ForEach(gizmo => gizmo.Draw(spriteBatch));
            list.Clear();
        }

    }
}
