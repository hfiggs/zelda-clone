using Game1.Player.PlayerInventory;

namespace Game1.Command
{
    class PlayerEquipItemCommand : ICommand
    {
        private Game1 game;
        private ItemEnum itemToEquip;

        public PlayerEquipItemCommand(Game1 game, ItemEnum itemToEquip)
        {
            this.game = game;
            this.itemToEquip = itemToEquip;
        }

        public void Execute()
        {
            if(game.Screen.Player.PlayerInventory.HasItem(itemToEquip) && itemToEquip != ItemEnum.Arrow)
            {
                game.Screen.Player.PlayerInventory.EquippedItem = itemToEquip;
            }
        }
    }
}
