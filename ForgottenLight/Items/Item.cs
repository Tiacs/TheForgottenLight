using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.Items {
    class Item {

        public string Name {
            get; set;
        }
        
        public string Description {
            get; set;
        }

        public ItemCode ID {
            get; set;
        } = ItemCode.NONE;

        public bool RandomLoot {
            get; set;
        } = true;

        public int CountLimit {
            get; set;
        } = -1;
    }

    public enum ItemCode {
        NONE, KEY
    }
}
