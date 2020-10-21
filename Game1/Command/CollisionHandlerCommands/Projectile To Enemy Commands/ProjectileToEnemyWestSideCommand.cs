

using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Game1.Projectile;
using System.CodeDom;
using System.Reflection;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnemyWestSideCommand : ICollisionCommand
    {
        public ProjectileToEnemyWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            ((IProjectile)collision.collider).BeginDespawn();
            if(collision.collider.GetType() == typeof(Boomerang))
            {
                if(collision.collidee.GetType() == typeof(Jelly))
                {

                }
            }
            else if(collision.collider.GetType() == typeof(Arrow))
            {
                
            }
            else if(collision.collider.GetType() == typeof(SwordBeam))
            {

            }
            else if(collision.collider.GetType() == typeof(BombProjectile))
            {

            }
        }
    }
}
