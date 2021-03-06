﻿/* Author: Hunter Figgs.3 */

using Game1.GameState;
using Game1.HUD;
using System.Diagnostics;

namespace Game1.Command
{
    class ExitHUDStateCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public ExitHUDStateCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                game.SetState(new GameStateHUDToRoom(game));

                stopWatch.Restart();
            }
        }
    }
}
