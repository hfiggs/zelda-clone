using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Aquamentus : IEnemy
    {
        private EnemyStateMachine stateMachine;
        public ISprite Sprite { get; private set; }
        private float health;

        public Aquamentus(Game1 game, Vector2 position) {
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState(new AquamentusWalkLeft(game, stateMachine, position));
            health = 6f;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, stateMachine, direction);
            stateMachine.swapInList(this, decorator);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            stateMachine.Draw(spriteBatch, Color.White);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }

        public void editPosition(Vector2 amount)
        {
            stateMachine.editPosition(amount);
        }

        public bool shouldRemove()
        {
            return health <= 0;
        }

        public Rectangle GetHitbox()
        {
            return stateMachine.GetHitbox();
        }
    }
}
