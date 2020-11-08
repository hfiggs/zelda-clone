using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDCompass : IHudItem
    {
        public Vector2 location { get; set; } = new Vector2(28,90);
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDCompass(IPlayerInventory inv, HUDSprite sprite)
        {
            this.inv = inv;
            this.sprite = sprite;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if (inv.HasCompass)
            {
                sprite.Draw(spriteBatch, location + movement, color);
            }
        }

        public void Update(GameTime time)
        {
            //Compass receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDCompass(inv);
        }
    }
}