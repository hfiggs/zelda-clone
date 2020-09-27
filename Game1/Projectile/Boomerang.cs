using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Projectile
{
    class Boomerang : IProjectile
    {
        private int xModifier, yModifier, counter, rowModifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private bool returned;
        private Vector2 position;

        public Boomerang(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            xModifier = 0;
            yModifier = 0;
            rowModifier = 0;
            counter = 0;
            returned = false;

        }
        public void Update()
        {
            if (counter < 100) {
                if (direction == 'N') {
                    yModifier -= 2;
                } else if (direction == 'S') {
                    yModifier += 2;
                } else if (direction == 'W') {
                    xModifier -= 2;
                } else {
                    xModifier += 2;
                }
            } else if (!returned) {
                Rectangle currentLocation = sprite.PickSprite(0, 0);
                currentLocation.Location = new Point((int)position.X + xModifier, (int)position.Y + yModifier);
                Rectangle playerRectangle = new Rectangle(0, 360, 50, 50); // TODO: initialize player rectangle

                if (currentLocation.X < playerRectangle.X) {
                    xModifier += 2;
                } else if (currentLocation.X > playerRectangle.X) {
                    xModifier -= 2;
                }

                if (currentLocation.Y < playerRectangle.Y) {
                    yModifier += 2;
                } else if (currentLocation.Y > playerRectangle.Y) {
                    yModifier -= 2;
                }

                returned = currentLocation.Intersects(playerRectangle);
            }

            counter++;

            // Used to change sprite sheet row to allow for flashing
            if (counter % 5 == 0) {
                if (rowModifier == 3) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
                Rectangle destinationRectangle = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}
