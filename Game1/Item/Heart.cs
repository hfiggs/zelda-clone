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
    ISprite mySprite;

    public Heart()
	{
        mySprite = ItemSpriteFactory.Instance.CreateHeartSprite();
        timeTillSwap = flashTimer;
	}

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        mySprite.Draw(spriteBatch, position, color);
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
