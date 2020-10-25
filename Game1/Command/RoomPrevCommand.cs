using Game1.RoomLoading;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class RoomPrevCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public RoomPrevCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (game.Screen.Rooms.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                Room temp = game.Screen.Rooms.Last();

                game.Screen.Rooms.RemoveLast();

                game.Screen.Rooms.AddFirst(temp);

                game.Screen.CurrentRoom = game.Screen.Rooms.First();

                stopWatch.Restart();
            }
        }
    }
}
