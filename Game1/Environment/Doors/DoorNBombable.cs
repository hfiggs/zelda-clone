using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorNBombable : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public bool open = false;

        private Rectangle openHitbox1 = new Rectangle(0, 0, 8, 32);
        private Rectangle openHitbox2 = new Rectangle(24, 0, 8, 32);

        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 32);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorNBombable(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNBlank();
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

        public void openDoor()
        {
            open = true;
            sprite = EnvironmentSpriteFactory.instance.createDoorNHole();
            hitboxes = new List<Rectangle>();
            hitboxes.Add(openHitbox1);
            hitboxes.Add(openHitbox2);
        }
    }
}
