using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading
{
    public class Screen
    {
        public IPlayer Player { get; set; }
        private LinkedList<IItem> ItemList { get; set; }
        private LinkedList<IEnvironment> NonInteractEnviornment { get; set; }
        private LinkedList<IEnvironment> InteractEnviornment { get; set; }
        private LinkedList<IEnemy> EnemyList { get; set; }
        private LinkedList<IProjectile> ProjectileList { get; set; }
        private Room Room { get; set; }
        public Screen(Game1 game, char x, int y)
        {
            this.Room = new Room(game, x , y);
            this.ProjectileList = new LinkedList<IProjectile>();
            this.Player = new Player1(game, new Vector2(80, 80));
            ItemList = Room.GetItems();
            NonInteractEnviornment = Room.GetNonInteractableEnvinornment();
            InteractEnviornment = Room.GetInteractableEnvinornment();
            EnemyList = Room.GetEnemies();
        }

        public void Update(GameTime gameTime)
        {

            LinkedList<IProjectile> projectilesToRemove = new LinkedList<IProjectile>();

            foreach (IProjectile projectile in ProjectileList)
            {
                if (projectile.Update(gameTime))
                {
                    projectilesToRemove.AddFirst(projectile);
                }
            }

            foreach (IProjectile projectile in projectilesToRemove)
            {
                ProjectileList.Remove(projectile);
            }

            Player.Update(gameTime);

            foreach (IItem item in ItemList)
            {
                item.Update(gameTime);
            }

            foreach (IEnemy enemy in EnemyList)
            {
                enemy.Update(gameTime, new Rectangle(32, 32, 224, 144));
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

            Player.Draw(spriteBatch, Color.White);

            foreach (IProjectile projectile in ProjectileList)
            {
                projectile.Draw(spriteBatch, Color.White);
            }
        }

        public void SpawnProjectile(IProjectile projectile)
        {
            ProjectileList.AddLast(projectile);
        }

        public Rectangle GetPlayerRectangle()
        {
            return Player.GetLocation();
        }
    }
}
