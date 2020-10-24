using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorWOpen : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 8);
        private Rectangle hitbox2 = new Rectangle(0, 24, 32, 8);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorWOpen(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorWOpen();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitbox2.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            hitboxes.Add(hitbox2);
        }

        public void BehaviorUpdate()
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
    }
}
