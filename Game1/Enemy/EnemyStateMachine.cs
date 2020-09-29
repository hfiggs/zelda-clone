using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class EnemyStateMachine
    {
        private Game1 game;

        private IEnemyState state;

        private SpriteBatch spriteBatch;

        public EnemyStateMachine(Game1 game, SpriteBatch spriteBatch)
        {
            this.game = game;

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

        public Vector2 GetDirection()
        {
            return state.GetDirection();
        }

        public Rectangle GetPlayerRectangle()
        {
            return game.GetPlayerRectangle();
        }

        public Vector2 GetWindowDimensions()
        {
            return game.GetWindowDimensions();
        }
    }
}
