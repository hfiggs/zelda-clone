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
        private int columns = 15;
        private int rows = 8;

        private int delay;
        private int currentFrame;
        private int totalFrames;
        public EnemySprite(Texture2D texture, int column, int row, int totalFrames)
        {
            this.texture = texture;
            this.row = row;
            this.column = column;
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

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;

            Console.WriteLine(width + "+" + height);

            Rectangle sourceRectangle = new Rectangle(width * column, height * (row + +currentFrame), width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
