using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Projectile
{
    class SwordBeam : IProjectile
    {
        private int xModifier, yModifier, columnModifier, counter, rowModifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private Point position;

        public SwordBeam(char direction, Point position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            xModifier = 0;
            yModifier = 0;
            columnModifier = 0;
            rowModifier = 0;
            counter = 0;
        }
        public void Update()
        {
            if (direction == 'N') {
                yModifier -= 5;
                rowModifier = 0;
            } else if (direction == 'S') {
                yModifier += 5;
                rowModifier = 1;
            } else if (direction == 'W') {
                xModifier -= 5;
                rowModifier = 2;
            } else if (direction == 'E') {
                xModifier += 5;
                rowModifier = 3;
            }

            // Used to change sprite sheet column and allow for flashing
            if (counter == 5) {
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
        public void Draw(SpriteBatch spriteBatch)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite + columnModifier, rowModifier); ;
            Rectangle destinationRectangle = new Rectangle(position.X + xModifier, position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
