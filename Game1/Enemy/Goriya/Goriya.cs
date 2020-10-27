using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Goriya : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private Vector2 oldDirection;
        private double totalElapsedSeconds;
        private double attackCooldown = 5;
        private IEnemyState state;
        private Game1 game;
        private Vector2 position;

        private float health;

        public Goriya(Game1 game, Vector2 spawnPosition)
        {
            health = 3f;
            this.game = game;
            this.position = spawnPosition;
            state = new EnemyStateSpawning(position, this, new GoriyaStateMovingRight(game, this, spawnPosition));
            oldDirection = state.GetDirection();
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
            state.Draw(spriteBatch,color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            if (StunnedTimer == 0)
            {
                Vector2 newDirection = state.GetDirection();
                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= attackCooldown)
                {
                    state.Attack();
                    totalElapsedSeconds -= attackCooldown;
                }

                else if (newDirection.X != oldDirection.X || newDirection.Y != oldDirection.Y)
                {
                    if (state.GetDirection().X < 0)
                    {
                        state = new GoriyaStateMovingLeft(game, this, state.GetPosition());
                    }
                    if (state.GetDirection().X > 0)
                    {
                        state = new GoriyaStateMovingRight(game, this, state.GetPosition());
                    }
                    if (state.GetDirection().Y < 0)
                    {
                        state = new GoriyaStateMovingUp(game, this, state.GetPosition());
                    }
                    if (state.GetDirection().Y > 0)
                    {
                        state = new GoriyaStateMovingDown(game, this, state.GetPosition());
                    }
                    oldDirection = state.GetDirection();
                }

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

        public void EditPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public bool ShouldRemove()
        {
            return health <= 0;
        }
    }
}

