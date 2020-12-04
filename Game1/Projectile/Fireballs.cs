using Game1.Util;
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
        private const float moveSpeed = 100, slightChangeInY = 100, largeChangeInYMultiplier = 2;
        private float topAndBottomModifier;
        private bool removeMe = false;
        private const char top = 'T', bottom = 'B', middle = 'M';
        private const int yOffset = 20, xOffset = -12, positionDifferenceMin = 15;
        private char topBottomOrMiddle; // 'T' = Top, 'B' = Bottom, 'M' = Middle
        private float playerXPositionDifference;
        private bool smallPositionDifference;

        public Fireballs(Vector2 position, Rectangle rec, char topBottomOrMiddle)
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            rowModifier = 0;
            counter = 0;
            topAndBottomModifier = 0;
            this.position = position;
            this.topBottomOrMiddle = topBottomOrMiddle;

            Vector2 playerPosition = new Vector2(rec.Center.X, rec.Center.Y - yOffset);
            directionOfPlayer = new Vector2(position.X, position.Y) - playerPosition;
            directionOfPlayer = Vector2.Normalize(directionOfPlayer);

            playerXPositionDifference = Math.Abs(position.X - playerPosition.X);
            smallPositionDifference = playerXPositionDifference <= positionDifferenceMin;
        }
        public void Update(GameTime gameTime)
        {
            float xVelocity = directionOfPlayer.X * moveSpeed;

            if (Math.Abs(xVelocity) < 100 && xVelocity > 0 && !smallPositionDifference) {
                xVelocity = moveSpeed;
            } else if (Math.Abs(xVelocity) < 100 && xVelocity < 0 && !smallPositionDifference) {
                xVelocity = -1 * moveSpeed;
            }

            position.Y -= directionOfPlayer.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= xVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            
            switch (topBottomOrMiddle)
            {
                case top:
                    Rectangle topFireball;
                    if (smallPositionDifference && directionOfPlayer.Y > 0) {
                        topFireball = new Rectangle((int)position.X + xOffset, (int)(position.Y - largeChangeInYMultiplier * topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
                    } else if (smallPositionDifference && directionOfPlayer.Y < 0) {
                        topFireball = new Rectangle((int)position.X + xOffset, (int)(position.Y + topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
                    } else {
                        topFireball = new Rectangle((int)position.X, (int)(position.Y - topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
                    }
                    spriteBatch.Draw(sprite.GetTexture(), topFireball, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
                    break;
                case bottom:
                    Rectangle lowerFireball;
                    if (smallPositionDifference && directionOfPlayer.Y < 0) {
                        lowerFireball = new Rectangle((int)position.X + xOffset, (int)(position.Y + largeChangeInYMultiplier * topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
                    } else if (smallPositionDifference && directionOfPlayer.Y > 0) {
                        lowerFireball = new Rectangle((int)position.X + xOffset, (int)(position.Y - topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
                    } else {
                        lowerFireball = new Rectangle((int)position.X, (int)(position.Y + topAndBottomModifier), sourceRectangle.Width, sourceRectangle.Height);
                    }
                    spriteBatch.Draw(sprite.GetTexture(), lowerFireball, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
                    break;
                case middle:
                    Rectangle middleFireball;
                    if (smallPositionDifference) {
                        middleFireball = new Rectangle((int)position.X + xOffset, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                    } else {
                        middleFireball = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                    }
                    spriteBatch.Draw(sprite.GetTexture(), middleFireball, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
                    break;
                default:
                    break;
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
            Rectangle hitbox;
            switch (topBottomOrMiddle)
            {
                case top:
                    if (smallPositionDifference && directionOfPlayer.Y > 0) {
                        hitbox = new Rectangle((int)position.X + xdiff + xOffset, (int)(position.Y + ydiff - largeChangeInYMultiplier * topAndBottomModifier), width, height);
                    } else if (smallPositionDifference && directionOfPlayer.Y < 0) {
                        hitbox = new Rectangle((int)position.X + xdiff + xOffset, (int)(position.Y + ydiff + topAndBottomModifier), width, height);
                    } else {
                        hitbox = new Rectangle((int)position.X + xdiff, (int)(position.Y + ydiff - topAndBottomModifier), width, height);
                    }
                    break;
                case bottom:
                    if (smallPositionDifference && directionOfPlayer.Y < 0) {
                        hitbox = new Rectangle((int)position.X + xdiff + xOffset, (int)(position.Y + ydiff + largeChangeInYMultiplier * topAndBottomModifier), width, height);
                    } else if (smallPositionDifference && directionOfPlayer.Y > 0) {
                        hitbox = new Rectangle((int)position.X + xdiff + xOffset, (int)(position.Y + ydiff - topAndBottomModifier), width, height);
                    } else {
                        hitbox = new Rectangle((int)position.X + xdiff, (int)(position.Y + ydiff + topAndBottomModifier), width, height);
                    }
                    break;
                case middle:
                default:
                    if (smallPositionDifference) {
                        hitbox = new Rectangle((int)position.X + xdiff + xOffset, (int)position.Y + ydiff, width, height);
                    } else {
                        hitbox = new Rectangle((int)position.X + xdiff, (int)position.Y + ydiff, width, height);
                    }
                    break;
            }
            return hitbox;
        }

        public void EditPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
