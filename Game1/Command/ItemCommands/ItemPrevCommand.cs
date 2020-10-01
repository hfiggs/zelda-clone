/* Author: Hunter */

using Game1.Item;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class ItemPrevCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public ItemPrevCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if(game.ItemList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IItem last = game.ItemList.Last();

                game.ItemList.RemoveLast();

                game.ItemList.AddFirst(last);

                stopWatch.Restart();
            }
        }
    }
}
