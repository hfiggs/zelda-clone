using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Enemy
{
    static class EnemyListFactory
    {
        public static LinkedList<IEnemy> GetEnemyList(Game1 game)
        {
            var enemyPosition = new Vector2(175, 100);

            var enemyList = new LinkedList<IEnemy>();
            enemyList.AddLast(new Skeleton(game, enemyPosition));
            
            return enemyList;
        }
    }
}
