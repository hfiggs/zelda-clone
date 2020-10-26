using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Heart : IItem
{
    Color color = Color.White;
    int timeTillSwap;
    const int flashTimer = 250; //ms
    private ISprite sprite;
    public Vector2 Position { get; set; }
    public Heart(Vector2 Position)
	{
        sprite = ItemSpriteFactory.Instance.CreateHeartSprite();

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
                color = Color.Red;
            else
                color = Color.White;

            timeTillSwap = flashTimer;
        }
    }

    public Rectangle GetHitbox()
    {
        return new Rectangle((int)Position.X + 10, (int)Position.Y + 10, 20, 20);
    }
}
