using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{

    public class Triforce : IItem
    {
        Color color = Color.White;
        int timeTillSwap;
        const int flashTimer = 250; //ms
        private ISprite sprite;

        private Vector2 position;

        public Triforce(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateTriforceSprite();

            this.position = position;

            timeTillSwap = flashTimer;
        }

        public void Draw(SpriteBatch spriteBatch, Color UNUSED)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (color.Equals(Color.White))
                    color = Color.CornflowerBlue;
                else
                    color = Color.White;

                timeTillSwap = flashTimer;
            }
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, 20, 20);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
