using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class Fireballs : IProjectile
    {
        private int rowModifier, yDistance, xDistance, counter;
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private float moveSpeed, slightChangeInY, largeChangeInY, topAndBottomModifier;

        public Fireballs(Vector2 position, Rectangle rec)
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            rowModifier = 0;
            counter = 0;
            topAndBottomModifier = 0;
            moveSpeed = 400;
            slightChangeInY = 100;
            largeChangeInY = 200;
            this.position = position;
            Rectangle playerRec = rec;
            xDistance = (int)position.X - playerRec.X;
            yDistance = (int)position.Y - playerRec.Y;
        }
        public void Update(GameTime gameTime)
        {
            // Cases for each fireball direction
            if (yDistance >= xDistance) {
                position.Y -= largeChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if (yDistance >= xDistance/2) {
                position.Y -= slightChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if (-yDistance >= xDistance) {
                position.Y += largeChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if (-yDistance >= xDistance / 2) {
                position.Y += slightChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Used to change sprite sheet row and allow for flashing
            if (counter % 5 == 0) {
                if (rowModifier == 3) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }

            counter++;

            position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            topAndBottomModifier += slightChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle middleFireball = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
            Rectangle upperFireball = new Rectangle((int)position.X, (int)(position.Y - topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
            Rectangle lowerFireball = new Rectangle((int)position.X, (int)(position.Y + topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch.Draw(sprite.GetTexture(), middleFireball, sourceRectangle, Color.White);
            spriteBatch.Draw(sprite.GetTexture(), upperFireball, sourceRectangle, Color.White);
            spriteBatch.Draw(sprite.GetTexture(), lowerFireball, sourceRectangle, Color.White);
        }
    }
}
