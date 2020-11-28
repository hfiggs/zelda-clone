using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;

namespace Game1.Command
{
    class PlayerEquipItemCommand : ICommand
    {
        private Game1 game;
        private ItemEnum itemToEquip;
        private PlayerIndex playerIndex;

        public PlayerEquipItemCommand(Game1 game, ItemEnum itemToEquip, PlayerIndex playerIndex = PlayerIndex.One)
        {
            this.game = game;
            this.itemToEquip = itemToEquip;
            this.playerIndex = playerIndex;
        }

        public void Execute()
        {
            if(game.Screen.Players[(int)playerIndex].PlayerInventory.HasItem(itemToEquip) && itemToEquip != ItemEnum.Arrow)
            {
                game.Screen.Players[(int)playerIndex].PlayerInventory.EquippedItem = itemToEquip;
            }
        }
    }
}
