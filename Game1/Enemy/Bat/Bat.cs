using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;

namespace Game1.Enemy
{
    class Bat : IEnemy
    {
        private IEnemyState state;
        private Game1 game;
        private float health;
        public Bat(Game1 game, Vector2 spawnPosition)
        {
            health = .5f;
            state = new BatStateMoving(spawnPosition);
            this.game = game;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, direction, game);
            game.EnemyList.AddLast(decorator);
            game.EnemyList.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, color);
        }

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
        public void editPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }
        public bool shouldRemove()
        {
            return health <= 0;
        }
    }
}
