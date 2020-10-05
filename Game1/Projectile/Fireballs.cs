using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Projectile
{
    class Fireballs : IProjectile
    {
        private int rowModifier, counter;
        private ProjectileSpriteSheet sprite;
        private Vector2 position, directionOfPlayer;
        private float moveSpeed, slightChangeInY, topAndBottomModifier;

        public Fireballs(Vector2 position, Rectangle rec)
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            rowModifier = 0;
            counter = 0;
            topAndBottomModifier = 0;
            moveSpeed = 200;
            slightChangeInY = 100;
            this.position = position;
            directionOfPlayer = Vector2.Normalize(position - new Vector2(rec.X, rec.Y));
        }
        public bool Update(GameTime gameTime)
        {
            // Rounding is to make it less accurate so it's more like the game
            position.Y -= (float)Math.Round(directionOfPlayer.Y, 1) * moveSpeed * (float)Math.Round((float)gameTime.ElapsedGameTime.TotalSeconds, 2);
            position.X -= directionOfPlayer.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Used to change sprite sheet row and allow for flashing
            if (counter % 5 == 0) {
                if (rowModifier == 3) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }

            counter++;
            
            topAndBottomModifier += slightChangeInY * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //for now just return false, able to return true in the future when this needs to be removed from the projectiles list in game.
            return false;
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle middleFireball = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
            Rectangle upperFireball = new Rectangle((int)position.X, (int)(position.Y - topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
            Rectangle lowerFireball = new Rectangle((int)position.X, (int)(position.Y + topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch.Draw(sprite.GetTexture(), middleFireball, sourceRectangle, color);
            spriteBatch.Draw(sprite.GetTexture(), upperFireball, sourceRectangle, color);
            spriteBatch.Draw(sprite.GetTexture(), lowerFireball, sourceRectangle, color);
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
