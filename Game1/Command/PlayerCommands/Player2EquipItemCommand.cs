using Game1.Player.PlayerInventory;

namespace Game1.Command
{
    class Player2EquipItemCommand : ICommand
    {
        private Game1 game;
        private ItemEnum itemToEquip;

        public Player2EquipItemCommand(Game1 game, ItemEnum itemToEquip)
        {
            this.game = game;
            this.itemToEquip = itemToEquip;
        }

        public void Execute()
        {
            if(game.Screen.Player2.PlayerInventory.HasItem(itemToEquip) && itemToEquip != ItemEnum.Arrow)
            {
                game.Screen.Player2.PlayerInventory.EquippedItem = itemToEquip;
            }
        }
    }
}
