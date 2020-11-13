using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Enemy;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;

namespace Game1.CollisionDetection
{
    class EnemyCollisions
    {
        private List<Collision> collisionList;
        private List<IEnemy> EnemyList;
        private IPlayer player;
        private Rectangle playerHitbox;

        public EnemyCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnemyList = screen.CurrentRoom.EnemyList;

            player = screen.Player;
            playerHitbox = player.GetPlayerHitbox();
        }

        public List<Collision> GetCollisionList()
        {
            // Enemy collides with Player
            foreach (IEnemy enemy in EnemyList)
            {
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    Rectangle intersectPlayer = Rectangle.Intersect(enemyHitbox, playerHitbox);
                    if (!intersectPlayer.IsEmpty)
                    {
                        char side = DetermineSide(enemyHitbox, playerHitbox, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, enemy, player));
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
