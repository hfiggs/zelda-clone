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
        public Key(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateKeySprite();

            this.Position = Position;

            const string keyAudio = "key";
            AudioManager.PlayFireForget(keyAudio);
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
            const int xAndYDiff = 10, widthAndHeight = 20;
            return new Rectangle((int)Position.X + xAndYDiff, (int)Position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
