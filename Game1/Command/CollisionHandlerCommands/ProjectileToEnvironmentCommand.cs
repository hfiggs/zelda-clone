/* Author: Hunter Figgs.3 */

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Projectile;
using Game1.Util;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnvironmentCommand : ICollisionCommand
    {
        private Game1 game;

        private const int bombWidth = 12; // bomb's width before explosion
        private const int bombHeight = 16; // bomb's height before explosion
        private const int rightBorder = 244, leftBorder = 30, bottomBorder = 144, topBorder = 30;

        public ProjectileToEnvironmentCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IProjectile proj = ((IProjectile)collision.collider);
            IEnvironment envo = (IEnvironment)collision.collidee;

            if (envo is LaserField)
            {
                proj.BeginDespawn();
            }
            else if (envo is PortalBlock portalBlock)
            {
                PortalUtil.HandleProjectilePortal(portalBlock, proj, game.Screen);
            }
            else if (proj is BombProjectile && proj.GetHitbox().Width > bombWidth && proj.GetHitbox().Height > bombHeight)
            {
                RoomUtil.OpenBombableDoor(game.Screen, envo);
            }
            else if (!(proj is SwordBeam) && !(proj is BombProjectile))
            {
                if (collision.intersectionRec.X >= rightBorder || collision.intersectionRec.X <= leftBorder || collision.intersectionRec.Y >= bottomBorder || collision.intersectionRec.Y <= topBorder)
                    proj.BeginDespawn();
            }
        }
    }
}
