using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy.Snake
{
    class Snake : IEnemy
    {
        private Game1 game;

        ISprite sprite;
        SpriteBatch spriteBatch;

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
        private const int fastSpeed = 2;

        public Snake(Game1 game, SpriteBatch spriteBatch, Vector2 position)
        {
            this.game = game;

            this.spriteBatch = spriteBatch;

            rand = new Random();

            isFacingLeft = rand.Next(2) == 0;
            sprite = isFacingLeft ? EnemySpriteFactory.Instance.CreateSnakeLeftSprite() : EnemySpriteFactory.Instance.CreateSnakeRightSprite();

            this.position = position;

            timeUntilNewDirection = moveTime;

            moveDirection = rand.Next(4);

            timeUntilNextFrame = animationTime;
        }

        public void Attack() { /* Cannot attack */ }

        public void Draw()
        {
            sprite.Draw(spriteBatch, position, Color.White);
        }

        public void ReceiveDamage()  { /* Cannot receive damage */ }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            timeUntilNewDirection -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNewDirection <= 0)
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

            // TODO: determine if player is in front of snake and if so then speed = fastSpeed
            int speed = normalSpeed;

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
    }
}
