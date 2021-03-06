﻿using Game1.Item;
using Game1.RoomLoading;
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
        const float twoHearts = 2.0f;

        public Skeleton(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            positon = spawnPosition;
            state = new EnemyStateSpawning(positon, this, new SkeletonStateMoving(spawnPosition, this));
            health = twoHearts;
        }

        public Skeleton(Game1 game, Room room, Vector2 spawnPosition, IItem item)
        {
            this.game = game;
            positon = spawnPosition;
            health = twoHearts;
            state = new EnemyStateSpawning(positon, this, new SkeletonStateMoving(room, spawnPosition, item, this));
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
            state.Draw(spriteBatch, color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);

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
           if(StunnedTimer <= 0)
            {
                state.editPosition(amount);
            }
            
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
