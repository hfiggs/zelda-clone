using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDRupeeTextBox : IHudItem
    {
        private Texture2D HUDIconsTexture;
        private int row;
        public Vector2 location { get ; set; }
        public Rectangle selectionRectangle { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        private IPlayerInventory inv;
        private bool twoPlayers;

        public HUDRupeeTextBox(Texture2D HUDIconsTexture, int row, IPlayerInventory inv, bool twoPlayers)
        {
            this.inv = inv;
            this.HUDIconsTexture = HUDIconsTexture;
            this.row = row;
            this.twoPlayers = twoPlayers;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            const int columnModifier = 1, columns = 16, rows = 3, x = 66, y = 149, xModifier = 8, placeInterval = 10;
            int twoPlayerXModifier = 0;

            if (twoPlayers) {
                const int secondPlayerTextBoxXLocation = 34;
                twoPlayerXModifier = secondPlayerTextBoxXLocation;
            }

            location = new Vector2(x + twoPlayerXModifier, y);
            HUDSprite textSprite = new HUDSprite(HUDIconsTexture, row, 0, columns, rows);
            textSprite.Draw(spriteBatch, location + movement, color);
            location = new Vector2(location.X + xModifier, location.Y);

            int currentRupees = inv.RupeeCount;
            int totalPlaces = 1;
            currentRupees = currentRupees / placeInterval;
            while (currentRupees > 0)
            {
                currentRupees = currentRupees / placeInterval;
                totalPlaces++;
            }
            currentRupees = inv.RupeeCount;

            int[] numbers = new int[totalPlaces];
            for (int i = 0; i < totalPlaces; i++)
            {
                numbers[i] = currentRupees % placeInterval;
                currentRupees = currentRupees / placeInterval;
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
            return HUDItemFactory.Instance.BuildHUDRupeeTextBox(inv);
        }
    }
}