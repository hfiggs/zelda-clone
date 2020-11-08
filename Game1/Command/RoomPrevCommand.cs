﻿using Game1.RoomLoading;
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
            game.Screen.CurrentRoom.StopRoomAmbience();

            if (game.Screen.RoomsList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                Room temp = game.Screen.RoomsList.Last();

                game.Screen.RoomsList.RemoveLast();

                game.Screen.RoomsList.AddFirst(temp);

                game.Screen.CurrentRoom = game.Screen.RoomsList.First();

                stopWatch.Restart();
            }

            game.Screen.CurrentRoom.PlayRoomAmbience();
        }
    }
}
