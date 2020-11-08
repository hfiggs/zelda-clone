﻿using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class GoriyaStateAttackingLeft : IEnemyState
    {
        public ISprite Sprite { get; private set; }

        private Vector2 direction;
        private Vector2 position;
        private const float moveSpeed = .5f;
        private const int zero = 0;
        private const int negative = -1;
        private IProjectile projectile;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds = 2;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public GoriyaStateAttackingLeft(Game1 game, Vector2 position)
        {
            this.position = position;
            this.direction = new Vector2(negative * moveSpeed, zero);
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaLeftSprite();
            const char boomerangDirection = 'W';
            projectile = new EnemyBoomerang(boomerangDirection, position);
            game.Screen.SpawnProjectile(projectile);

            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {

        }
        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                this.direction = GetRandomDirection();
                this.MovementChangeTimeSeconds = zero;
            }


            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= zero)
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
            return direction;
        }

        public List<Rectangle> GetHitboxes()
        {
            const int spriteWidth = 13;
            const int spriteHeight = 16;
            const int xAdjustment = 8;
            const int yAdjustment = 7;
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xAdjustment, (int)position.Y + yAdjustment, spriteWidth, spriteHeight));
            return hitboxList;
        }

        private float GetRandomDirectionMovementChangeTimeSeconds()
        {
            const double one = 1.0;
            const double minimumTime = 0.3;
            Random random = new Random();
            return (float) (random.NextDouble() * one + minimumTime);
        }

        private Vector2 GetRandomDirection()
        {
            const int numberOfDirections = 4;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int randomDirection = random.Next(numberOfDirections);

            switch (randomDirection)
            {
                case 0:
                    return new Vector2(negative * moveSpeed, zero);
                case 1:
                    return new Vector2(moveSpeed, zero);
                case 2:
                    return new Vector2(zero, negative * moveSpeed);
                default:
                    return new Vector2(zero, moveSpeed);
            }
        }

        public void editPosition(Vector2 amount)
        {
            //Do Nothing, No knockback while Attacking
        }
    }
}
