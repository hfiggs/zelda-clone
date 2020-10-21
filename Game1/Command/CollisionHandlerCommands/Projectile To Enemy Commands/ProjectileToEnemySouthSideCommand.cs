

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnemySouthSideCommand : ICollisionCommand
    {
        public ProjectileToEnemySouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            ((IProjectile)collision.collider).BeginDespawn();
        }
    }
}
