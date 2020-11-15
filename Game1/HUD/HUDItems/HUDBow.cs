using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace Game1.HUD
{
    internal class HUDBow : IHudItem
    {
        public Vector2 location { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.Bow;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDBow(IPlayerInventory inv, HUDSprite sprite, Vector2 position)
        {
            this.inv = inv;
            this.sprite = sprite;
            this.location = position;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if (inv.HasItem(ItemEnum.Bow))
            {
                if (inv.HasItem(ItemEnum.Arrow))
                {
                    const int xDiff = 183, yDiff = 4, widthAndHeight = 20;
                    selectionRectangle = new Rectangle(xDiff, yDiff, widthAndHeight, widthAndHeight);
                } else {
                    selectionRectangle = new Rectangle(-1, -1, -1, -1);
                }

                sprite.Draw(spriteBatch, location + movement, color);
            }
        }

        public void Update(GameTime time)
        {
            //bow receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDBow(inv, location);
        }
    }
}