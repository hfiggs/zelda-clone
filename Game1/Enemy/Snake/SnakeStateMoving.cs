using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class SnakeStateMoving : IEnemyState
    {
        private Vector2 position;

        private Random rand;

        private bool isFacingLeft;

        // N=0, S=1, E=2, W=3
        private CompassDirection moveDirection;
        private bool playerSpotted;
        private Game1 game;

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const int normalSpeed = 1;
        private const int fastSpeed = 2;
        private const int viewWidth = 16;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        public ISprite Sprite { get; private set; }
        private IEnemy snake;

        public SnakeStateMoving(Game1 game, Vector2 position, IEnemy snake)
        {
            rand = new Random();

            const int randomDirectionFacingMax = 2;
            isFacingLeft = rand.Next(randomDirectionFacingMax) == 0;
            Sprite = isFacingLeft ? EnemySpriteFactory.Instance.CreateSnakeLeftSprite() : EnemySpriteFactory.Instance.CreateSnakeRightSprite();

            this.position = position;
            this.game = game;
            this.snake = snake;

            timeUntilNewDirection = moveTime;

            const int randomDirectionMax = 4;
            moveDirection = (CompassDirection)rand.Next(randomDirectionMax);

            timeUntilNextFrame = animationTime;

            playerSpotted = false;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, color);
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
            playerSpotted = false;
        }

        public Vector2 GetDirection()
        {
            return CompassDirectionUtil.GetDirectionVector(moveDirection);
        }

        public List<Rectangle> GetHitboxes()
        {
            const int widthAndHeight = 15;
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, widthAndHeight, widthAndHeight));
            return hitboxList;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if (snake.StunnedTimer == 0)
            {
                timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNewDirection <= 0 && !playerSpotted)
                {
                    const int randomNumberMax = 4;
                    moveDirection = (CompassDirection)rand.Next(randomNumberMax);

                    timeUntilNewDirection += moveTime;
                }

                if (isFacingLeft && moveDirection == CompassDirection.East)
                {
                    isFacingLeft = false;
                    Sprite = EnemySpriteFactory.Instance.CreateSnakeRightSprite();
                }
                else if (!isFacingLeft && moveDirection == CompassDirection.West)
                {
                    isFacingLeft = true;
                    Sprite = EnemySpriteFactory.Instance.CreateSnakeLeftSprite();
                }

                Vector2 windowDims = game.GetWindowDimensions();
                foreach (Rectangle playerRect in game.Screen.GetPlayerRectangle())
                {
                    if (isFacingLeft && playerRect.Intersects(new Rectangle((int)(position.X - windowDims.X), (int)position.Y, (int)windowDims.X, viewWidth)))
                    {
                        playerSpotted = true;
                        moveDirection = CompassDirection.West;
                    }
                    else if (!isFacingLeft && playerRect.Intersects(new Rectangle((int)position.X, (int)position.Y, (int)windowDims.X, viewWidth)))
                    {
                        playerSpotted = true;
                        moveDirection = CompassDirection.East;
                    }
                }

                var speed = playerSpotted ? fastSpeed : normalSpeed;

                position = Vector2.Add(position, Vector2.Multiply(CompassDirectionUtil.GetDirectionVector(moveDirection), speed));
            }

            timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }
    }
}
