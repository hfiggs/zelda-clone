using Game1.Audio;
using Game1.Collision_Handling;
using Game1.Item;
using Game1.Player;

namespace Game1.Command.CollisionHandlerCommands
{
    class LinkToItemCommand : ICollisionCommand
    {
        public LinkToItemCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IPlayer player = (IPlayer)collision.collider;
            IItem item = (IItem)collision.collidee;

            CollisionHandlerUtil.HandlePlayerPickupItem(player, item);

            AudioManager.PlayItemSound(item);
        }
    }
}
