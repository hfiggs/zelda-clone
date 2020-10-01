﻿/* Author: Hunter */

using Game1.Enemy;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class EnemyPrevCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public EnemyPrevCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if(game.EnemyList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnemy last = game.EnemyList.Last();

                game.EnemyList.RemoveLast();

                game.EnemyList.AddFirst(last);

                stopWatch.Restart();
            }
        }
    }
}