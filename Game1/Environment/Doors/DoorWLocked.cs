using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorWLocked : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public DoorWLocked(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorWLocked();
            this.position = position;
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
