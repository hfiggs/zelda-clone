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
    class Fireballs : IProjectile
    {
        private int xModifier, yModifier, rowModifier, yDistance, xDistance, counter, topAndBottomModifier;
        private ProjectileSpriteSheet sprite;
        private Vector2 position;

        public Fireballs(Vector2 position, Rectangle rec)
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            xModifier = 0;
            yModifier = 0;
            rowModifier = 0;
            counter = 0;
            topAndBottomModifier = 0; // For top and bottom fireballs
            this.position = position;
            Rectangle playerRec = rec; // TODO: assign playerRec
            xDistance = (int)position.X - playerRec.X;
            yDistance = (int)position.Y - playerRec.Y;
        }
        public void Update()
        {
            // Cases for each fireball direction
            if (yDistance >= xDistance) {
                yModifier -= 2;
            } else if (yDistance >= xDistance/2) {
                yModifier -= 1;
            } else if (-yDistance >= xDistance) {
                yModifier += 2;
            } else if (-yDistance >= xDistance / 2) {
                yModifier += 1;
            }

            topAndBottomModifier++;

            // Used to change sprite sheet row and allow for flashing
            if (counter == 5) {
                if (rowModifier == 3) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }

                counter = 0;
            } else {
                counter++;
            }

            xModifier -= 5;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle middleFireball = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);
            Rectangle upperFireball = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier - topAndBottomModifier, sourceRectangle.Width, sourceRectangle.Height);
            Rectangle lowerFireball = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier + topAndBottomModifier, sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch.Draw(sprite.GetTexture(), middleFireball, sourceRectangle, Color.White);
            spriteBatch.Draw(sprite.GetTexture(), upperFireball, sourceRectangle, Color.White);
            spriteBatch.Draw(sprite.GetTexture(), lowerFireball, sourceRectangle, Color.White);
        }
    }
}
