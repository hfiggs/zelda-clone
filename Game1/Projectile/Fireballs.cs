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
        private int modifier;
        private ProjectileSpriteSheet sprite;

        public Fireballs()
        {
            sprite = ProjectileSpriteFactory.Instance.CreateFireballsSprite();
            modifier = 0;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

        }
    }
}
