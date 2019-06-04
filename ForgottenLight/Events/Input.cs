/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.Events {
    class Input {

        static Input instance;
        public static Input Instance {
            get {
                if (instance == null) instance = new Input();
                return instance;
            }
        }

        public static void ClearInstance() {
            instance = null;
        }

        public delegate void KeyboardEvent();
        
        private Dictionary<Keys, KeyboardEvent> downEvents;
        private Dictionary<Keys, KeyboardEvent> upEvents;

        private KeyboardState prevState;

        private Input() {
            this.downEvents = new Dictionary<Keys, KeyboardEvent>();
            this.upEvents = new Dictionary<Keys, KeyboardEvent>();
            prevState = Keyboard.GetState();
        }

        public void RegisterOnKeyDownEvent(Keys key, KeyboardEvent ev) {
            if(this.downEvents.ContainsKey(key)) {
                this.downEvents[key] += ev;
                return;
            }
            this.downEvents[key] = ev;
        }

        public void RegisterOnKeyUpEvent(Keys key, KeyboardEvent ev) {
            if(this.upEvents.ContainsKey(key)) {
                this.upEvents[key] += ev;
                return;
            }
            this.upEvents[key] = ev;
        }

        public void RaiseEvent(Keys key) {
            if(!downEvents.ContainsKey(key)) {
                return;
            }
            downEvents[key].Invoke();
        }

        public void Update() {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach(Keys key in keyboardState.GetPressedKeys()) {
               if(prevState.IsKeyUp(key) && downEvents.ContainsKey(key)) {
                    downEvents[key].Invoke();
                }
            }
            foreach (Keys key in this.upEvents.Keys) {
                if (prevState.IsKeyDown(key) && keyboardState.IsKeyUp(key)) {
                    downEvents[key].Invoke();
                }
            }
            this.prevState = keyboardState;
        }

    }
}
