using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    static class EnemyListFactory
    {
        public static LinkedList<IEnemy> GetEnemyList(Game1 game, SpriteBatch spriteBatch)
        {
            var enemyPosition = new Vector2(650, 300);

            var enemyList = new LinkedList<IEnemy>();
            enemyList.AddLast(new Bat(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new Dodongo(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new Goriya(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new Hand(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new Jelly(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new OldMan(spriteBatch, enemyPosition));
            enemyList.AddLast(new Skeleton(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new Snake(game, spriteBatch, enemyPosition));
            enemyList.AddLast(new SpikeTrap(game, spriteBatch, enemyPosition, 100, 100));
            enemyList.AddLast(new Merchant(spriteBatch, enemyPosition));
            // TODO: missing Aquamentus

            return enemyList;
        }
    }
}
