

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Particle;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentNorthSideCommand : ICollisionCommand
    {
        private const int rightXBorder = 224, leftXBorder = 30, topYBorder = 30, botYBorder = 144;

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
                ((DoorSBombable)envo).openDoor();
            }
            if (proj is SwordBeam)
                return;
            if ((collision.intersectionRec.X >= rightXBorder || collision.intersectionRec.X <= leftXBorder || collision.intersectionRec.Y >= botYBorder || collision.intersectionRec.Y <= topYBorder) && proj.GetType() != typeof(BombProjectile))
                proj.BeginDespawn();
        }
    }
}
