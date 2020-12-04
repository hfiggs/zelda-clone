namespace Game1.Command
{
    class Player2UseItemCommand : ICommand
    {
        private Game1 game;
        private const int playerIndex = 1;

        public Player2UseItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Screen.Players[playerIndex].UseItem();
        }
    }
}
