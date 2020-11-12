using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDRoom : IHudItem
    {

        public Vector2 location { get; set; }
        public Rectangle selectionRectangle { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        private HUDSprite sprite;

        public HUDRoom(HUDSprite sprite, Vector2 Position)
        {
            this.sprite = sprite;
            this.location = Position;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            sprite.Draw(spriteBatch, location + movement, color);
        }

        public void Update(GameTime time)
        {

        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDSelectionSquare();
        }
    }
}