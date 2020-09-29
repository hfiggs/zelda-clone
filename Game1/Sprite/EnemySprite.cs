using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class EnemySprite : ISprite
    {
        private Texture2D texture;
        private int row;
        private int column;
        private int maxColumns;
        private int maxRows;

        private int delay;
        private int currentFrame;
        private int totalFrames;
        public EnemySprite(Texture2D texture, int column, int row, int maxColumns, int maxRows, int totalFrames)
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
            delay++;

            if (delay == 9)
            {
                currentFrame++;
                delay = 0;
            }

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
    }
}
