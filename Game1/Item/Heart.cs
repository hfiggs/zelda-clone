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

    private Vector2 position;

    public Heart(Vector2 position)
	{
        sprite = ItemSpriteFactory.Instance.CreateHeartSprite();

        this.position = position;

        timeTillSwap = flashTimer;
	}

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, position, color);
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
}
