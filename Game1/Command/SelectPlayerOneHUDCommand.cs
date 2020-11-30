﻿using System.Diagnostics;

namespace Game1.Command
{
    class SelectPlayerOneHUDCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

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
                    game.HUD.displayHUD1 = true;
                    game.HUD.displayHUD2 = false;
                }

                stopWatch.Restart();
            }
        }
    }
}