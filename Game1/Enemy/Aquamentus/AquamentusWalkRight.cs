﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class AquamentusWalkRight : IEnemyState
    {
        private Vector2 position;
        private float totalTime;
        private int timeCap;
        Random random;
        private const float moveSpeed = 7;
        Game1 game;
        IEnemy aquamentus;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame;
        private float animationTime = 150.0f;

        public AquamentusWalkRight(Game1 game, IEnemy aquamentus, Vector2 position) {
            Sprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
            this.position = position;
            random = new Random(Guid.NewGuid().GetHashCode());
            timeCap = random.Next(3);
            timeCap++;
            totalTime = 0;
            this.game = game;
            this.aquamentus = aquamentus;
            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {

        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            totalTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            
            if (totalTime <= timeCap) {
                if (drawingLimits.Contains(position.X + moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds, position.Y)) {
                    position.X += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                }
                if (random.Next(100) < 1) {
                    aquamentus.SetState(new AquamentusWalkRightAttack(game, aquamentus, position));
                }
            } else {
                aquamentus.SetState(new AquamentusWalkLeft(game, aquamentus, position));
            }

            timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, color);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(moveSpeed, 0);
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }

        public List<Rectangle> GetHitboxes()
        {
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, 25, 32));
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, 15, 12));
            return hitboxList;
        }
    }
}
