using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDSword : IHudItem
    {
        public Vector2 location { get; set; } = new Vector2(135, 140);

        public Rectangle selectionRectangle { get; set; }
        public HUDSprite sprite;
        public HUDSword(HUDSprite sprite)
        {
            this.sprite = sprite;
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
            return HUDItemFactory.Instance.BuildHUDSword();
        }
    }
}