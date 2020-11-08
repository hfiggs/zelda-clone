using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDFlashingDot : IHudItem
    {
        public Vector2 location { get; set; }
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite displaySprite;
        private HUDSprite spriteOne;
        private HUDSprite spriteTwo;
        private float flashTimer = 150f;

        public HUDFlashingDot(IPlayerInventory inv, Vector2 bossPosition, HUDSprite spriteOne, HUDSprite spriteTwo)
        {
            this.inv = inv;
            this.displaySprite = spriteOne;
            this.spriteOne = spriteOne;
            this.spriteTwo = spriteTwo;
            this.location = bossPosition;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if (inv.HasCompass)
            {
                displaySprite.Draw(spriteBatch, location + movement, color);
            }
        }

        public void Update(GameTime time)
        {
            flashTimer -= (float)time.ElapsedGameTime.TotalMilliseconds;

            if(flashTimer <= 0)
            {
                flashTimer = 150f;
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
            return HUDItemFactory.Instance.BuildHUDFlashingDot(inv, location);
        }
    }
}