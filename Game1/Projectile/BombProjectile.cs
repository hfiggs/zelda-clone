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
    class BombProjectile : IProjectile
    {
        private int counter;
        private bool detonated;
        private ProjectileSpriteSheet sprite;
        private Point position;

        public BombProjectile(Point position)
        {
            this.position = position;
            detonated = false;
            counter = 0;
            sprite = ProjectileSpriteFactory.Instance.CreateBombProjectileSprite();
        }
        public void Update()
        {
            if (counter < 70) {
                counter++;
            } else {
                detonated = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!detonated) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, 0);
                Rectangle destinationRectangle = new Rectangle(position.X, position.Y, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}

