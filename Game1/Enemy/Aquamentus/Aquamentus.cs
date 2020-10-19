using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Aquamentus : IEnemy
    {
        private EnemyStateMachine stateMachine;
        public ISprite Sprite { get; private set; }

        public Aquamentus(Game1 game, Vector2 position) {
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState(new AquamentusWalkLeft(game, stateMachine, position));
        }

        public void ReceiveDamage()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            stateMachine.Draw(spriteBatch, Color.White);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }

        public Rectangle GetHitbox()
        {
            return stateMachine.GetHitbox();
        }
    }
}
