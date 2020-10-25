

using Game1.Collision_Handling;
using Game1.Environment;
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
            IProjectile proj = ((IProjectile)collision.collider);
            IEnvironment envo = (IEnvironment)collision.collidee;
            if (envo.GetType() == typeof(DoorSBombable) && proj.GetType() == typeof(BombProjectile))
            {
                ((DoorSBombable)envo).openDoor();
            }

            if (collision.intersectionRec.Top >= 156)
                proj.BeginDespawn();
        }
    }
}
