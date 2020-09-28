using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Bow : IItem
{
    ISprite MySprite;
	public Bow()
	{
        MySprite = ItemSpriteFactory.Instance.CreateBowSprite();
	}

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        MySprite.Draw(spriteBatch, position, Color.White);
    }

    public void Update(GameTime gameTime)
    {
        //Do Nothing
    }
}
