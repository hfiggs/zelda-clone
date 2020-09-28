using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Environment;

namespace Game1.Environment
{
    class EnvironmentSprite : ISprite
    {
        private SpriteSheet spriteSheet;
        private int column;
        private int row;
        private int spriteID;
        private Rectangle sourceRect;
        private bool animated;

        private int delayTime = 0;

        public EnvironmentSprite(SpriteSheet spriteSheet, int column, int row, int spriteID, bool animated)
        {
            this.spriteSheet = spriteSheet;
            this.column = column;
            this.row = row;
            this.spriteID = spriteID;
            sourceRect = spriteSheet.PickSprite(column, row);
            this.animated = animated;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position, Color color)
        {
            Rectangle destRect = new Rectangle(position.ToPoint(), new Point(sourceRect.Width, sourceRect.Height));
            spritebatch.Draw(spriteSheet.GetTexture(), destRect, sourceRect, color);
        }

        public void Update()
        {
            if (animated && delayTime > 2)
            {
                if (column == 2)
                {
                    sourceRect = spriteSheet.PickSprite(column = 3, 2);
                } else if(column == 3)
                {
                    sourceRect = spriteSheet.PickSprite(column = 2, 2);
                }
                delayTime = 0;
            } else
            {
                delayTime++;
            }
        }

        public int GetSpriteID()
        {
            return spriteID;
        }
    }
}
