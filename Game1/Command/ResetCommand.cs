/* Author: Hunter Figgs */

namespace Game1.Command
{
    class ResetCommand : ICommand
    {
        private Game1 game;

        public ResetCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Reset();
        }
    }
}
