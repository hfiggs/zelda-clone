using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDBluePotion : IHudItem
    {
        public Vector2 location { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.BluePotion;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDBluePotion(IPlayerInventory inv, HUDSprite sprite, Vector2 position)
        {
            this.inv = inv;
            this.sprite = sprite;

            location = position;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement, Color color)
        {
            if (inv.BluePotionCount > 0)
            {
                const int xDiff = 183, yDiff = 23, widthAndHeight = 20;
                selectionRectangle = new Rectangle(xDiff, yDiff, widthAndHeight, widthAndHeight);
                sprite.Draw(spriteBatch, location + movement, color);
            } else {
                selectionRectangle = new Rectangle(-1, -1, -1, -1);
            }
        }

        public void Update(GameTime time)
        {
            // BluePotion receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDBluePotion(inv, location);
        }
    }
}
