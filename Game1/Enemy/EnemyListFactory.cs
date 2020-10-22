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
            enemyList.AddLast(new Aquamentus(game, enemyPosition));
            enemyList.AddLast(new Dodongo(game, enemyPosition));
            enemyList.AddLast(new Bat(game, enemyPosition));
            enemyList.AddLast(new Goriya(game, enemyPosition));
            enemyList.AddLast(new Hand(game, enemyPosition));
            enemyList.AddLast(new Jelly(game, enemyPosition));
            enemyList.AddLast(new OldMan(enemyPosition));
            enemyList.AddLast(new Skeleton(game, enemyPosition));
            enemyList.AddLast(new Snake(game, enemyPosition));
            enemyList.AddLast(new SpikeTrap(game, enemyPosition, 100, 100));
            enemyList.AddLast(new Merchant(enemyPosition));
            
            return enemyList;
        }
    }
}
