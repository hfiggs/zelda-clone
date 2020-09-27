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
        private SkeletonStateMachine stateMachine;
        public Skeleton(Vector2 position, SpriteBatch spriteBatch)
        {
            stateMachine = new SkeletonStateMachine(spriteBatch, position);
        }

        public void Attack()
        {
            stateMachine.BasicAttack();
        }

        public void ReceiveDamage()
        {
            stateMachine.ReceiveDamage();
        }

        public void Draw()
        {
            stateMachine.Draw();
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }
    }
}
