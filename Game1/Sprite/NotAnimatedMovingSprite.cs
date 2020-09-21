using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1.Sprite
{
    class NotAnimatedMovingSprite : ISprite
    {
        private Texture2D texture;
        private int rows;
        private int columns;
        private Vector2 position;
        private int windowHeight;

        public NotAnimatedMovingSprite(Texture2D texture, int rows, int columns, Vector2 position, int windowHeight)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.position = position;
            this.windowHeight = windowHeight;
        }

        public void Update()
        {
            position += new Vector2(0f, -2f);

            if (position.Y + (texture.Height / rows) <= 0)
                position = new Vector2(position.X, windowHeight);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;

            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle destinationRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
