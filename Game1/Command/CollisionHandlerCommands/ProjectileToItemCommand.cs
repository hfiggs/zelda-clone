using Game1.Audio;
using Game1.Collision_Handling;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToItemCommand : ICollisionCommand
    {
        private Game1 game;

        public ProjectileToItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IProjectile projectile = (IProjectile)collision.Collider;
            IItem item = (IItem)collision.Collidee;

            IPlayer player;
            switch(projectile)
            {
                case Boomerang _:
                    player = ((Boomerang)projectile).Player;
                    CollisionHandlerUtil.HandlePlayerPickupItem(game, player, item);
                    break;

                case Arrow _:
                    if(item.GetType() == typeof(Fairy))
                    {
                        player = ((Arrow)projectile).Player;
                        CollisionHandlerUtil.HandlePlayerPickupItem(game, player, item);
                    }
                    break;
            }

            if (!projectile.GetType().Equals(typeof(SwordBeam)) && !projectile.GetType().Equals(typeof(EnemyBoomerang)) && !projectile.GetType().Equals(typeof(Fireballs)) && !(projectile is PortalProjectile)) {
                AudioManager.PlayItemSound(item);
            }
        }
    }
}
