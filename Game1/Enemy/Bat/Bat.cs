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
        private Vector2 position;


        public Bat(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            this.position = spawnPosition;
            health = .5f;
            state = new EnemyStateSpawning(position, this, new BatStateMoving(spawnPosition));
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
