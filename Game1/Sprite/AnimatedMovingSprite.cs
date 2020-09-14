using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1.Sprite
{
    class AnimatedMovingSprite : ISprite
    {
        private Texture2D texture;
        private int rows;
        private int columns;
        private int totalFrames;
        private int currentFrame;
        private int delay;
        private Vector2 position;
        private int windowWidth;

        public AnimatedMovingSprite(Texture2D texture, int rows, int columns, int totalFrames, Vector2 position, int windowWidth)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.position = position;
            this.totalFrames = totalFrames;
            currentFrame = 0;
            delay = 0;
            this.windowWidth = windowWidth;
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

            position += new Vector2(2f, 0f);

            if (position.X >= windowWidth)
                position = new Vector2(-(texture.Width / columns), position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
