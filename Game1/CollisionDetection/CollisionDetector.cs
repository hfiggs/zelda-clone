using System.Collections.Generic;
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

            collisionList.AddRange(new EnvironmentCollisions(screen).GetCollisionList());         // Collisions to the Environment (by everything but items)
            collisionList.AddRange(new EnemyCollisions(screen).GetCollisionList());               // Collisions by Enemy to Player
            collisionList.AddRange(new PlayerCollisions(screen).GetCollisionList());              // Collisions by Player to Item and Enemy
            collisionList.AddRange(new ProjectileCollisions(screen).GetCollisionList());          // Collisions by Projectile to Player, Enemy, and Item

            return collisionList;
        }
    }
}
