﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class StartSprite : ISprite
    {
        private Texture2D texture;
        private int row;
        private int column;
        private int maxColumns;
        private int maxRows;

        private int currentFrame;
        private int totalFrames;

        public StartSprite(Texture2D texture, int column, int row, int maxColumns, int maxRows, int totalFrames)
        {
            this.texture = texture;
            this.column = column;
            this.row = row;
            this.maxColumns = maxColumns;
            this.maxRows = maxRows;
            this.totalFrames = totalFrames;
        }

        public void Update()
        {
            currentFrame++;

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            int width = texture.Width / maxColumns;
            int height = texture.Height / maxRows;

            Rectangle sourceRectangle = new Rectangle(width * column, height * (row + +currentFrame), width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position, Color color, float layerDepth)
        {
            int width = texture.Width / maxColumns;
            int height = texture.Height / maxRows;

            Rectangle sourceRectangle = new Rectangle(width * column, height * (row + +currentFrame), width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spritebatch.Draw(texture, destinationRectangle, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, layerDepth);
        }
    }
}
