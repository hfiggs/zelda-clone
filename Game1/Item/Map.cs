using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    class Map : IItem
    {
        ISprite mySprite;

        public Map()
        {
            mySprite = ItemSpriteFactory.Instance.CreateMapSprite();
        }
        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
