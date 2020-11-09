

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Particle;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentEastSideCommand : ICollisionCommand
    {
        public ProjectileToEnvironmentEastSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = ((IProjectile)collision.collider);
            IEnvironment envo = (IEnvironment)collision.collidee;
            const int bombWidth = 12; // bomb's width before explosion
            const int bombHeight = 16; // bomb's height before explosion
            if (envo.GetType() == typeof(DoorWBombable) && proj.GetType() == typeof(BombProjectile) && proj.GetHitbox().Width > bombWidth && proj.GetHitbox().Height > bombHeight)
            {
                ((DoorWBombable)envo).openDoor();
            }
            if (proj is SwordBeam)
                return;
            if ((collision.intersectionRec.X >= 224 || collision.intersectionRec.X <= 30 || collision.intersectionRec.Y >= 144 || collision.intersectionRec.Y <= 30) && proj.GetType() != typeof(BombProjectile))
                proj.BeginDespawn();
        }
    }
}
