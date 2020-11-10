using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Enemy
{
    static class EnemyListFactory
    {
        public static LinkedList<IEnemy> GetEnemyList(Game1 game)
        {
            const int xValue = 175, yValue = 100;
            var enemyPosition = new Vector2(xValue, yValue);

            var enemyList = new LinkedList<IEnemy>();
            enemyList.AddLast(new Aquamentus(game, enemyPosition));
            
            return enemyList;
        }
    }
}
