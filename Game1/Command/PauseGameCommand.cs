/* Author: Hunter Figgs.3 */

using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class PauseGameCommand : ICommand
    {
        private Game1 game;
        private readonly Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public PauseGameCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();

        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (game.State is GameStatePaused)
                {
                    game.SetState(new GameStateRoom(game));
                }
                else if (game.State is GameStateRoom)
                {
                    game.SetState(new GameStatePaused(game));
                }

                stopWatch.Restart();
            }
        }
    }
}
