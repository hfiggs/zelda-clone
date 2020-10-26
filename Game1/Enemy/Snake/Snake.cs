using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Snake : IEnemy
    {
        IEnemyState state;

        private Game1 game;
        private float health;

        public Snake(Game1 game, Vector2 position)
        {
            this.game = game;

            state = new EnemyStateSpawning(position, this, new SnakeStateMoving(position));

            health = 0.5f;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, color);
        }

        public void ReceiveDamage(float amount, Vector2 direction) 
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, direction, game);
            game.Screen.CurrentRoom.EnemyList.Add(decorator);
            game.Screen.CurrentRoom.EnemyList.Remove(this);
        }

        public void editPosition( Vector2 amount)
        {
            state.editPosition(amount);
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

        public bool shouldRemove()
        {
            return health <= 0;
        }
    }
}
