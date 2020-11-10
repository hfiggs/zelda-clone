﻿

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentSouthSideCommand : ICollisionCommand
    {
        private const int rightXBorder = 224, leftXBorder = 30, topYBorder = 30, botYBorder = 144;

        public ProjectileToEnvironmentSouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {

            IProjectile proj = ((IProjectile)collision.collider);
            IEnvironment envo = (IEnvironment)collision.collidee;
            const int bombWidth = 12; // bomb's width before explosion
            const int bombHeight = 16; // bomb's height before explosion
            if (envo.GetType() == typeof(DoorNBombable) && proj.GetType() == typeof(BombProjectile) && proj.GetHitbox().Width > bombWidth && proj.GetHitbox().Height > bombHeight)
            {
                ((DoorNBombable)envo).openDoor();
            }
            if (proj is SwordBeam)
                return;
            if ((collision.intersectionRec.X >= rightXBorder || collision.intersectionRec.X <= leftXBorder || collision.intersectionRec.Y >= botYBorder || collision.intersectionRec.Y <= topYBorder) && proj.GetType() != typeof(BombProjectile))
                proj.BeginDespawn();
        }
    }
}
