using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class Goriya : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private Vector2 oldDirection;
        private double totalElapsedSeconds;
        private const double attackCooldown = 5;
        private IEnemyState state;
        private Game1 game;
        private Vector2 position;
        private const int zero = 0;

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
            game.Screen.CurrentRoom.DecoratedEnemyList.Add(decorator);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            if (StunnedTimer == zero)
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
                    if (state.GetDirection().X < zero)
                    {
                        state = new GoriyaStateMovingLeft(game, this, state.GetPosition());
                    }
                    if (state.GetDirection().X > zero)
                    {
                        state = new GoriyaStateMovingRight(game, this, state.GetPosition());
                    }
                    if (state.GetDirection().Y < zero)
                    {
                        state = new GoriyaStateMovingUp(game, this, state.GetPosition());
                    }
                    if (state.GetDirection().Y > zero)
                    {
                        state = new GoriyaStateMovingDown(game, this, state.GetPosition());
                    }
                    oldDirection = state.GetDirection();
                }

                state.Update(gameTime, drawingLimits);
            }

            StunnedTimer -= (StunnedTimer == int.MaxValue) ? zero : (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            StunnedTimer = Math.Max(zero, StunnedTimer);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }    

        public void EditPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public bool ShouldRemove()
        {
            return health <= zero;
        }
    }
}

