using Game1.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command
{
    class EnvironmentPrevCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public EnvironmentPrevCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public void Execute()
        {
            if (game.EnvironmentList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnvironment last = game.EnvironmentList.Last();

                game.EnvironmentList.RemoveLast();

                game.EnvironmentList.AddFirst(last);

                stopWatch.Restart();
            }
        }
    }
}
