using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Environment.Tiles

namespace Game1.Environment
{
    class EnvironmentSprite : ISprite
    {
        private SpriteSheet spriteSheet;
        private int column;
        private int row;

        public EnvironmentSprite(SpriteSheet spriteSheet, int column, int row)
        {
            this.spriteSheet = spriteSheet;
            this.column = column;
            this.row = row;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            Rectangle sourceRect = spriteSheet.PickSprite(column, row);
            Rectangle destRect = new Rectangle(position.ToPoint(), new Point(sourceRect.Width, sourceRect.Height));
            spritebatch.Draw(spriteSheet.GetTexture(), destRect, sourceRect, Color.White);
        }

        public void Update()
        {
            //Environment Objects have no sprite animations
        }
    }
}
