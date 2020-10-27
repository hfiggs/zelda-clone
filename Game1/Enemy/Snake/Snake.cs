using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Snake : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private IEnemyState state;

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
            game.Screen.CurrentRoom.DecoratedEnemyList.Add(decorator);
        }

        public void EditPosition( Vector2 amount)
        {
            state.editPosition(amount);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            if (StunnedTimer == 0)
            {
                state.Update(gameTime, drawingLimits);
            }

            StunnedTimer -= (StunnedTimer == int.MaxValue) ? 0 : (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            StunnedTimer = Math.Max(0, StunnedTimer);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public Rectangle GetHitbox()
        {
            return state.GetHitbox();
        }

        public bool ShouldRemove()
        {
            return health <= 0;
        }
    }
}
