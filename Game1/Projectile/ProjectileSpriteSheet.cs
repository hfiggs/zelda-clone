using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class ProjectileSpriteSheet
    {
        private Texture2D projectileSprites;
        private int columnOfSprite, width, height;

        public ProjectileSpriteSheet(Texture2D spriteSheet, int columns, int rows, int columnOfSprite)
        {
            projectileSprites = spriteSheet;
            this.columnOfSprite = columnOfSprite;
            width = projectileSprites.Width / columns;
            height = projectileSprites.Height / rows;
        }

        public Rectangle PickSprite(int column, int row)
        {
            return new Rectangle(width * column, height * row, width, height);
        }

        public Texture2D GetTexture()
        {
            return projectileSprites;
        }

        public int GetColumnOfSprite()
        {
            return columnOfSprite;
        }
    }
}

