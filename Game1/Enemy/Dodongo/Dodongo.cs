using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Dodongo : IEnemy
    {
        private IEnemyState state;

        public Dodongo(Game1 game, Vector2 position)
        {
            switch ((new Random()).Next(4))
            {
                case 0:
                    state = new DodongoStateUp(this, position);
                    break;
                case 1:
                    state = new DodongoStateDown(this, position);
                    break;
                case 2:
                    state = new DodongoStateLeft(this, position);
                    break;
                case 3:
                    state = new DodongoStateRight(this, position);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, color);
        }

        public void ReceiveDamage() {  /* TODO: Receive damage */ }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public Rectangle GetHitbox()
        {
            return state.GetHitbox();
        }
    }
}
