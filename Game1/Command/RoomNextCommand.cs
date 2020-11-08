using Game1.RoomLoading;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class RoomNextCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public RoomNextCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if(game.Screen.RoomsList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                Room temp = game.Screen.RoomsList.First();

                game.Screen.RoomsList.RemoveFirst();

                game.Screen.RoomsList.AddLast(temp);

                game.Screen.CurrentRoom = game.Screen.RoomsList.First();

                stopWatch.Restart();
            }
        }
    }
}
