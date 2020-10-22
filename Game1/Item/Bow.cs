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

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        sprite.Draw(spriteBatch, position, color);
    }

    public void Update(GameTime gameTime)
    {
        //Do Nothing
    }

    public Rectangle GetHitbox()
    {
        return new Rectangle((int)position.X + 10, (int)position.Y + 10, 20, 20);
    }
}
