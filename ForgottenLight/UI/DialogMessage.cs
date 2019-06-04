/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

namespace ForgottenLight.UI {
    class DialogMessage {

        public string Text {
            get; set;
        }

        public bool AutoContinue {
            get; set;
        }

        public int Length => Text.Length;
        
        public DialogMessage(string text = "", bool autoContinue = false) {
            this.Text = text;
            this.AutoContinue = autoContinue;
        }

    }
}
