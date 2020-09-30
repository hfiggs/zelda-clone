using Game1.Environment;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class NextEnvironmentCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms
        public NextEnvironmentCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public void Execute()
        {
            if(game.environmentList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnvironment first = game.environmentList.First();

                game.environmentList.RemoveFirst();

                game.environmentList.AddLast(first);

                stopWatch.Restart();
            }
        }
    }
}
