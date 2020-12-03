using Game1.Audio;
using System.Diagnostics;

namespace Game1.Command
{
    class SelectPlayerTwoHUDCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public SelectPlayerTwoHUDCommand(Game1 game)
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
                    AudioManager.PlayFireForget("linkPop");
                    game.HUD.displayHUD2 = true;
                    game.HUD.displayHUD1 = false;
                }

                stopWatch.Restart();
            }
        }
    }
}
