﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{

public class Triforce : IItem
{
    Color color = Color.White;
    int timeTillSwap;
    const int flashTimer = 250; //ms
    private ISprite sprite;
    public Vector2 Position { get; set; }
    public Triforce(Vector2 Position)
	{
        sprite = ItemSpriteFactory.Instance.CreateTriforceSprite();

        this.Position = Position;

            timeTillSwap = flashTimer;
        }

    public void Draw(SpriteBatch spriteBatch, Color UNUSED)
    {
        sprite.Draw(spriteBatch, Position, color);
    }

        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (color.Equals(Color.White))
                    color = Color.CornflowerBlue;
                else
                    color = Color.White;

                timeTillSwap = flashTimer;
            }
        }

        public Rectangle GetHitbox()
        {
            const int xOffset = 15, yOffset = 14, width = 12, height = 12;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
