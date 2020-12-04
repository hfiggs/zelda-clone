using Game1.Audio;
using System.Diagnostics;

namespace Game1.Command
{
    class SelectPlayerOneHUDCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms
        private float soundVolume = 0.5f;

        public SelectPlayerOneHUDCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (game.HUD.twoPlayers)
                {
                    AudioManager.PlayFireForget("bombPlace", 0.0f, soundVolume);
                    game.HUD.displayHUD1 = true;
                    game.HUD.displayHUD2 = false;
                }

                stopWatch.Restart();
            }
        }
    }
}
