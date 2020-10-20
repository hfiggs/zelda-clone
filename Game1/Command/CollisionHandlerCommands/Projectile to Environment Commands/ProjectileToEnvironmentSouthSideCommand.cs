

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentSouthSideCommand : ICollisionCommand
    {
        public ProjectileToEnvironmentSouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
           if (collision.intersectionRec.Bottom >= 176)
                ((IProjectile)collision.collider).BeginDespawn();
        }
    }
}
