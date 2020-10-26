using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading
{
    public class Room
    {
        public List<IItem> ItemList { get; set; }
        public List<IEnvironment> NonInteractEnviornment { get; set; }
        public List<IEnvironment> InteractEnviornment { get; set; }
        public List<IEnemy> EnemyList { get; set; }

        private Boolean opened = false;
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
        }

        public void Update(GameTime gameTime)
        {
            if (!opened)
            {
                //foreach (IEnemy enemy in EnemyList)
                //{
                //    enemy.SpawnAnimation();
                //}
                try
                {
                    foreach (IEnemy enemy in EnemyList)
                    {
                        enemy.SpawnAnimation();
                    }
                }
                catch (System.InvalidOperationException e)
                {
                    Console.WriteLine("Enemy foreach error");
                }
                opened = true;
            }
            

            foreach (IItem item in ItemList)
            {
                item.Update(gameTime);
            }

            try
            {
                foreach (IEnemy enemy in EnemyList)
                {
                    enemy.Update(gameTime, new Rectangle(0, 0, 256, 176));
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine("Enemy foreach error");
            }



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
    }
}
