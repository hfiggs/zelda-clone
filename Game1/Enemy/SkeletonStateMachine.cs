using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class SkeletonStateMachine
    {
        private IEnemyState state;
        private SpriteBatch spriteBatch;

        public SkeletonStateMachine(SpriteBatch spriteBatch, Vector2 position)
        {
            state = new SkeletonStateMoving(this, position);
            this.spriteBatch = spriteBatch;
        }

        public void BasicAttack()
        {
            state.Attack();
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
        }

        public void Draw()
        {
            state.Sprite.Draw(spriteBatch, state.GetPosition());
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);
        }
    }
}
