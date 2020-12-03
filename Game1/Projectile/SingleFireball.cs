using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Projectile
{
    class SingleFireball : IProjectile
    {
        private int rowModifier, counter;
        private ProjectileSpriteSheet sprite;
        private Vector2 position, directionOfPlayer;
        private const float moveSpeed = 100, slightChangeInY = 100;
        private float topAndBottomModifier;
        private bool removeMe = false;
        private const int yDiff = 20;

        public SingleFireball(Vector2 position, Rectangle rec)
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            rowModifier = 0;
            counter = 0;
            topAndBottomModifier = 0;
            this.position = position;
            directionOfPlayer = Vector2.Normalize(position - (new Vector2 (rec.Center.X, rec.Center.Y - yDiff)));
        }
        public void Update(GameTime gameTime)
        {
            position.Y -= directionOfPlayer.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= directionOfPlayer.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Used to change sprite sheet row and allow for flashing
            const int spriteChangeInterval = 5, rowMax = 3;

            if (counter % spriteChangeInterval == 0) {
                if (rowModifier == rowMax) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }

            counter++;
            
            topAndBottomModifier += slightChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle middleFireball = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(sprite.GetTexture(), middleFireball, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool ShouldDelete()
        {
            return removeMe;
        }

        public void BeginDespawn()
        {
            removeMe = true;
        }

        public Rectangle GetHitbox()
        {
            const int xdiff = 16, ydiff = 15, width = 8, height = 10;
            return new Rectangle((int)position.X + xdiff, (int)position.Y + ydiff, width, height);
        }
    }
}
