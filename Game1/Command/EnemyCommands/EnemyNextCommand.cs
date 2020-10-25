/* Author: Hunter */

using Game1.Enemy;
using System.Diagnostics;
using System.Linq;

namespace Game1.Command
{
    class EnemyNextCommand : ICommand
    {
        private Game1 game;

        private Stopwatch stopWatch;

        private const int cooldown = 250; // ms

        public EnemyNextCommand(Game1 game)
        {
            this.game = game;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if(game.Screen.CurrentRoom.EnemyList.Count > 1 && stopWatch.ElapsedMilliseconds >= cooldown)
            {
                IEnemy first = game.Screen.CurrentRoom.EnemyList.First();

                game.Screen.CurrentRoom.EnemyList.RemoveFi();

                game.Screen.CurrentRoom.EnemyList.Add(first);

                stopWatch.Restart();
            }
        }
    }
}
