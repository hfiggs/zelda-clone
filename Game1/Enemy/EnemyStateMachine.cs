using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class EnemyStateMachine
    {
        IEnemyState state;

        SpriteBatch spriteBatch;

        public EnemyStateMachine(SpriteBatch spriteBatch, IEnemyState state)
        {
            this.state = state;

            this.spriteBatch = spriteBatch;
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public void Draw(Color color)
        {
            state.Sprite.Draw(spriteBatch, state.GetPosition(), color);
        }

        public void Attack()
        {
            state.Attack();
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            state.Update(gametime, drawingLimits);
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
        }

        public Vector2 GetPosition()
        {
            return state.GetPosition();
        }
    }
}
