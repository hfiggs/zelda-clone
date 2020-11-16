using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class Jelly : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private IEnemyState state;
        private Vector2 position;
        private Game1 game;
        private float health;

        public Jelly(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            this.position = spawnPosition;
            const float halfHeart = 0.5f;
            this.health = halfHeart;
            state = new EnemyStateSpawning(position, this, new JellyStateMoving(spawnPosition));
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, direction, game);
            game.Screen.CurrentRoom.DecoratedEnemyList.Add(decorator);

            if (health <= 0)
            {
                state = new EnemyStateDying(this, state.GetPosition());
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
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

        public void EditPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public bool ShouldRemove()
        {
            if (state.GetType() == typeof(EnemyStateDying))
            {
                EnemyStateDying temp = (EnemyStateDying)state;
                return temp.dead;
            }
            else
            {
                return false;
            }
        }
        public Vector2 GetPosition()
        {
            return state.GetPosition();
        }
        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }
    }
}
