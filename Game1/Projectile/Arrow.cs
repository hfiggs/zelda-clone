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
        private bool removeMe = false;

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
            Rectangle hitbox;

            if (direction == 'N' || direction == 'S')
            {
                hitbox = new Rectangle((int)position.X + 18, (int)position.Y + 12, 5, 16);
            }
            else
            {
                hitbox = new Rectangle((int)position.X + 11, (int)position.Y + 17, 19, 5);
            }

            return hitbox;
        }

        public bool ShouldDelete()
        {
            return removeMe;
        }

        public void BeginDespawn()
        {
            removeMe = true;
        }
    }
}
