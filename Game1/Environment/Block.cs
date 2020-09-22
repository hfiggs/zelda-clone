using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class Block : IEnvironment
    {
        SpriteSheet spriteSheet;
        public Block(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
        }
        public void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            Rectangle sourceRect = spriteSheet.PickSprite(0, 0);
            Rectangle destRect = new Rectangle(position.ToPoint(), new Point(sourceRect.Width, sourceRect.Height));
            spritebatch.Draw(spriteSheet.GetTexture(), destRect, sourceRect, Color.White);
        }

        public void Update()
        {
            //Extensible later
        }
    }
}
