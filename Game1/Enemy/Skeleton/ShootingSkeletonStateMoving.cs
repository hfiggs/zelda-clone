using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Game1.Item;
using System.Collections.Generic;
using Game1.RoomLoading;
using Game1.Player;
using Game1.Environment;
using Game1.Projectile;

namespace Game1.Enemy
{
    class ShootingSkeletonStateMoving : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private IEnemy skeleton;
        private IPlayer player;
        private Screen screen;
        private Vector2 position;
        private Vector2 direction;
        private const float moveSpeed = .8f;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds = 0.8;
        private const char middle = 'M';

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public ShootingSkeletonStateMoving(Vector2 position, IEnemy skeleton, Screen screen)
        {
            this.Sprite = EnemySpriteFactory.Instance.CreateRedSkeletonSprite();

            this.position = position;
            this.player = screen.Players[0];
            this.screen = screen;
            this.direction = new Vector2(0,0);

            this.timeUntilNextFrame = animationTime;

            this.skeleton = skeleton;
        }

        public void Attack()
        {

        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            if (skeleton.StunnedTimer == 0)
            {
                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= MovementChangeTimeSeconds)
                {
                    totalElapsedSeconds -= MovementChangeTimeSeconds;
                    direction = GetRandomDirection();
                    screen.CurrentRoom.SpawnProjectile(new Fireballs(position, player.GetPlayerHitbox(), middle));
                }
                if (drawingLimits.Contains(position.X + direction.X, position.Y + direction.Y))
                {
                    position += direction;
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

        public ISprite GetSprite()
        {
            return this.Sprite;
        } 

        public Vector2 GetDirection()
        {
            return direction;
        }
        public Vector2 GetPosition()
        {
            return position;
        }

        public List<Rectangle> GetHitboxes()
        {
            const int xAndYDiff = 7;
            const int width = 15;
            const int height = 16;
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xAndYDiff, (int)position.Y + xAndYDiff, width, height));
            return hitboxList;
        }

        private Vector2 GetRandomDirection()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const int randomNumberMax = 4;
            int randomDirection = random.Next(randomNumberMax);

            const int goLeft = 0, goRight = 1, goUp = 2;
            switch (randomDirection)
            {
                case goLeft:
                    return new Vector2(-1 * moveSpeed, 0);
                case goRight:
                    return new Vector2(moveSpeed, 0);
                case goUp:
                    return new Vector2(0, -1 * moveSpeed);
                default:
                    return new Vector2(0, moveSpeed);
            }
        }
        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }
    }
}
