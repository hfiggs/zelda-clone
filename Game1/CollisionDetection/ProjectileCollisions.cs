using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Item;
using Game1.Enemy;
using Game1.Projectile;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using Game1.CollisionDetection.CollisionDetectionUtil;

namespace Game1.CollisionDetection
{
    class ProjectileCollisions
    {
        private readonly List<Collision> collisionList;
        private readonly List<IProjectile> projectileList;
        private readonly List<IPlayer> players;
        private readonly List<IEnemy> enemyList;
        private readonly List<IItem> itemList;

        public ProjectileCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            itemList = screen.CurrentRoom.ItemList;

            enemyList = screen.CurrentRoom.EnemyList;

            projectileList = screen.CurrentRoom.ProjectileList;

            players = screen.Players;
        }

        // Collision order: projectile to player, projectile to enemy, projectile to item
        public List<Collision> GetCollisionList()
        {
            // Projectile hits Player
            foreach (IProjectile proj in projectileList)
            {
                Rectangle projHitbox = proj.GetHitbox();

                // Projectile hits Player
                foreach (IPlayer player in players)
                {
                    DetectionUtil.AddCollision(projHitbox, player.GetPlayerHitbox(), proj, player, collisionList);
                }

                // Projectile hits Enemy
                foreach (IEnemy enemy in enemyList)
                {
                    foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                    {
                        DetectionUtil.AddCollision(projHitbox, enemyHitbox, proj, enemy, collisionList);
                    }
                }

                // Projectile hits Item
                foreach (IItem item in itemList)
                {
                    DetectionUtil.AddCollision(projHitbox, item.GetHitbox(), proj, item, collisionList);
                }
            }

            return collisionList;
        }
    }
}
