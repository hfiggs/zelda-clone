﻿using Game1.AudioManagement;
using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game1.RoomLoading
{
    public class Room
    {
        public List<IItem> ItemList { get; set; }
        public List<IEnvironment> NonInteractEnviornment { get; set; }
        public List<IEnvironment> InteractEnviornment { get; set; }
        public List<IEnemy> EnemyList { get; set; }
        public List<IEnemy> DecoratedEnemyList { get; set; }
        public List<IEnemy> UnDecoratedEnemyList { get; set; }

        private List<AmbientSound> soundList;

        public Room(Game1 game, String file)
        {
            game.Screen.CurrentRoom = this;
            RoomParser parser = new RoomParser(game, file);
            ItemList = new List<IItem>();
            NonInteractEnviornment = new List<IEnvironment>();
            InteractEnviornment = new List<IEnvironment>();
            EnemyList = new List<IEnemy>();
            ItemList.AddRange(parser.GetItems());
            NonInteractEnviornment.AddRange(parser.GetNonInteractableEnvinornment());
            InteractEnviornment.AddRange(parser.GetInteractableEnvinornment());
            EnemyList.AddRange(parser.GetEnemies());

            DecoratedEnemyList = new List<IEnemy>();
            UnDecoratedEnemyList = new List<IEnemy>();

            soundList = parser.GetAmbienceNode();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IItem item in ItemList)
            {
                item.Update(gameTime);
            }

            CheckClocksAndStunEnemies();

            ItemList.RemoveAll(p => p.ShouldDelete);

            foreach (IEnemy enemy in EnemyList)
            {
                const int width = 256, height = 176; 
                enemy.Update(gameTime, new Rectangle(0, 0, width, height));
            }

            foreach (IEnemy decoratedEnemy in DecoratedEnemyList)
            {
                EnemyList.Remove(((EnemyDamageDecorator)decoratedEnemy).original);
                EnemyList.Add(decoratedEnemy);
            }
            DecoratedEnemyList.Clear();

            foreach (IEnemy unDecoratedEnemy in UnDecoratedEnemyList)
            {
                EnemyList.Remove((EnemyDamageDecorator)unDecoratedEnemy);
                EnemyList.Add(((EnemyDamageDecorator)unDecoratedEnemy).original);
            }
            UnDecoratedEnemyList.Clear();

            EnemyList.RemoveAll(p => p.ShouldRemove());

            foreach (IEnvironment interactEnvironment in InteractEnviornment)
            {
                interactEnvironment.BehaviorUpdate(gameTime);
            }

            foreach (IEnvironment nonInternactEnvironment in NonInteractEnviornment)
            {
                nonInternactEnvironment.BehaviorUpdate(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IEnvironment nonInternactEnvironment in NonInteractEnviornment)
            {
                nonInternactEnvironment.Draw(spriteBatch, Color.White);
            }

            foreach (IEnvironment internactEnvironment in InteractEnviornment)
            {
                internactEnvironment.Draw(spriteBatch, Color.White);
            }

            foreach (IEnemy enemy in EnemyList)
            {
                enemy.Draw(spriteBatch, Color.White);
            }

            foreach (IItem item in ItemList)
            {
                item.Draw(spriteBatch, Color.White);
            }
        }

        private void CheckClocksAndStunEnemies()
        {
            foreach(IItem item in ItemList)
            {
                if(item.GetType() == typeof(Clock) && item.ShouldDelete)
                {
                    foreach(IEnemy enemy in EnemyList)
                    {
                        enemy.StunnedTimer = int.MaxValue;
                    }
                    break;
                }
            }
        }

        public void StopRoomAmbience()
        {
            foreach(AmbientSound sound in soundList)
            {
                sound.Stop();
            }
        }

        public void PlayRoomAmbience()
        {
            foreach (AmbientSound sound in soundList)
            {
                sound.Play();
            }
        }
    }
}
