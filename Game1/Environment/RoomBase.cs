using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class RoomBase : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public RoomBase(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createBase();
            this.position = position;
        }
        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException("For collision mechanics later");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
