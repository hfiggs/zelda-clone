﻿using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDHeartBar : IHudItem
    {
        public Vector2 location { get; set; }
        public ItemEnum myItem { get; private set; } = ItemEnum.None;
        public Rectangle selectionRectangle { get; set; }
        private IPlayerInventory inv;
        private HUDSprite fullHeart;
        private HUDSprite halfHeart;
        private HUDSprite emptyHeart;

        public HUDHeartBar(IPlayerInventory inv, HUDSprite FullHeart, HUDSprite HalfHeart, HUDSprite EmptyHeart)
        {
            this.inv = inv;
            this.fullHeart = FullHeart;
            this.halfHeart = HalfHeart;
            this.emptyHeart = EmptyHeart;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
            const int x = 180, y = 155, xModifier = 9, heart = 2 ;

            this.location = new Vector2(x, y);
            int heartDrawCount = inv.HalfHeartCount;
            int emptyHeartCount = inv.MaxHalfHearts - inv.HalfHeartCount;
            while (heartDrawCount != 0)
            {
                if (heartDrawCount - heart >= 0)
                {
                    heartDrawCount -= heart;
                    fullHeart.Draw(spriteBatch, location + movement, color);
                    location = new Vector2(location.X + xModifier,location.Y);
                }
                else
                {
                    heartDrawCount -= 1;
                    halfHeart.Draw(spriteBatch, location + movement, color);
                    location = new Vector2(location.X + xModifier, location.Y);
                    emptyHeartCount--;
                }
            }
            while(emptyHeartCount != 0)
            {
                emptyHeartCount -= heart;
                emptyHeart.Draw(spriteBatch, location + movement, color);
                location = new Vector2(location.X + xModifier, location.Y);
            }
        }

        public void Update(GameTime time)
        {
            //Heart Tracking Logic done within Draw
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDHeartBar(inv);
        }
    }
}