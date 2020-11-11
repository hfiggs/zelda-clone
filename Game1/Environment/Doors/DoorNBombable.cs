using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;

namespace Game1.Environment
{
    class DoorNBombable : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public bool open = false;

        private const float topLayer = 1f;

        const int width = 8, height = 32, xDiff = 24;
        private Rectangle openHitbox1 = new Rectangle(0, 0, width, height);
        private Rectangle openHitbox2 = new Rectangle(xDiff, 0, width, height);

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorNBombable(Vector2 position, bool isOpen)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNBlank();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);

            if (isOpen)
            {
                OpenDoor(false);
            }
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (open)
            {
                sprite.Draw(spriteBatch, position, color, topLayer);
            }
            else
            {
                sprite.Draw(spriteBatch, position, color);
            }
        }
        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public void OpenDoor(bool shouldPlaySound)
        {
            open = true;
            sprite = EnvironmentSpriteFactory.instance.createDoorNHole();
            hitboxes = new List<Rectangle>();
            //hitboxes.Add(openHitbox1);
            //hitboxes.Add(openHitbox2);

            if (shouldPlaySound)
            {
                AudioManager.PlayFireForget("reveal");
            }
        }
    }
}
