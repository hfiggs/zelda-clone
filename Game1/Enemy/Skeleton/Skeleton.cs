using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Skeleton : IEnemy
    {
        private IEnemyState state;
        private Game1 game;
        private float health;

        public Skeleton(Game1 game, Vector2 spawnPosition)
        {
            state = new SkeletonStateMoving(spawnPosition);
            health = 2f;
            this.game = game;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, direction, game);
            game.Screen.CurrentRoom.EnemyList.Add(decorator);
            game.Screen.CurrentRoom.EnemyList.Remove(this);
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
