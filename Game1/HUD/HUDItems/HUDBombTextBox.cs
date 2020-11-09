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
            location = new Vector2(85,168);
            HUDSprite textSprite = new HUDSprite(HUDIconsTexture, row, 0, 16, 3);
            textSprite.Draw(spriteBatch, location + movement, color);
            location = new Vector2(location.X + 9, location.Y);

            int currentBombs = inv.BombCount;
            int totalPlaces = 1;
            currentBombs = currentBombs / 10;
            while (currentBombs > 0)
            {
                currentBombs = currentBombs / 10;
                totalPlaces++;
            }
            currentBombs = inv.BombCount;

            int[] numbers = new int[totalPlaces];
            for (int i = 0; i < totalPlaces; i++)
            {
                numbers[i] = currentBombs % 10;
                currentBombs = currentBombs / 10;
            }


            for(int i = totalPlaces - 1; i >= 0; i--)
            {
                textSprite = new HUDSprite(HUDIconsTexture, row, numbers[i] + 1, 16, 3);
                textSprite.Draw(spriteBatch, location + movement, color);
                location = new Vector2(location.X + 9, location.Y);
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