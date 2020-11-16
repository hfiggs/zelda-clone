using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDSelectionSquare : IHudItem
    {
        private const int xAndY = -100;
        public Vector2 location { get; set; } = new Vector2(xAndY, xAndY);
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        public Rectangle selectionRectangle { get; set; }
        private HUDSprite displaySprite;
        private HUDSprite spriteOne;
        private HUDSprite spriteTwo;
        private const float flashTimerMax = 150f;
        private float flashTimer = flashTimerMax;

        public HUDSelectionSquare(HUDSprite spriteOne, HUDSprite spriteTwo)
        {
            this.displaySprite = spriteOne;
            this.spriteOne = spriteOne;
            this.spriteTwo = spriteTwo;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            const int x = 125, y = 1, width = 128, height = 120;
            Rectangle boundry = new Rectangle(x, y, width, height);
            Rectangle testRec = new Rectangle(selectionRectangle.X + (int)location.X, selectionRectangle.Y + (int)location.Y, selectionRectangle.Width, selectionRectangle.Height);

            if (testRec.Intersects(boundry)) // This prevents the selection box from showing up at -1, -1 or 0, 0
                displaySprite.Draw(spriteBatch, location + movement, color);
        }

        public void Update(GameTime time)
        {
            flashTimer -= (float)time.ElapsedGameTime.TotalMilliseconds;

            if (flashTimer <= 0)
            {
                flashTimer = flashTimerMax;
                if (displaySprite == spriteOne)
                {
                    displaySprite = spriteTwo;
                }
                else
                    displaySprite = spriteOne;
            }
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDSelectionSquare();
        }
    }
}