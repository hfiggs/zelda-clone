﻿using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class GoriyaStateAttackingDown : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private IEnemy goriya;

        private Vector2 direction;
        private Vector2 position;
        private const float moveSpeed = .5f;
        private const int negative = -1;
        private IProjectile projectile;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds = 2;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public GoriyaStateAttackingDown(Game1 game, IEnemy goriya, Vector2 position)
        {
            this.position = position;
            this.direction = new Vector2(0, moveSpeed);
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaDownSprite();
            const char boomerangDirection = 'S';
            projectile = new EnemyBoomerang(boomerangDirection, position);
            game.Screen.CurrentRoom.SpawnProjectile(projectile);

            timeUntilNextFrame = animationTime;

            this.goriya = goriya;
        }

        public void Attack()
        {

        }


        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            if (goriya.StunnedTimer == 0)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());

                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= MovementChangeTimeSeconds)
                {

                    totalElapsedSeconds -= MovementChangeTimeSeconds;
                    this.direction = GetRandomDirection();
                    this.MovementChangeTimeSeconds = 0;
                }
            }

            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

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

            const int goLeft = 0, goRight = 1, goUp = 2;
            switch (randomDirection)
            {
                case goLeft:
                    return new Vector2(negative * moveSpeed, 0);
                case goRight:
                    return new Vector2(moveSpeed, 0);
                case goUp:
                    return new Vector2(0, negative * moveSpeed);
                default:
                    return new Vector2(0, moveSpeed);
            }
        }

        public void editPosition(Vector2 amount)
        {
            //Do Nothing, No knockback while Attacking
        }
    }
}
