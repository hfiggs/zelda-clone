using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Game1.HUD.HUDItems
{
    class DungeonOneMap : IHudItem
    {
        private const int x = 5, y = 140;
        public Vector2 location { get; set; } = new Vector2(x, y);
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite sprite;

        public DungeonOneMap(IPlayerInventory inv, HUDSprite sprite)
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
            //Dungeon map receives no updates.
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildDungeonOneMap(inv);
        }
    }
}
