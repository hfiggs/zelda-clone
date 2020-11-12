/* Author: Hunter Figgs.3 */

using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class EnterHUDStateCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public EnterHUDStateCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();

        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                game.SetState(new GameStateRoomToHUD(game));

                stopWatch.Restart();
            }
        }
    }
}
