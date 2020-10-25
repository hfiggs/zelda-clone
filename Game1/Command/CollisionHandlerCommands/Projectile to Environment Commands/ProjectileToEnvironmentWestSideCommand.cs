

using Game1.Collision_Handling;
using Game1.Environment;
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
            IProjectile proj = ((IProjectile)collision.collider);
            IEnvironment envo = (IEnvironment)collision.collidee;
            if (envo.GetType() == typeof(DoorEBombable))
            {
                ((DoorEBombable)envo).openDoor();
            }
            if (collision.intersectionRec.Left <= 0)
                proj.BeginDespawn();
        }
    }
}
