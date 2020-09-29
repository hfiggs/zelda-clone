using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class SpriteSheet
    {
        private Texture2D sprites;
        private int columns;
        private int rows;

        public SpriteSheet(Texture2D spriteSheet, int columns, int rows)
        {
            sprites = spriteSheet;
            this.columns = columns;
            this.rows = rows;
        }

        //0-based index for row and column
        public Rectangle PickSprite(int column, int row)
        {
            int width = sprites.Width / columns;
            int height = sprites.Height / rows;

            return new Rectangle(width * column, height * row, width, height);
        }

        public Texture2D GetTexture()
        {
            return sprites;
        }
    }
}
