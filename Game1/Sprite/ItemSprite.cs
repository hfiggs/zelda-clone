using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Sprite
{
    class ItemSprite : ISprite
    {
        private Texture2D texture;
        private int row;
        private int column;
        private int columns = 10;
        private int rows = 4;
        public ItemSprite(Texture2D texture, int column, int row)
        {
            this.texture = texture;
            this.row = row;
            this.column = column;
        }

        public void Update()
        {
        }

       public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }
    }
    
}

