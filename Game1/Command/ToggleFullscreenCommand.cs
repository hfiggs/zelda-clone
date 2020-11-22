/* Author: Hunter Figgs */

using Game1.ResolutionManager;
using System.Diagnostics;

namespace Game1.Command
{
    class ToggleFullscreenCommand : ICommand
    {
        private Game1 game;
        private readonly Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public ToggleFullscreenCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                game.ResolutionManager.ToggleFullscreen();

                stopWatch.Restart();
            }
        }
    }
}
