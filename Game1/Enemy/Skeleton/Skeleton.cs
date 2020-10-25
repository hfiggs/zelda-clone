﻿using Game1.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Skeleton : IEnemy
    {
        private IEnemyState state;
        private Game1 game;
        private Vector2 positon;
        public Skeleton(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            this.positon = spawnPosition;
            state = new SkeletonStateMoving(spawnPosition);
        }

        public Skeleton(Game1 game, Vector2 spawnPosition, IItem item)
        {
            this.game = game;
            this.positon = spawnPosition;
            state = new SkeletonStateMoving(game, spawnPosition, item);
        }

        public void SpawnAnimation()
        {
            SpawnDecorator decorator = new SpawnDecorator(this, positon, game);
            game.Screen.CurrentRoom.EnemyList.Add(decorator);
            game.Screen.CurrentRoom.EnemyList.Remove(this);
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
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
    }
}
