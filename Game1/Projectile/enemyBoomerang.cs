using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.CodeDom;

namespace Game1.Projectile
{
    class EnemyBoomerang : IProjectile
    {
        private int rowModifier, counter;
        private const float moveSpeed = 100;
        private float totalElapsedGameTime;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private ProjectileSpriteSheet sprite;
        private bool returned;
        private Vector2 position;
        private Vector2 originalPosition;

        public EnemyBoomerang(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
            this.originalPosition = position;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            rowModifier = 0;
            totalElapsedGameTime = 0;
            counter = 0;
            returned = false;

        }
        public void Update(GameTime gameTime)
        {
            const int widthAndHeight = 16;
            Rectangle origin = new Rectangle((int)originalPosition.X, (int)originalPosition.Y, widthAndHeight, widthAndHeight);
            // Stop drawing and updating position of boomerang if it has returned to its owner
            totalElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            const int oneSecond = 1, twoSeconds = 2;

            if (origin.Contains((int)position.X, (int)position.Y) && totalElapsedGameTime > oneSecond) {
                returned = true;
            }


            if (totalElapsedGameTime < oneSecond) {
                if (direction == north) {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == south) {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == west) {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == east)
                {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            } else if (totalElapsedGameTime < twoSeconds) {
                if (direction == north) {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == south) {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == west) {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == east)
                {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }



            // Used to change sprite sheet row to allow for flashing
            const int spriteChangeInterval = 5, rowMax = 3;

            if (counter % spriteChangeInterval == 0) { 
                if (rowModifier == rowMax) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }

            counter++;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
            }
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Rectangle GetHitbox()
        {
            const int xAndYDiff = 16, widthAndHeight = 8;
            return new Rectangle((int)position.X + xAndYDiff, (int)position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
        }

        public bool ShouldDelete()
        {
            return returned;
        }

        public void BeginDespawn()
        {
            totalElapsedGameTime = 1; // seconds
        }
    }
}
