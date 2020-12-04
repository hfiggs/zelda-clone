using Game1.Player.PlayerInventory;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDSword : IHudItem
    {
        private const int x = 139, y = 141;
        public Vector2 location { get; set; } = new Vector2(x, y);
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        public Rectangle selectionRectangle { get; set; }
        public HUDSprite sprite;
        private bool twoPlayers;
        public HUDSword(HUDSprite sprite, bool twoPlayers)
        {
            this.sprite = sprite;
            this.twoPlayers = twoPlayers;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            const int xModifier = 38;
            Vector2 player2Modifier = new Vector2(xModifier, 0);

            if (!twoPlayers)
            {
                sprite.Draw(spriteBatch, location + movement, color);
            } else
            {
                sprite.Draw(spriteBatch, location + movement + player2Modifier, color);
            }
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