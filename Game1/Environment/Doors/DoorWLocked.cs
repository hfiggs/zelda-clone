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

        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 32);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        public bool open = false;

        public DoorWLocked(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorWLocked();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public void Open()
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorWOpen();
            open = true;
        }
    }
}
