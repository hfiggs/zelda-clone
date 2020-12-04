/* Author: Hunter Figgs */

namespace Game1.Command
{
    class Player2UpCommand : ICommand
    {
        private Game1 game;
        private const int playerIndex = 1;

        public Player2UpCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Screen.Players[playerIndex].MoveUp();
        }
    }
}
