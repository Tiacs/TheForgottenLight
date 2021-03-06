﻿/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenLight.Levels;

namespace ForgottenLight.UI {
    class ClickableLabel : Label {

        public delegate void OnClickEvent();

        public OnClickEvent OnUse;

        private Color primaryColor;

        public Color HoverColor {
            get; set;
        }

        public Color PrimaryColor {
            get => primaryColor;
            set {
                primaryColor = value;
                Color = value;
            }
        }

        public ClickableLabel(SpriteFont font, Scene scene) : base(font, scene) {

        }

        public ClickableLabel(string text, Vector2 position, SpriteFont font, Color color, Scene scene) : base(text, position, font, color, scene) {

        }

        public ClickableLabel(string text, Vector2 position, SpriteFont font, Scene scene) : this(text, position, font, Color.White, scene) {

        }

        protected override void OnMouseEnter() {
            Color = HoverColor;
        }

        protected override void OnMouseLeave() {
            Color = PrimaryColor;
        }

        protected override void OnClick() {
            OnUse.Invoke();
        }
    }
}
