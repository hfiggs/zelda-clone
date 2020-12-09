using Game1.Audio;
using Game1.Collision_Handling;
using Game1.Item;
using Game1.Player;

namespace Game1.Command.CollisionHandlerCommands
{
    class LinkToItemCommand : ICollisionCommand
    {
        private Game1 game;

        public LinkToItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IPlayer player = (IPlayer)collision.Collider;
            IItem item = (IItem)collision.Collidee;

            if (!(player.GetType() == typeof(Player2) && game.Mode == 2))
            {
                CollisionHandlerUtil.HandlePlayerPickupItem(game, player, item);

                AudioManager.PlayItemSound(item);
            }
        }
    }
}
