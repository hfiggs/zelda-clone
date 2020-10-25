using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Skeleton : IEnemy
    {
        private IEnemyState state;

        private EnemyStateMachine stateMachine;
        private float health;

        public Skeleton(Game1 game, Vector2 spawnPosition)
        {
            state = new SkeletonStateMoving(spawnPosition);
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState( new SkeletonStateMoving(stateMachine, spawnPosition));
            health = 2000f;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, stateMachine, direction);
            stateMachine.swapInList(this, decorator);
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
            stateMachine.editPosition(amount);
        }

        public bool shouldRemove()
        {
            return health <= 0;
        }
    }
}
