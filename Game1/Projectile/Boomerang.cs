using Game1.Player;
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
        private IPlayer player;
        private Point position;

        public Boomerang(char direction, IPlayer player) {
            this.direction = direction;
            this.player = player;
            position = player.GetLocation().Location;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            xModifier = 0;
            yModifier = 0;
            rowModifier = 0;
            counter = 0;
            returned = false;
        }
        public void Update() {
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
                currentLocation.Location = new Point(position.X + xModifier, position.Y + yModifier);
                Rectangle playerRectangle = player.GetLocation();

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
        public void Draw(SpriteBatch spriteBatch) {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
                Rectangle destinationRectangle = new Rectangle(position.X + xModifier, position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}
