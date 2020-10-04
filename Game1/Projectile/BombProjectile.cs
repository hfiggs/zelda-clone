using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class BombProjectile : IProjectile
    {
        private float detonationTime, timer;
        private bool detonated;
        private ProjectileSpriteSheet sprite;
        private Vector2 position;

        public BombProjectile(Vector2 position)
        {
            this.position = position;
            detonated = false;
            detonationTime = 70;
            timer = 0;
            sprite = ProjectileSpriteFactory.Instance.CreateBombProjectileSprite();
        }
        public void Update(GameTime gameTime)
        {
            timer += detonationTime * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 70) {
                detonated = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (!detonated) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, 0);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
            }
        }
    }
}

