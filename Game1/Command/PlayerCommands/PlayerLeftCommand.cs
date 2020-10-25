/* Author: Hunter Figgs */

namespace Game1.Command
{
    class PlayerLeftCommand : ICommand
    {
        private Game1 game;

        public PlayerLeftCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Screen.Player.MoveLeft();
        }
    }
}
