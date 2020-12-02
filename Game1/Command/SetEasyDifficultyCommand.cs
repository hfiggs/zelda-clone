/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.GameState;
using Game1.ResolutionManager;
using System.Diagnostics;

namespace Game1.Command
{
    class SetEasyDifficultyCommand : ICommand
    {
        private Game1 game;
        private readonly Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public SetEasyDifficultyCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                AudioManager.StopAllMusic();

                game.SetState(new GameStateStartToSpawn(game));

                game.Screen.LoadAllRooms(0);

                stopWatch.Restart();
            }
        }
    }
}
