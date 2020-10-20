

using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Player;
using Game1.Projectile;
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
                if(collision.intersectionRec.Right >= 256)
                ((IProjectile)collision.collider).BeginDespawn();
        }
    }
}
