using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class Skeleton : IEnemy
    {
        public Skeleton(Vector2 position, SpriteBatch spriteBatch)
        {
            EnemyStateFactory.Instance.InitializeSkeleton(spriteBatch, position);
        }

        public void Attack()
        {
            EnemyStateFactory.Instance.BasicAttack();
        }

        public void ReceiveDamage()
        {
            EnemyStateFactory.Instance.ReceiveDamage();
        }

        public void Draw()
        {
            EnemyStateFactory.Instance.Draw();
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            EnemyStateFactory.Instance.Update(gameTime, drawingLimits);
        }
    }
}
