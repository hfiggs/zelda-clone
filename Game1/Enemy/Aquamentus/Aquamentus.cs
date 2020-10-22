using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Aquamentus : IEnemy
    {
        public ISprite Sprite { get; private set; }
        private IEnemyState state;

        public Aquamentus(Game1 game, Vector2 position) {
            state = new AquamentusWalkLeft(game, this, position);
        }

        public void ReceiveDamage()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, Color.White);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }
    }
}
