using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Aquamentus : IEnemy
    {
        public ISprite Sprite { get; private set; }
        private float health;
        private IEnemyState state;
        private Game1 game;

        public Aquamentus(Game1 game, Vector2 position) {
            state = new AquamentusWalkLeft(game, this, position);
            health = 6f;
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

        public void editPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public bool shouldRemove()
        {
            return health <= 0;
        }

        public Rectangle GetHitbox()
        {
            return state.GetHitbox();
        }
    }
}
