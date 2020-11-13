using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Enemy;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using Game1.Environment;

namespace Game1.CollisionDetection
{
    class EnemyCollisions
    {
        
        private readonly List<IEnemy> EnemyList;
        private IPlayer player;
        private readonly Rectangle playerHitbox;
        private readonly List<IEnvironment> EnvironmentList;
        private readonly List<Collision> collisionList;

        public EnemyCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnemyList = screen.CurrentRoom.EnemyList;

            EnvironmentList = screen.CurrentRoom.InteractEnviornment;

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

            foreach (IEnemy enemy in EnemyList)
            {
                bool collision = false;
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    foreach (IEnvironment environment in EnvironmentList)
                    {
                        foreach (Rectangle envHitbox in environment.GetHitboxes())
                        {
                            Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, envHitbox);
                            if (!intersectEnemy.IsEmpty)
                            {
                                char side = DetermineSide(enemyHitbox, envHitbox, intersectEnemy);
                                collisionList.Add(new Collision(side, intersectEnemy, enemy, environment));
                                collision = true;
                                break;
                            }
                        }
                        if (collision)
                            break;
                    }
                    if (collision)
                        break;
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
