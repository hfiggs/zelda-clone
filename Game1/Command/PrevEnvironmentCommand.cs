using Game1.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command
{
    class PrevEnvironmentCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public PrevEnvironmentCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public void Execute()
        {
            if (game.environmentList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnvironment last = game.environmentList.Last();

                game.environmentList.RemoveLast();

                game.environmentList.AddFirst(last);

                stopWatch.Restart();
            }
        }
    }
}
