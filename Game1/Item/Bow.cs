using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Bow : IItem
{
    private ISprite sprite;

    public Vector2 Position { get; set; }

    public Bow(Vector2 Position)
	{
        sprite = ItemSpriteFactory.Instance.CreateBowSprite();

        this.Position = Position;
    }

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        sprite.Draw(spriteBatch, Position, color);
    }

    public void Update(GameTime gameTime)
    {
        //Do Nothing
    }

    public Rectangle GetHitbox()
    {
        const int xOffset = 16, yOffset = 11, width = 10, height = 18;
        return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
    }

    public bool ShouldDelete { get; set; } = false;
}
