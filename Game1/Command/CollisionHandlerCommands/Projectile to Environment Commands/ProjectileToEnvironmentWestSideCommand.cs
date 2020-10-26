

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
            if (envo.GetType() == typeof(DoorEBombable) && proj.GetType() == typeof(BombProjectile))
            {
                ((DoorEBombable)envo).openDoor();
            }
            if (collision.intersectionRec.X >= 224 || collision.intersectionRec.X <= 30 || collision.intersectionRec.Y >= 144 || collision.intersectionRec.Y <= 30)
                proj.BeginDespawn();
        }
    }
}
