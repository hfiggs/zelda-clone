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
        private readonly List<Collision> collisionList;
        private readonly List<IEnvironment> EnvironmentList;
        private readonly List<IProjectile> ProjectileList;

        public EnvironmentCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnvironmentList = screen.CurrentRoom.InteractEnviornment;

            ProjectileList = screen.CurrentRoom.ProjectileList;
        }

        // Collision order: player to environment, enemy to environment, projectile to environment
        public List<Collision> GetCollisionList()
        {
            // Projectile collides with Environment
            foreach (IProjectile proj in ProjectileList)
            {
                foreach (IEnvironment environment in EnvironmentList)
                {
                    foreach (Rectangle envHitbox in environment.GetHitboxes())
                    {
                        Rectangle projHitbox = proj.GetHitbox();
                        Rectangle intersectEnv = Rectangle.Intersect(projHitbox, envHitbox);
                        if (!intersectEnv.IsEmpty)
                        {
                            char side = DetermineSide(projHitbox, envHitbox, intersectEnv);
                            collisionList.Add(new Collision(side, intersectEnv, proj, environment));
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
