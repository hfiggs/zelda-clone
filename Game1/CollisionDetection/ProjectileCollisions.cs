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
        private List<Collision> collisionList;
        private List<IProjectile> ProjectileList;
        private List<IPlayer> players;
        private Rectangle playerHitbox;
        private List<IEnemy> EnemyList;
        private List<IItem> ItemList;

        public ProjectileCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            ItemList = screen.CurrentRoom.ItemList;

            EnemyList = screen.CurrentRoom.EnemyList;

            ProjectileList = screen.CurrentRoom.ProjectileList;

            players = screen.Players;

        }

        // Collision order: projectile to player, projectile to enemy, projectile to item
        public List<Collision> GetCollisionList()
        {
            // Projectile hits Player
            foreach (IProjectile proj in ProjectileList)
            {
                Rectangle projHitbox = proj.GetHitbox();
                foreach (IPlayer player in players)
                {
                    playerHitbox = player.GetPlayerHitbox();
                    Rectangle intersectPlayer = Rectangle.Intersect(projHitbox, playerHitbox);
                    if (!intersectPlayer.IsEmpty)
                    {
                        char side = CollisonDetectionUtil.DetermineSide(projHitbox, playerHitbox, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, proj, player));
                    }
                }
                // Projectile hits Enemy
                foreach (IEnemy enemy in EnemyList)
                {
                    foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                    {
                        Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, projHitbox);
                        if (!intersectEnemy.IsEmpty)
                        {
                            char side = CollisonDetectionUtil.DetermineSide(projHitbox, enemyHitbox, intersectEnemy);
                            collisionList.Add(new Collision(side, intersectEnemy, proj, enemy));
                        }
                    }
                }

                // Projectile hits Item
                foreach (IItem item in ItemList)
                {
                    Rectangle itemHitbox = item.GetHitbox();
                    Rectangle interscetItem = Rectangle.Intersect(itemHitbox, projHitbox);
                    if (!interscetItem.IsEmpty)
                    {
                        char side = CollisonDetectionUtil.DetermineSide(projHitbox, itemHitbox, interscetItem);
                        collisionList.Add(new Collision(side, interscetItem, proj, item));
                    }
                }
            }

            return collisionList;
        }
    }
}
