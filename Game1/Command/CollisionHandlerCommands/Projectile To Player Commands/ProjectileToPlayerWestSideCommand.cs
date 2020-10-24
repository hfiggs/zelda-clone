

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
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
            IProjectile proj = ((IProjectile)collision.collider);
            if(proj.GetType() == typeof(EnemyBoomerang) || proj.GetType() == typeof(Fireballs))
            {
                ((IPlayer)collision.collidee).ReceiveDamage(new Vector2(1, 0));
                proj.BeginDespawn();
            }
        }
    }
}
