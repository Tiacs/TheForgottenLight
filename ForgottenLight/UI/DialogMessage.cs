using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
