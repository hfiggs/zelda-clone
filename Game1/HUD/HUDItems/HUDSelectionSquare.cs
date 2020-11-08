﻿using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.HUD
{
    internal class HUDSelectionSquare : IHudItem
    {
        public Vector2 location { get; set; } = new Vector2(-100,-100);
        public Rectangle selectionRectangle { get; set; }
        private HUDSprite displaySprite;
        private HUDSprite spriteOne;
        private HUDSprite spriteTwo;
        private float flashTimer = 150f;

        public HUDSelectionSquare(HUDSprite spriteOne, HUDSprite spriteTwo)
        {
            this.displaySprite = spriteOne;
            this.spriteOne = spriteOne;
            this.spriteTwo = spriteTwo;
            selectionRectangle = new Rectangle(-1, -1, -1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 movement ,Color color)
        {
                displaySprite.Draw(spriteBatch, location + movement, color);
        }

        public void Update(GameTime time)
        {
            flashTimer -= (float)time.ElapsedGameTime.TotalMilliseconds;

            if (flashTimer <= 0)
            {
                flashTimer = 150f;
                if (displaySprite == spriteOne)
                {
                    displaySprite = spriteTwo;
                }
                else
                    displaySprite = spriteOne;
            }
        }

        public IHudItem copyOf()
        {
            return HUDItemFactory.Instance.BuildHUDSelectionSquare();
        }
    }
}