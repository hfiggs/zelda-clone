

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentNorthSideCommand : ICollisionCommand
    {
        public ProjectileToEnvironmentNorthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
/*            if (collision.intersectionRec.Top <= 0)
                ((IProjectile)collision.collider).despawn();*/
        }
    }
}
