

using Game1.Collision_Handling;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerWestSideCommand : ICollisionCommand
    {
        public ProjectileToPlayerWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            //((IProjectile)collision.collider).BeginDespawn();
            ((IPlayer)collision.collidee).ReceiveDamage(new Vector2(-1, 0));
        }
    }
}
