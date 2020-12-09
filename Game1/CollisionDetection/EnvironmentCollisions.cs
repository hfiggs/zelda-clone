using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Environment;
using Game1.Projectile;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using Game1.CollisionDetection.CollisionDetectionUtil;

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
                            char side = CollisonDetectionUtil.DetermineSide(projHitbox, envHitbox, intersectEnv);
                            collisionList.Add(new Collision(side, intersectEnv, proj, environment));
                        }
                    }
                }
            }

            return collisionList;
        }
    }
}
