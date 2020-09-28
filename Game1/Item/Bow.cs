using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Bow : IItem
{
    private ISprite sprite;

    private Vector2 position;

	public Bow(Vector2 position)
	{
        sprite = ItemSpriteFactory.Instance.CreateBowSprite();

        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, position, Color.White);
    }

    public void Update(GameTime gameTime)
    {
        //Do Nothing
    }
}
