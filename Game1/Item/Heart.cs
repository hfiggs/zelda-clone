using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1.Item
{
    public class Heart : IItem
    {
        Color color = new Color(new Vector3(0.973f, 0.22f, 0.0f));
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
                    if (color.Equals(new Color(new Vector3(0.973f, 0.22f, 0.0f))))
                        color = new Color(new Vector3(0.0f, 0.0f, 0.737f));
                    else
                        color = new Color(new Vector3(0.973f, 0.22f, 0.0f));

                timeTillSwap = flashTimer;
                }
            }

            public Rectangle GetHitbox()
            {
            const int xAndYDiff = 10, widthAndHeight = 20;
            return new Rectangle((int)Position.X + xAndYDiff, (int)Position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
            }

            public bool ShouldDelete { get; set; } = false;
    }
}
