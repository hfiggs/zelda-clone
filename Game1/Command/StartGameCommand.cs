﻿/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class StartGameCommand : ICommand
    {
        Game1 game;
        private Stopwatch stopWatch;
        private const int cooldown = 250; // ms

        public StartGameCommand(Game1 game)
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

                stopWatch.Restart();
            }
        }
    }
}