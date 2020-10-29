using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Sprite
{
	public class PlayerSprite : ISprite
	{
        private Texture2D texture;

        private int height;
        private int width;

        private int currentRow;
        private int columnLocation;
        private int maxRow;

        public PlayerSprite(Texture2D texture, int rows, int columns, int frameColumn, int maxRow)
		{
            this.texture = texture;
            //calculate frame width
            width = (texture.Width / columns);
            //calculate frame column
            columnLocation = width * frameColumn;
            //calculate frame height
            height = texture.Height / rows;
            this.maxRow = maxRow;
            currentRow = 0;

		}

        public void Update()
        {
            currentRow++;
            if(currentRow == maxRow)
            {
                currentRow = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            Rectangle sourceRect = new Rectangle(columnLocation, height*currentRow, width, height);
            Rectangle destinationRect = new Rectangle((int)location.X,(int)location.Y, width, height);

            spriteBatch.Draw(texture, destinationRect, sourceRect, color);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position, Color color, float layerDepth)
        {
            Rectangle sourceRect = new Rectangle(columnLocation, height * currentRow, width, height);
            Rectangle destinationRect = new Rectangle((int)position.X, (int)position.Y, width, height);

            spritebatch.Draw(texture, destinationRect, sourceRect, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, layerDepth);
        }
    }
}