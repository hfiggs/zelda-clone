using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;


namespace Game1.HUD.HUDItems
{
    class HUDBase : IHudItem
    {
        public Vector2 location { get; set; } = new Vector2(0, 0);
        public Rectangle selectionRectangle { get; set; }
        private HUDSprite sprite;

        public HUDBase(HUDSprite sprite)
        {
            this.sprite = sprite;
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
            return HUDItemFactory.Instance.BuildHUDBase();
        }
    }
}
