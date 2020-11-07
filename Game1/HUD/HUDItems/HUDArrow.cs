using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDArrow : IHudItem
    {
        public Vector2 location { get; set; } = new Vector2(170, -9);
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDArrow(IPlayerInventory inv, HUDSprite sprite)
        {
            this.inv = inv;
            this.sprite = sprite;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if(inv.HasArrow)
            {
                sprite.Draw(spriteBatch, location + movement, color);
            }
        }

        public void Update(GameTime time)
        {
            //bow receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDArrow(inv);
        }
    }
}