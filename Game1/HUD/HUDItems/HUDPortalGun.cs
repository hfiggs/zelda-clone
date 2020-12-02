using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDPortalGun : IHudItem
    {
        public Vector2 location { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.PortalGun;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        private const int x = 203, y = 23, widthAndHeight = 20;

        public HUDPortalGun(IPlayerInventory inv, HUDSprite sprite, Vector2 position)
        {
            this.inv = inv;
            this.sprite = sprite;
            this.location = position;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement, Color color)
        {
            if (inv.HasItem(ItemEnum.PortalGun))
            {
                selectionRectangle = new Rectangle(x, y, widthAndHeight, widthAndHeight);
                sprite.Draw(spriteBatch, location + movement, color);
            } else {
                selectionRectangle = new Rectangle(-1, -1, -1, -1);
            }
        }

        public void Update(GameTime time)
        {
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDPortalGun(inv,location);
        }
    }
}