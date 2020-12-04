using Game1.Player.PlayerInventory;

namespace Game1.Command
{
    class Player2EquipItemCommand : ICommand
    {
        private Game1 game;
        private ItemEnum itemToEquip;
        private const int playerIndex = 1;

        public Player2EquipItemCommand(Game1 game, ItemEnum itemToEquip)
        {
            this.game = game;
            this.itemToEquip = itemToEquip;
        }

        public void Execute()
        {
            if(game.Screen.Players[playerIndex].PlayerInventory.HasItem(itemToEquip) && itemToEquip != ItemEnum.Arrow)
            {
                game.Screen.Players[playerIndex].PlayerInventory.EquippedItem = itemToEquip;
            }
        }
    }
}
