using Game1.Audio;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    class Key : IItem
    {
        private ISprite sprite;

        public Vector2 Position { get; set; }

        const int positionXOffset = 13, positionYOffset = 11;
        const int spriteXOffset = 16, spriteYOffset = 11, spriteWidth = 10, spriteHeight = 18;

        public Key(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateKeySprite();

            const string keyAudio = "key";
            AudioManager.PlayFireForget(keyAudio);

            this.Position = Vector2.Subtract(Position, new Vector2(positionXOffset, positionYOffset));
        }
        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)Position.X + spriteXOffset, (int)Position.Y + spriteYOffset, spriteWidth, spriteHeight);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
