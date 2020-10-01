using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Environment
{
    class Black : IEnvironment
    {
        private ISprite sprite;
        Vector2 position;
        public Black(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createBlack();
            this.position = position;
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}
