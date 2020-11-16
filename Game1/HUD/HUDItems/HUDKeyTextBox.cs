using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDKeyTextBox : IHudItem
    {
        private Texture2D HUDIconsTexture;
        private int row;
        public Vector2 location { get ; set; }
        public Rectangle selectionRectangle { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        private IPlayerInventory inv;
        public HUDKeyTextBox(Texture2D HUDIconsTexture, int row, IPlayerInventory inv)
        {
            this.inv = inv;
            this.HUDIconsTexture = HUDIconsTexture;
            this.row = row;
        }



        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            const int columnModifier = 1, columns = 16, rows = 3, xModifier = 9, x = 85, y = 158, placeInterval = 10;

            location = new Vector2(x, y);
            HUDSprite textSprite = new HUDSprite(HUDIconsTexture, row, 0, columns, rows);
            textSprite.Draw(spriteBatch, location + movement, color);
            location = new Vector2(location.X + xModifier, location.Y);

            int currentKey = inv.KeyCount;
            int totalPlaces = 1;
            currentKey = currentKey / placeInterval;
            while (currentKey > 0)
            {
                currentKey = currentKey / placeInterval;
                totalPlaces++;
            }
            currentKey = inv.KeyCount;

            int[] numbers = new int[totalPlaces];
            for (int i = 0; i < totalPlaces; i++)
            {
                numbers[i] = currentKey % placeInterval;
                currentKey = currentKey / placeInterval;
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
            return HUDItemFactory.Instance.BuildHUDKeyTextBox(inv);
        }
    }
}