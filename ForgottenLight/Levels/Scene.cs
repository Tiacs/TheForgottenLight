﻿/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Entities;
using ForgottenLight.UI;
using ForgottenLight.Events;

namespace ForgottenLight.Levels {
    abstract class Scene {

        private Game1 game;
        
        protected ContentManager contentManager;

        public string Version => game.Version;

        public List<Entity> Entities {
            get; protected set;
        }

        public List<Light> Lights {
            get; protected set;
        }

        public Player Player {
            get; protected set;
        }

        public float Width {
            get; protected set;
        }

        public float Height {
            get; protected set;
        }

        public UserInterface Interface {
            get; protected set;
        }

        public bool IsPaused {
            get; set;
        }

        public Scene(float width, float height) {
            this.Width = width;
            this.Height = height;
            
            this.Entities = new List<Entity>();
            this.Lights = new List<Light>();
        }

        protected abstract void LoadContent(ContentManager contentManager);

        public virtual void Initialize(ContentManager contentManager, Game1 game) {
            this.game = game;

            this.contentManager = contentManager;
            this.LoadContent(contentManager);

            this.CreateWalls();
        }

        protected virtual void CreateWalls() {

        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            Input.Instance.Update();

            if (!IsPaused) {
                this.Entities.ForEach(entity => entity.Update(gameTime, keyboardState, mouseState));
                this.Lights.ForEach(light => light.Update(gameTime, keyboardState, mouseState));
            }
            this.Interface.Update(gameTime, keyboardState, mouseState);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Entities.Sort(CompareDepth);
            this.Entities.ForEach(entity => entity.Draw(spriteBatch, gameTime));
        }

        private int CompareDepth(Entity e1, Entity e2) {
            return e1.DepthIndex - e2.DepthIndex;
        }

        public void DrawLights(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Lights.ForEach(light => light.Draw(spriteBatch, gameTime));
        }

        public void DrawUI(SpriteBatch spriteBatch, GameTime gameTime) {
            this.Interface.Draw(spriteBatch, gameTime);
        }
        
        public void LoadScene(Scene scene) {
            this.game.LoadScene(scene);
        }
        
        public virtual void ReloadScene() {
            Scene instance = (Scene) Activator.CreateInstance(this.GetType()); // create new instance from current type
            this.game.LoadScene(instance); // Load new scene into gam
        }

        public virtual void NextScene() {

        }

        /// <summary>
        /// Converts screen position to position in game. (Is needed for multiple screen resolutions in fullscreen mode, when using mouse position.)
        /// </summary>
        /// <param name="screenPosition">Position on screen</param>
        /// <returns>Position in game</returns>
        public Vector2 ScreenToGamePosition(Vector2 screenPosition) {
            return game.ScreenToGamePosition(screenPosition);
        }

        /// <summary>
        /// Exit the game.
        /// </summary>
        public void Exit() {
            this.game.Exit();
        }
    }
}
