using Game1;
using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Triforce : IItem
{
    Color color = Color.White;
    int timeTillSwap;
    const int flashTimer = 250; //ms
    ISprite mySprite;

	public Triforce()
	{
        mySprite = ItemSpriteFactory.Instance.CreateTriforceSprite();
        timeTillSwap = flashTimer;
	}

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        mySprite.Draw(spriteBatch, position, color);
    }

    public void Update(GameTime gameTime)
    {
        timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        if(timeTillSwap <= 0)
        {
            if (color.Equals(Color.White))
                color = Color.CornflowerBlue;
            else
                color = Color.White;

            timeTillSwap = flashTimer;
        }
    }
}
