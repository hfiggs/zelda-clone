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
            this.health = .5f;
            state = new EnemyStateSpawning(position, this, new JellyStateMoving(spawnPosition));
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
            return health <= 0;
        }

        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }
    }
}
