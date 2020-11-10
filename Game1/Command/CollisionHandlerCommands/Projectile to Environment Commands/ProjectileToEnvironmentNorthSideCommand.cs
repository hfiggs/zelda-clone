

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
            const int bombWidth = 12; // bomb's width before explosion
            const int bombHeight = 16; // bomb's height before explosion
            if (envo.GetType() == typeof(DoorSBombable) && proj.GetType() == typeof(BombProjectile) && proj.GetHitbox().Width > bombWidth && proj.GetHitbox().Height > bombHeight)
            {
                ((DoorSBombable)envo).OpenDoor(true);
            }
            if (proj is SwordBeam)
                return;
            if ((collision.intersectionRec.X >= 224 || collision.intersectionRec.X <= 30 || collision.intersectionRec.Y >= 144 || collision.intersectionRec.Y <= 30) && proj.GetType() != typeof(BombProjectile))
                proj.BeginDespawn();
        }
    }
}
