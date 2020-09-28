/* Author: Hunter Figgs */

namespace Game1.Command
{
    class PlayerUpCommand : ICommand
    {
        private Game1 game;

        public PlayerUpCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Player.MoveUp();
        }
    }
}
