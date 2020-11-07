using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.XPath;

namespace Game1.HUD
{
    internal class HUDRupeeTextBox : IHudItem
    {
        private Texture2D HUDIconsTexture;
        private int row;
        public Vector2 location { get ; set; }
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        public HUDRupeeTextBox(Texture2D HUDIconsTexture, int row, IPlayerInventory inv)
        {
            this.inv = inv;
            this.HUDIconsTexture = HUDIconsTexture;
            this.row = row;
        }



        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            location = new Vector2(85, 142);
            HUDSprite textSprite = new HUDSprite(HUDIconsTexture, row, 0, 16, 3);
            textSprite.Draw(spriteBatch, location + movement, color);
            location = new Vector2(location.X + 9, location.Y);

            int currentRupees = inv.RupeeCount;
            int totalPlaces = 1;
            currentRupees = currentRupees / 10;
            while (currentRupees > 0)
            {
                currentRupees = currentRupees / 10;
                totalPlaces++;
            }
            currentRupees = inv.RupeeCount;

            int[] numbers = new int[totalPlaces];
            for (int i = 0; i < totalPlaces; i++)
            {
                numbers[i] = currentRupees % 10;
                currentRupees = currentRupees / 10;
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
            return HUDItemFactory.Instance.BuildHUDRupeeTextBox(inv);
        }
    }
}