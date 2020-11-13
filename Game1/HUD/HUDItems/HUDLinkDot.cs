using Game1.Player.PlayerInventory;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDLinkDot : IHudItem
    {
        private HUDSprite HUDSprite;
        private Screen screen;
        public Vector2 location { get; set; }
        public Rectangle selectionRectangle { get; set; }
        public ItemEnum myItem { get; private set; }
        public HUDLinkDot(HUDSprite hUDSprite, Screen screen)
        {
            this.HUDSprite = hUDSprite;
            this.screen = screen;
        }

        public IHudItem copyOf()
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement, Color color)
        {
            HUDSprite.Draw(spriteBatch, location + movement, color);
        }

        public void Update(GameTime time)
        {

        }
    }
}
