using Game1.Item;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1.Item
{
    public class Heart : IItem
    {
        const float colorRed = 0.973f, colorGreen = 0.22f, colorBlue = 0.0f, colorRed2 = 0.0f, colorGreen2 = 0.0f, colorBlue2 = 0.737f;
        Color color = new Color(new Vector3(colorRed, colorGreen, colorBlue));
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
                    if (color.Equals(new Color(new Vector3(colorRed, colorGreen, colorBlue))))
                        color = new Color(new Vector3(colorRed2, colorGreen2, colorBlue2));
                    else
                        color = new Color(new Vector3(colorRed, colorGreen, colorBlue));

                timeTillSwap = flashTimer;
                }
            }

            public Rectangle GetHitbox()
            {
            const int xOffset = 16, yOffset = 15, width = 9, height = 10;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
            }

            public bool ShouldDelete { get; set; } = false;
    }
}
