using Game1.Environment;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class EnvironmentNextCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms
        public EnvironmentNextCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        public void Execute()
        {
            if(game.EnvironmentList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnvironment first = game.EnvironmentList.First();

                game.EnvironmentList.RemoveFirst();

                game.EnvironmentList.AddLast(first);

                stopWatch.Restart();
            }
        }
    }
}
