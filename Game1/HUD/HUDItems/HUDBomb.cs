using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDBomb : IHudItem
    {
        public Vector2 location { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.Bomb;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDBomb(IPlayerInventory inv, HUDSprite sprite, Vector2 position)
        {
            this.inv = inv;
            this.sprite = sprite;

            location = position;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if (inv.BombCount > 0)
            {
                selectionRectangle = new Rectangle(163, 4, 20, 20);
                sprite.Draw(spriteBatch, location + movement, color);
            }
        }

        public void Update(GameTime time)
        {
            //bow receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDBomb(inv, location);
        }
    }
}