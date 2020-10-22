using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Snake : IEnemy
    {
        ISprite sprite;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private Vector2 position;

        private Random rand;

        private bool isFacingLeft;
        // N=0, S=1, E=2, W=3
        private int moveDirection;

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const int normalSpeed = 1;
        private const int fastSpeed = 4;
        private const int viewWidth = 10;
        public bool playerSpotted { private get; set; }
        Rectangle playerRect;
        Vector2 windowDims;

        public Snake(Game1 game, Vector2 position) {
            rand = new Random();

            isFacingLeft = rand.Next(2) == 0;
            sprite = isFacingLeft ? EnemySpriteFactory.Instance.CreateSnakeLeftSprite() : EnemySpriteFactory.Instance.CreateSnakeRightSprite();

            this.position = position;

            timeUntilNewDirection = moveTime;

            moveDirection = rand.Next(4);

            timeUntilNextFrame = animationTime;
            playerSpotted = false;
            playerRect = game.GetPlayerRectangle();
            windowDims = game.GetWindowDimensions();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch,position,color);
        }

        public void ReceiveDamage()  { /* TODO: Receive damage */ }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            timeUntilNewDirection -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNewDirection <= 0 && !playerSpotted)
            {
                moveDirection = rand.Next(4);

                timeUntilNewDirection += moveTime;
            }

            if (isFacingLeft && moveDirection == 2)
            {
                isFacingLeft = false;
                sprite = EnemySpriteFactory.Instance.CreateSnakeRightSprite();
            }
            else if (!isFacingLeft && moveDirection == 3)
            {
                isFacingLeft = true;
                sprite = EnemySpriteFactory.Instance.CreateSnakeLeftSprite();
            }

            if (isFacingLeft && playerRect.Intersects(new Rectangle((int)(position.X - windowDims.X), (int)position.Y, (int)windowDims.X, viewWidth))) {
                playerSpotted = true;
                moveDirection = 3;
            } else if (!isFacingLeft && playerRect.Intersects(new Rectangle((int)position.X, (int)position.Y, (int)windowDims.X, viewWidth))) {
                playerSpotted = true;
                moveDirection = 2;
            }

            int speed = normalSpeed;
            if (playerSpotted) {
                speed = fastSpeed;
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

            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void SetState(IEnemyState state)
        {
            // Do Nothing
        }
    }
}
