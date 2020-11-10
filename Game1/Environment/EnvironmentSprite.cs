using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Draw(SpriteBatch spritebatch, Vector2 position, Color color, float layerDepth)
        {
            Rectangle destRect = new Rectangle(position.ToPoint(), new Point(sourceRect.Width, sourceRect.Height));
            spritebatch.Draw(spriteSheet.GetTexture(), destRect, sourceRect, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, layerDepth);
        }

        public void Update()
        {
            const int animationChangeTime = 2, firstSprite = 2, secondSprite = 3, row = 2;

            if (animated && delayTime > animationChangeTime)
            {
                if (column == firstSprite)
                {
                    sourceRect = spriteSheet.PickSprite(column = secondSprite, row);
                } else if(column == secondSprite)
                {
                    sourceRect = spriteSheet.PickSprite(column = firstSprite, row);
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
