using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Sprite
{
    class ProjectileSprite : ISprite
    {
        private Texture2D texture;
        private int row;
        private int column;

        private int currentFrame;
        private int totalFrames;
        private int width;
        private int height;

        public ProjectileSprite(Texture2D texture, int column, int row, int maxColumns, int maxRows, int totalFrames)
        {
            this.texture = texture;
            this.column = column;
            this.row = row;
            this.totalFrames = totalFrames;
            width = texture.Width / maxColumns;
            height = texture.Height / maxRows;
        }

        public void Update()
        {
            currentFrame++;

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(width * column, height * (row + +currentFrame), width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }
    }
}
