using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class Arrow : IProjectile
    {
        private int rowModifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private float moveSpeed;

        public Arrow(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateArrowSprite();
            moveSpeed = 500;
        }
        public void Update(GameTime gameTime)
        {
            if (direction == 'N') {
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = 0;
            } else if (direction == 'S') {
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = 1;
            } else if (direction == 'W') {
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = 2;
            } else if (direction == 'E') {
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = 3;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
        }
    }
}
