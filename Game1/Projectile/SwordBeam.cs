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
        private int modifier, column;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;

        public SwordBeam(char direction)
        {
            this.direction = direction;
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            modifier = 0;
            column = 1;
        }
        public void Update()
        {
            if (direction = 'N' || direction = 'W')
            {
                modifier -= 2;
            }
            else
            {
                modifier += 2;
            }

            // Used to change sprite sheet column and allow for flashing
            if (column == 1)
            {
                column = 2;
            } else {
                column = 1;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;

            switch (direction)
            {
                case 'N':
                    sourceRectangle = sprite.PickSprite(column, 0);
                    destinationRectangle = new Rectangle((int)position.X, (int)position.Y + modifier, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                case 'S':
                    sourceRectangle = sprite.PickSprite(column, 1);
                    destinationRectangle = new Rectangle((int)position.X, (int)position.Y + modifier, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                case 'W':
                    sourceRectangle = sprite.PickSprite(column, 2);
                    destinationRectangle = new Rectangle((int)position.X + modifier, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                case 'E':
                    sourceRectangle = sprite.PickSprite(column, 3);
                    destinationRectangle = new Rectangle((int)position.X + modifier, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                default:
                    break;
            }

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
