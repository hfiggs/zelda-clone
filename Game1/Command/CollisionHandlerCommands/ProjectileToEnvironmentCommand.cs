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

        public ProjectileToEnvironmentCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IProjectile proj = ((IProjectile)collision.collider);
            IEnvironment envo = (IEnvironment)collision.collidee;

            if (proj is BombProjectile && proj.GetHitbox().Width > bombWidth && proj.GetHitbox().Height > bombHeight)
            {
                RoomUtil.OpenBombableDoor(game.Screen, envo);
            }
            else if (!(proj is SwordBeam) && !(proj is BombProjectile))
            {
                if ((collision.intersectionRec.X >= 224 || collision.intersectionRec.X <= 30 || collision.intersectionRec.Y >= 144 || collision.intersectionRec.Y <= 30))
                    proj.BeginDespawn();
            }
        }
    }
}
