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
    class RoomBorder : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public RoomBorder(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createRoom();
            this.position = position;
        }
        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For collision mechanics later");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
