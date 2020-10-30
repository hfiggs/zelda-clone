namespace Game1.Command
{
    class PlayerUseItemCommand : ICommand
    {
        private Game1 game;

        public PlayerUseItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Screen.Player.UseItem();
        }
    }
}
