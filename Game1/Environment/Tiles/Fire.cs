using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Environment
{
    class Fire : IEnvironment
    {
        private ISprite sprite;
        Vector2 position;
        public Fire(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createFire();
            this.position = position;
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            sprite.Update();
            //throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}
