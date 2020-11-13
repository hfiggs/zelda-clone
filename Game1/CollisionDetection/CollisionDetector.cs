using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Collision_Handling;
using Game1.RoomLoading;

namespace Game1.CollisionDetection
{
    class CollisionDetector
    {
        private readonly Screen screen;
        private List<Collision> collisionList;

        public CollisionDetector(Screen screen)
        {
            this.screen = screen;
        }

        public List<Collision> GetCollisionList()
        {
            collisionList = new List<Collision>();

            EnvironmentCollisions enviromentCollisions = new EnvironmentCollisions(screen); // Collisions to the Environment (by everything but items)
            EnemyCollisions enemyCollisions = new EnemyCollisions(screen);                  // Collisions by Enemy to Player
            PlayerCollisions playerCollisions = new PlayerCollisions(screen);               // Collisions by Player to Item and Enemy
            ProjectileCollisions projectileCollisions = new ProjectileCollisions(screen);   // Collisions by Projectile to Player, Enemy, and Item

            collisionList.AddRange(enviromentCollisions.GetCollisionList());
            collisionList.AddRange(enemyCollisions.GetCollisionList());
            collisionList.AddRange(playerCollisions.GetCollisionList());
            collisionList.AddRange(projectileCollisions.GetCollisionList());

            return collisionList;
        }
    }
}
