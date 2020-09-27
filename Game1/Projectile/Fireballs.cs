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
    class Fireballs : IProjectile
    {
        private int xModifier, yModifier, rowModifier, yDistance, xDistance;
        private ProjectileSpriteSheet sprite;
        private Vector2 position;

        public Fireballs(Vector2 position)
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            xModifier = 0;
            yModifier = 0;
            rowModifier = 0;
            this.position = position;
            Rectangle playerRec = new Rectangle(0, 0, 0, 0); // TODO: assign playerRec
            xDistance = (int)position.X - (int)playerRec.X;
            yDistance = (int)position.Y - (int)playerRec.Y;
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

            // Used to change sprite sheet row and allow for flashing
            if (rowModifier == 3) {
                rowModifier = 0;
            } else {
                rowModifier++;
            }

            xModifier -= 2;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle middleFireball = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);
            Rectangle upperFireball = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier - 1, sourceRectangle.Width, sourceRectangle.Height);
            Rectangle lowerFireball = new Rectangle((int)position.X + xModifier, (int)position.Y + yModifier + 1, sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch.Draw(sprite.GetTexture(), middleFireball, sourceRectangle, Color.White);
            spriteBatch.Draw(sprite.GetTexture(), upperFireball, sourceRectangle, Color.White);
            spriteBatch.Draw(sprite.GetTexture(), lowerFireball, sourceRectangle, Color.White);
        }
    }
}
