using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1.Sprite
{
    class TextSprite : ISprite
    {
        private SpriteFont font;
        private Vector2 position;
        private string text;

        public TextSprite(SpriteFont font, Vector2 position, string text)
        {
            this.font = font;
            this.position = position;
            this.text = text;
        }

        public void Update()
        {
            // Do nothing
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }
    }
}
