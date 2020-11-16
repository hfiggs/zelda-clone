using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDBoomerang : IHudItem
    {
        public Vector2 location { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.Boomerang;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDBoomerang(IPlayerInventory inv, HUDSprite sprite, Vector2 position)
        {
            this.inv = inv;
            this.sprite = sprite;
            this.location = position;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if (inv.HasItem(ItemEnum.Boomerang))
            {
                const int x = 143, y = 4, widthAndHeight = 20;
                selectionRectangle = new Rectangle(x, y, widthAndHeight, widthAndHeight);
                sprite.Draw(spriteBatch, location + movement, color);
            } else {
                selectionRectangle = new Rectangle(-1, -1, -1, -1);
            }
        }

        public void Update(GameTime time)
        {
            //bow receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDBoomerang(inv,location);
        }
    }
}