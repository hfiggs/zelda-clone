using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Item;
using Game1.Enemy;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;

namespace Game1.CollisionDetection
{
    class PlayerCollisions
    {
        private List<Collision> collisionList;
        private List<IEnemy> EnemyList;
        private IPlayer player;
        private Rectangle playerHitbox;
        private Rectangle swordHitbox;
        private List<IItem> ItemList;

        public PlayerCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnemyList = screen.CurrentRoom.EnemyList;

            ItemList = screen.CurrentRoom.ItemList;

            player = screen.Player;
            playerHitbox = player.GetPlayerHitbox();
            swordHitbox = player.GetSwordHitbox();
        }

        // Collision order: player to item, player to enemy
        public List<Collision> GetCollisionList()
        {
            // Player collides with Item
            foreach (IItem item in ItemList)
            {
                Rectangle itemHitbox = item.GetHitbox();
                Rectangle intersection = Rectangle.Intersect(itemHitbox, playerHitbox);
                if (!intersection.IsEmpty)
                {
                    char side = DetermineSide(playerHitbox, itemHitbox, intersection);
                    collisionList.Add(new Collision(side, intersection, player, item));
                }
                else
                {
                    intersection = Rectangle.Intersect(itemHitbox, swordHitbox);
                    if (!intersection.IsEmpty)
                    {
                        char side = DetermineSide(swordHitbox, itemHitbox, intersection);
                        collisionList.Add(new Collision(side, intersection, player, item));
                    }
                }
            }

            // Player collides with Enemy
            foreach (IEnemy enemy in EnemyList)
            {
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    Rectangle intersectSword = Rectangle.Intersect(swordHitbox, enemyHitbox);
                    if (!intersectSword.IsEmpty)
                    {
                        char side = player.GetDirection();
                        collisionList.Add(new Collision(side, intersectSword, player, enemy));
                    }
                }
            }

                    return collisionList;
        }

        private char DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec)
        {
            const char north = 'N', south = 'S', west = 'W', east = 'E';
            int xOverlap = intersectionRec.Width;
            int yOverlap = intersectionRec.Height;
            char side;

            if (xOverlap > yOverlap)
            {
                if (colider.Y < colidee.Y)
                {
                    side = north;
                }
                else
                {
                    side = south;
                }
            }
            else
            {
                if (colider.X < colidee.X)
                {
                    side = west;
                }
                else
                {
                    side = east;
                }
            }
            return side;
        }
    }
}
