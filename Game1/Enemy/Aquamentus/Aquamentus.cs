using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy.Aquamentus
{
    class Aquamentus : IEnemy
    {
        private EnemyStateMachine stateMachine;
        public ISprite Sprite { get; private set; }

        public Aquamentus(Game1 game, SpriteBatch spriteBatch, Vector2 position) {
            stateMachine = new EnemyStateMachine(game, spriteBatch);
            stateMachine.SetState(new AquamentusWalkLeft(stateMachine, position));
        }

        public void Attack()
        {
        }

        public void Draw()
        {
            stateMachine.Draw(Color.White);
        }

        public void ReceiveDamage()
        {
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }
    }
}
