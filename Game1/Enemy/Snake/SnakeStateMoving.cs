using Game1.Sprite;
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
        private int moveDirection;
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

        public SnakeStateMoving(Game1 game, Vector2 position)
        {
            rand = new Random();

            const int randomDirectionFacingMax = 2;
            isFacingLeft = rand.Next(randomDirectionFacingMax) == 0;
            Sprite = isFacingLeft ? EnemySpriteFactory.Instance.CreateSnakeLeftSprite() : EnemySpriteFactory.Instance.CreateSnakeRightSprite();

            this.position = position;
            this.game = game;

            timeUntilNewDirection = moveTime;

            const int randomDirectionMax = 4;
            moveDirection = rand.Next(randomDirectionMax);

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
            // N=0, S=1, E=2, W=3
            switch (moveDirection)
            {
                case 0:
                    return new Vector2(0, -1);
                case 1:
                    return new Vector2(0, 1);
                case 2:
                    return new Vector2(1, 0);
                case 3:
                default:
                    return new Vector2(-1, 0);
            }
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
            const int right = 2, left = 3;

            timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNewDirection <= 0 && !playerSpotted)
            {
                const int randomNumberMax = 4;
                moveDirection = rand.Next(randomNumberMax);

                timeUntilNewDirection += moveTime;
            }

            if (isFacingLeft && moveDirection == right)
            {
                isFacingLeft = false;
                Sprite = EnemySpriteFactory.Instance.CreateSnakeRightSprite();
            }
            else if (!isFacingLeft && moveDirection == left)
            {
                isFacingLeft = true;
                Sprite = EnemySpriteFactory.Instance.CreateSnakeLeftSprite();
            }

            Vector2 windowDims = game.GetWindowDimensions();
            if (isFacingLeft && game.Screen.GetPlayerRectangle().Intersects(new Rectangle((int)(position.X - windowDims.X), (int)position.Y, (int)windowDims.X, viewWidth))) {
                playerSpotted = true;
                moveDirection = left;
            } else if (!isFacingLeft && game.Screen.GetPlayerRectangle().Intersects(new Rectangle((int)position.X, (int)position.Y, (int)windowDims.X, viewWidth))) {
                playerSpotted = true;
                moveDirection = right;
            }

            int speed;
            if (playerSpotted) {
                speed = fastSpeed;
            } else {
                speed = normalSpeed;
            }

            switch (moveDirection)
            {
                case 0:
                    position.Y -= speed;
                    break;
                case 1:
                    position.Y += speed;
                    break;
                case 2:
                    position.X += speed;
                    break;
                case 3:
                    position.X -= speed;
                    break;
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
