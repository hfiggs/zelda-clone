using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Bat : IEnemy
    {
        private EnemyStateMachine stateMachine;
        public Bat(Game1 game, SpriteBatch spriteBatch, Vector2 spawnPosition)
        {
            stateMachine = new EnemyStateMachine(game, spriteBatch);
            stateMachine.SetState(new BatStateMoving(stateMachine, spawnPosition));
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
