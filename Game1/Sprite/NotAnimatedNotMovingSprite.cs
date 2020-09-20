using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1.Sprite
{
    class NotAnimatedNotMovingSprite : ISprite
    {
        private Texture2D texture;
        private int rows;
        private int columns;
        private Vector2 position;

        public NotAnimatedNotMovingSprite(Texture2D texture, int rows, int columns, Vector2 position)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.position = position;
        }

        public void Update()
        {
            // Do nothing
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 positon)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;

            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle destinationRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
