

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentWestSideCommand : ICollisionCommand
    {
        public ProjectileToEnvironmentWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
             if (collision.intersectionRec.Left <= 0)
                ((IProjectile)collision.collider).BeginDespawn();
        }
    }
}
