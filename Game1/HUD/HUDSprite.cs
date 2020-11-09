using Game1.Item;
using Game1.Player.PlayerInventory;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;

namespace Game1.HUD
{
    class HUDSprite : ISprite
    {

        Texture2D texture;
        ISprite possibleSprite;
        private int row;
        private int column;
        private int columns;
        private int rows;
        

        public HUDSprite(Texture2D texture, int row, int column, int columns, int rows)
        {
            this.texture = texture;
            this.row = row;
            this.column = column;
            this.columns = columns;
            this.rows = rows;
            possibleSprite = null;
        }
        public HUDSprite(ISprite item)
        {
            possibleSprite = item;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            if (possibleSprite == null)
            {
                int width = (int)Math.Ceiling((float)texture.Width / columns);
                int height = (int)Math.Ceiling((float)texture.Height / rows);

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
            }
            else
            {
                possibleSprite.Draw(spriteBatch, position, color);
            }
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position, Color color, float layerDepth)
        {
            if (possibleSprite == null)
            {
                int width = (int)Math.Ceiling((float)texture.Width / columns);
                int height = (int)Math.Ceiling((float)texture.Height / rows);

                Rectangle sourceRect = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRect = new Rectangle((int)position.X, (int)position.Y, width, height);

                spritebatch.Draw(texture, destinationRect, sourceRect, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, layerDepth);
            }
            else
            {
                possibleSprite.Draw(spritebatch, position, color);
            }
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
