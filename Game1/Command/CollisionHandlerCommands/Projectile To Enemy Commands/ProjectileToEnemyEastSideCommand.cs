

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnemyEastSideCommand : ICollisionCommand
    {
        public ProjectileToEnemyEastSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            ((IProjectile)collision.collider).BeginDespawn();
        }
    }
}
