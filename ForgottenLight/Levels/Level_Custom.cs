﻿using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Newtonsoft.Json;

using ForgottenLight.Entities;
using ForgottenLight.Entities.Ghosts;
using ForgottenLight.Items;
using ForgottenLight.Levels.LevelLoader;
using ForgottenLight.UI;
using System;

namespace ForgottenLight.Levels {
    class Level_Custom : Level {

        private List<Item> items;

        private Random random;

        public Level_Custom(ContentManager contentManager) : base(contentManager, 800, 480) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
        }
        
        public override void Initialize() {
            base.Initialize();

            this.random = new Random();

            LevelWrapper levelWrapper = JsonConvert.DeserializeObject<LevelWrapper>(ReadFromJsonFile("Content/levels/level_1.json"));
            LoadPlayer(levelWrapper);
            LoadItems(levelWrapper.Items);
            LoadEntities(levelWrapper.Entities);
            LoadLights(levelWrapper.Lights);

            this.Interface = new HUD(Width, Height, Player, contentManager);

        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
        }

        protected override void CreateWalls() {
            base.CreateWalls();
        }

        protected override void LoadContent(ContentManager content) {
            base.LoadContent(content);
        }

        private void LoadPlayer(LevelWrapper levelWrapper) {
            this.Player = new Player(levelWrapper.Player.X, levelWrapper.Player.Y, contentManager, this);
            this.Entities.Add(Player);
        }

        private void LoadEntities(EntityWrapper[] entities) {
            foreach(EntityWrapper entity in entities) {
                switch (entity.EntityType) {
                    case EntityWrapper.Type.NORMAL_GHOST:
                        this.Entities.Add(DeserializeNormalGhost(entity));
                        break;
                    case EntityWrapper.Type.CUPBOARD:
                        this.Entities.Add(DeserializeCupboard(entity));
                        break;
                    case EntityWrapper.Type.DOOR:
                        this.Entities.Add(new Door(entity.X, entity.Y, contentManager, this));
                        break;
                    case EntityWrapper.Type.WALL:
                        this.Entities.Add(new Wall((int)entity.Width, (int)entity.Height, entity.X, entity.Y, this));
                        break;
                }
            }
        }

        private Cupboard DeserializeCupboard(EntityWrapper entity) {
            Cupboard cupboard = new Cupboard(entity.X, entity.Y, contentManager, this);
            if(entity.ItemIndex >= 0 && entity.ItemIndex < items.Count) cupboard.Item = items[entity.ItemIndex];
            if (entity.RandomFill) cupboard.Item = GetRandomItem();
            return cupboard;
        }
        
        private NormalGhost DeserializeNormalGhost(EntityWrapper entity) {
            NormalGhost ghost = new NormalGhost(entity.X, entity.Y, contentManager, this);
            foreach (WaypointWrapper waypoint in entity.Path) {
                ghost.Patrol.Add(new Pathfinding.Waypoint(waypoint.X, waypoint.Y));
            }
            return ghost;
        }

        private Item GetRandomItem() {
            if (!HasRandomLoot()) return null;
            Item item;
            while (!(item = items[random.Next(items.Count)]).RandomLoot) {
            }

            if (item.CountLimit > 0) { // If item has limit
                item.CountLimit--; // descrese remaining count
                if (item.CountLimit == 0) {  // if no remaining; remove item from available list
                    items.Remove(item);
                }
            }
            return item;
        }

        private bool HasRandomLoot() {
            foreach(Item item in items) {
                if (item.RandomLoot) return true;
            }
            return false;
        }

        private void LoadItems(ItemWrapper[] items) {
            this.items = new List<Item>();
            foreach (ItemWrapper item in items) {
                this.items.Add(new Item() {
                    ID=item.ItemCode,
                    Name=item.Name,
                    Description=item.Description,
                    RandomLoot=item.RandomLoot,
                    CountLimit=item.CountLimit
                });
            }
        }

        private void LoadLights(LightWrapper[] lights) {
            foreach(LightWrapper light in lights) {
                this.Lights.Add(new Light(light.X, light.Y, contentManager, light.Radius, this));
            }
        }

        private string ReadFromJsonFile(string filePath) {
            try {
                using (StreamReader sr = new StreamReader(filePath)) {
                    return sr.ReadToEnd();
                }
            } catch (IOException) {
                throw new IOException("Level json file could not be read!");
            }
        }
    }
}