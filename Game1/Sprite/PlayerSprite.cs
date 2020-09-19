using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Sprite
{
	public class PlayerSprite : ISprite
	{
        private Texture2D texture;
        private bool damaged;
        private Color color = Color.White;

        private int height;
        private int width;

        private int currentRow;
        private int columnLocation;
        private int maxRow;


        public PlayerSprite(Texture2D texture, bool damaged, int rows, int columns, int frameColumn, int maxRow)
		{
            this.texture = texture;
            this.damaged = damaged;
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

            if(damaged)
            {
                if (color == Color.White)
                {
                    color = Color.Blue;
                }
                else
                {
                    color = Color.White;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            Rectangle sourceRect = new Rectangle(columnLocation, height*currentRow, width, height);
            Rectangle destinationRect = new Rectangle((int)location.X,(int)location.Y, width * 3,height * 3);

            
            spriteBatch.Draw(texture, destinationRect, sourceRect, color);
        }

    }
}