using Game1.Enemy;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Merchant : IEnemy
{
    ISprite mySprite;
    Vector2 position;
    SpriteBatch spriteBatch;

	public Merchant(Vector2 position, SpriteBatch spriteBatch)
	{
        mySprite = EnemySpriteFactory.Instance.CreateMerchantSprite();
        this.spriteBatch = spriteBatch;
        this.position = position;
	}

    public void Attack()
    {
        // Do Nothing
    }

    public void Draw()
    {
        mySprite.Draw(spriteBatch, position, Color.White);
    }

    public void ReceiveDamage()
    {
        // Do Nothing
    }

    public void Update(GameTime gameTime, Rectangle drawingLimits5)
    {
        //Do Nothing
    }
}
