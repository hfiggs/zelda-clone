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

/*
 * PLEASE NOTE:
 * This is a placeholder class simply for testing the basics of the collision detection system
 * This file will be replaced with the actual room object later
 */


namespace Game1.CollisionDetection
{
    class Room
    {
        public List<IEnvironment> EnvironmentList { get; set; }
        public List<IEnemy> EnemyList { get; set; }
        public List<IItem> ItemList { get; set; }
        public List<IProjectile> ProjectileList { get; set; }

        public IPlayer Link { get; set; }
    }
}
