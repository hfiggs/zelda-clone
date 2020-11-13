using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Environment;
using Game1.Enemy;
using Game1.Projectile;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;

namespace Game1.CollisionDetection
{
    class EnvironmentCollisions
    {
        private List<Collision> collisionList;
        private List<IEnvironment> EnvironmentList;
        private List<IEnemy> EnemyList;
        private List<IProjectile> ProjectileList;
        private IPlayer player;
        private Rectangle playerHitbox;

        public EnvironmentCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnvironmentList = screen.CurrentRoom.InteractEnviornment;

            EnemyList = screen.CurrentRoom.EnemyList;

            ProjectileList = screen.CurrentRoom.ProjectileList;

            player = screen.Player;
            playerHitbox = player.GetPlayerHitbox();
        }

        // Collision order: player to environment, enemy to environment, projectile to environment
        public List<Collision> GetCollisionList()
        {
            bool collision = false;
            foreach (IEnvironment environment in EnvironmentList)
            {
                // Some environment objects have multiple hitboxes
                if (collision)
                    break;
                foreach (Rectangle envHitbox in environment.GetHitboxes())
                {
                    // Player collides with Environment
                    Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, envHitbox);
                    if (!intersectPlayer.IsEmpty)
                    {
                        char side = DetermineSide(playerHitbox, envHitbox, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, player, environment));
                        collision = true;
                        break;
                    }

                    // Projectile collides with Environment
                    foreach (IProjectile proj in ProjectileList)
                    {
                        Rectangle projHitbox = proj.GetHitbox();
                        Rectangle intersectEnv = Rectangle.Intersect(projHitbox, envHitbox);
                        if (!intersectEnv.IsEmpty)
                        {
                            char side = DetermineSide(projHitbox, envHitbox, intersectEnv);
                            collisionList.Add(new Collision(side, intersectEnv, proj, environment));
                        }
                    }

                    // Enemy collides with Environment
                    foreach (IEnemy enemy in EnemyList)
                    {
                        foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
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
                if (colider.Y < colidee.Y) {
                    side = north;
                } else {
                    side = south;
                }
            } else {
                if (colider.X < colidee.X) {
                    side = west;
                }
                else {
                    side = east;
                }
            }
            return side;
        }
    }
}
