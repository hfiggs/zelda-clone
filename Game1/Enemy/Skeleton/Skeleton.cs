﻿using Game1.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class Skeleton : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private IEnemyState state;
        private Game1 game;
        private Vector2 positon;
        private float health;

        public Skeleton(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            this.positon = spawnPosition;
            state = new EnemyStateSpawning(positon, this, new SkeletonStateMoving(spawnPosition));
            health = 2f;
        }

        public Skeleton(Game1 game, Vector2 spawnPosition, IItem item)
        {
            this.game = game;
            this.positon = spawnPosition;
            health = 2f;
            state = new EnemyStateSpawning(positon, this, new SkeletonStateMoving(game, spawnPosition, item));

        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, direction, game);
            game.Screen.CurrentRoom.DecoratedEnemyList.Add(decorator);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, color);
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
