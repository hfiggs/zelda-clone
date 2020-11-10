using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class SwordBeam : IProjectile
    {
        private int columnModifier, counter, rowModifier;
        private float totalTime;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private const float moveSpeed = 200;
        private const float delay = 200; // miliseconds
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private bool removeMe = false;
        private const float xOffset = -2.0f, yOffset = 3.0f;
        private readonly Vector2 positionOffset = new Vector2(xOffset, yOffset);

        private const float soundDelay = 0.2f;

        public SwordBeam(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position + positionOffset;
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            columnModifier = 0;
            rowModifier = 0;
            counter = 0;
            totalTime = 0;

            AudioManager.PlayFireForget("swordBeam", soundDelay);
        }
        public void Update(GameTime gameTime)
        {
            totalTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            const int northSprite = 0, southSprite = 1, westSprite = 2, eastSprite = 3;

            if (totalTime >= delay) {
                if (direction == north) {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = northSprite;
                } else if (direction == south) {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = southSprite;
                } else if (direction == west) {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = westSprite;
                } else if (direction == east) {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = eastSprite;
                }

                // Used to change sprite sheet column and allow for flashing
                const int counterMax = 5;
                if (counter == counterMax) {
                    if (columnModifier == 0) {
                        columnModifier = 1;
                    } else {
                        columnModifier = 0;
                    }
                    counter = 0;
                } else {
                    counter++;
                }
            }

            const int topBoundry = 10, botBoundry = 130, leftBoundry = 10, rightBoundry = 206;
            if(position.Y < topBoundry || position.Y > botBoundry || position.X < leftBoundry || position.X > rightBoundry)
            {
                BeginDespawn();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (totalTime >= delay) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite + columnModifier, rowModifier); ;
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
            Rectangle hitbox;

            if (direction == north || direction == south)
            {
                const int xdiff = 17, ydiff = 12, width = 7, height = 16;
                hitbox = new Rectangle((int)position.X + xdiff, (int)position.Y + ydiff, width, height);
            } else
            {
                const int xdiff = 11, ydiff = 16, width = 18, height = 7;
                hitbox = new Rectangle((int)position.X + xdiff, (int)position.Y + ydiff, width, height);
            }

            return hitbox;
        }
    }
}
