/* Author: Hunter Figgs */

namespace Game1.Command
{
    class Player2RightCommand : ICommand
    {
        private Game1 game;

        public Player2RightCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Screen.Player2.MoveRight();
        }
    }
}
