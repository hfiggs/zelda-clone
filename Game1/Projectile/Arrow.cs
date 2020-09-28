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
    class Arrow : IProjectile
    {
        private int xModifier, yModifier, rowModifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private Point position;

        public Arrow(char direction, Point position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateArrowSprite();
            xModifier = 0;
            yModifier = 0;
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
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle destinationRectangle = new Rectangle(position.X + xModifier, position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
