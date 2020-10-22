﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Goriya : IEnemy
    {
        private Vector2 oldDirection;
        private double totalElapsedSeconds;
        private double attackCooldown = 5;
        private IEnemyState state;
        private Game1 game;

        public Goriya(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            state = new GoriyaStateMovingRight(game, this, spawnPosition);
            oldDirection = state.GetDirection();
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            Vector2 newDirection = state.GetDirection();
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedSeconds >= attackCooldown)
            {
                state.Attack();
                totalElapsedSeconds -= attackCooldown;
            }

            else if (newDirection.X != oldDirection.X || newDirection.Y != oldDirection.Y) {
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

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }
    }
}

