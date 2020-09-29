using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class Jelly : IEnemy
    {
        private EnemyStateMachine stateMachine;
        public Jelly(Game1 game, SpriteBatch spriteBatch, Vector2 spawnPosition)
        {
            stateMachine = new EnemyStateMachine(game, spriteBatch);
            stateMachine.SetState(new JellyStateMoving(stateMachine, spawnPosition));
        }

        public void Attack()
        {
            stateMachine.Attack();
        }

        public void ReceiveDamage()
        {
            stateMachine.ReceiveDamage();
        }

        public void Draw()
        {
            stateMachine.Draw(Color.White);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }
    }
}
