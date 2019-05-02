﻿using ForgottenLight.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgottenLight.UI {
    abstract class UserInterface : UIComponent {

        public SpriteFont MainFont {
            get; protected set;
        }

        public UserInterface(ContentManager content) : base(Vector2.Zero) {
            LoadContent(content);
        }

        private void LoadContent(ContentManager content) {
            this.MainFont = content.Load<SpriteFont>("fonts/Main");
        }

    }
}