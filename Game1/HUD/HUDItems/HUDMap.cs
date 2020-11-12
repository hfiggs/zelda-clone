using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.HUD
{
    internal class HUDMap : IHudItem
    {
        public Vector2 location { get; set; } = new Vector2(28, 50);
        public Rectangle selectionRectangle { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public HUDMap(IPlayerInventory inv, HUDSprite sprite)
        {
            this.inv = inv;
            this.sprite = sprite;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            if (inv.HasMap)
            {
                sprite.Draw(spriteBatch, location + movement, color);
            }
        }

        public void Update(GameTime time)
        {
            //Dungeon Map receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDMap(inv);
        }
    }
}