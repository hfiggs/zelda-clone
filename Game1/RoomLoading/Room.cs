using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
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
        public LinkedList<IItem> ItemList { get; set; }
        public LinkedList<IEnvironment> NonInteractEnviornment { get; set; }
        public LinkedList<IEnvironment> InteractEnviornment { get; set; }
        public LinkedList<IEnemy> EnemyList { get; set; }
        public Room(Game1 game, String file)
        {
            RoomParser parser = new RoomParser(game, file);
            ItemList = parser.GetItems();
            NonInteractEnviornment = parser.GetNonInteractableEnvinornment();
            InteractEnviornment = parser.GetInteractableEnvinornment();
            EnemyList = parser.GetEnemies();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IItem item in ItemList)
            {
                item.Update(gameTime);
            }

            foreach (IEnemy enemy in EnemyList)
            {
                enemy.Update(gameTime, new Rectangle(0, 0, 256, 176));
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
