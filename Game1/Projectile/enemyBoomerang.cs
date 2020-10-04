using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class EnemyBoomerang : IProjectile
    {
        private int rowModifier, counter;
        private float moveSpeed, totalElapsedGameTime;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private bool returned;
        private Vector2 position;

        public EnemyBoomerang(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            moveSpeed = 200;
            rowModifier = 0;
            totalElapsedGameTime = 0;
            counter = 0;
            returned = false;

        }
        public void Update(GameTime gameTime)
        {
            totalElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedGameTime < 1) {
                if (direction == 'N') {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'S') {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'W') {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            } else if (totalElapsedGameTime < 2) {
                if (direction == 'N') {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'S') {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'W') {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            // Stop drawing and updating position of boomerang if it has returned to its owner
            if (totalElapsedGameTime >= 2) {
                returned = true;
            }       

            // Used to change sprite sheet row to allow for flashing
            if (counter % 5 == 0) { 
                if (rowModifier == 3) {
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
    }
}
