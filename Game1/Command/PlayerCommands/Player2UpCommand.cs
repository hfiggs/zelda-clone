/* Author: Hunter Figgs */

namespace Game1.Command
{
    class Player2UpCommand : ICommand
    {
        private Game1 game;

        public Player2UpCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Screen.Player2.MoveUp();
        }
    }
}
