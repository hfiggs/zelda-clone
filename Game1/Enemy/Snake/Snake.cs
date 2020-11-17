﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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

            state = new EnemyStateSpawning(position, this, new SnakeStateMoving(game, position));

            const float halfHeart = 0.5f;
            health = halfHeart;
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

            if (health <= 0)
            {
                state = new EnemyStateDying(this, state.GetPosition());
            }


        }

        public void EditPosition( Vector2 amount)
        {
            if (StunnedTimer <= 0)
            {
                state.editPosition(amount);
            }
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

        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }

        public Vector2 GetPosition()
        {
            return state.GetPosition();
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
    }
}
