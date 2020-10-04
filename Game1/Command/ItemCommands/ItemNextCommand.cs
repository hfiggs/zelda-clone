﻿/* Author: Hunter */

using Game1.Item;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class ItemNextCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public ItemNextCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if(game.ItemList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IItem first = game.ItemList.First();

                game.ItemList.RemoveFirst();

                game.ItemList.AddLast(first);

                stopWatch.Restart();
            }
        }
    }
}
