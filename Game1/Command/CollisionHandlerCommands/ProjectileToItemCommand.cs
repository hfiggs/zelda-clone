using Game1.Collision_Handling;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToItemCommand : ICollisionCommand
    {
        public ProjectileToItemCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile projectile = (IProjectile)collision.collider;
            IItem item = (IItem)collision.collidee;

            if(projectile.GetType() == typeof(Boomerang))
            {
                IPlayer player = ((Boomerang)projectile).Player;

                CollisionHandlerUtil.HandlePlayerPickupItem(player, item);
            }
        }
    }
}
