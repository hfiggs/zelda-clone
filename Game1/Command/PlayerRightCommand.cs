/* Author: Hunter Figgs */

namespace Game1.Command
{
    class PlayerRightCommand : ICommand
    {
        private Game1 game;

        public PlayerRightCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Player.MoveRight();
        }
    }
}
