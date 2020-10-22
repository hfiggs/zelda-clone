/* Author: Hunter Figgs */

namespace Game1.Command
{
    class PlayerDownCommand : ICommand
    {
        private Game1 game;

        public PlayerDownCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Screen.Player.MoveDown();
        }
    }
}
