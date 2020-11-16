using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.XPath;

namespace Game1.HUD
{
    internal class HUDBombTextBox : IHudItem
    {
        private Texture2D HUDIconsTexture;
        private int row;
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        public Vector2 location { get ; set; }
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        public HUDBombTextBox(Texture2D HUDIconsTexture, int row, IPlayerInventory inv)
        {
            this.inv = inv;
            this.HUDIconsTexture = HUDIconsTexture;
            this.row = row;
        }



        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            const int columnModifier = 1, columns = 16, rows = 3, xModifier = 9, x = 85, y = 168, placeInterval = 10;

            location = new Vector2(x,y);
            HUDSprite textSprite = new HUDSprite(HUDIconsTexture, row, 0, columns, rows);
            textSprite.Draw(spriteBatch, location + movement, color);
            location = new Vector2(location.X + xModifier, location.Y);

            int currentBombs = inv.BombCount;
            int totalPlaces = 1;
            currentBombs = currentBombs / placeInterval;
            while (currentBombs > 0)
            {
                currentBombs = currentBombs / placeInterval;
                totalPlaces++;
            }
            currentBombs = inv.BombCount;

            int[] numbers = new int[totalPlaces];
            for (int i = 0; i < totalPlaces; i++)
            {
                numbers[i] = currentBombs % placeInterval;
                currentBombs = currentBombs / placeInterval;
            }


            for(int i = totalPlaces - 1; i >= 0; i--)
            {
                textSprite = new HUDSprite(HUDIconsTexture, row, numbers[i] + columnModifier, columns, rows);
                textSprite.Draw(spriteBatch, location + movement, color);
                location = new Vector2(location.X + xModifier, location.Y);
            }
        }

        public void Update(GameTime time)
        {
            //No Update logic for textboxes.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDBombTextBox(inv);
        }
    }
}