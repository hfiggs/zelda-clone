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
            if(game.Screen.CurrentRoom.InteractEnviornment.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnvironment first = game.Screen.CurrentRoom.InteractEnviornment.First();
                IEnvironment topFirst = game.Screen.CurrentRoom.NonInteractEnviornment.First();

                game.Screen.CurrentRoom.InteractEnviornment.RemoveFirst();
                game.Screen.CurrentRoom.NonInteractEnviornment.RemoveFirst();

                game.Screen.CurrentRoom.InteractEnviornment.AddLast(first);
                game.Screen.CurrentRoom.NonInteractEnviornment.AddLast(topFirst);

                stopWatch.Restart();
            }
        }
    }
}
