using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

/*
 * PLEASE NOTE:
 * This is a placeholder class simply for testing the basics of the collision detection system
 * This file will be replaced with the actual room object later
 */


namespace Game1.CollisionDetection
{
    class Room
    {
        public LinkedList<IEnvironment> EnvironmentList { get; set; }
        public LinkedList<IEnemy> EnemyList { get; set; }
        public LinkedList<IItem> ItemList { get; set; }
        public LinkedList<IProjectile> ProjectileList { get; set; }

        public IPlayer Link { get; set; }

        private CollisionDetector CollisionDetect;

        public Room(Game1 game)
        {
            EnvironmentList = game.EnvironmentList;
            ItemList = game.ItemList;
            EnemyList = game.EnemyList;
            ProjectileList = game.ProjectileList;
            Link = game.Player;
            CollisionDetect = new CollisionDetector(this);
        }

        public void Update()
        {
            string listString = "(";
            foreach(Collision data in CollisionDetect.GetCollisionList())
            {
                listString += data.ToString() + "), (";
            }
            Console.WriteLine(listString + ")");
        }
    }
}
