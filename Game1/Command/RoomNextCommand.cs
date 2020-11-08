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
            game.Screen.CurrentRoom.StopRoomAmbience();

            if(game.Screen.Rooms.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                Room temp = game.Screen.Rooms.First();

                game.Screen.Rooms.RemoveFirst();

                game.Screen.Rooms.AddLast(temp);

                game.Screen.CurrentRoom = game.Screen.Rooms.First();

                stopWatch.Restart();
            }

            game.Screen.CurrentRoom.PlayRoomAmbience();
        }
    }
}
