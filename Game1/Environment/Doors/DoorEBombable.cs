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
    class DoorEBombable : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public bool open = false;

        private const float topLayer = 1f;

        private const int width = 32, height = 8, yDiff = 24;
        private Rectangle hitboxOpen1 = new Rectangle(0, 0, width, height);
        private Rectangle hitboxOpen2 = new Rectangle(0, yDiff, width, height);

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        public DoorEBombable(Vector2 position, bool isOpen)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorEBlank();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);

            if (isOpen)
            {
                OpenDoor(false);
            }

            hitboxOpen1.Location += position.ToPoint();
            hitboxOpen2.Location += position.ToPoint();
        }

        public void Update(GameTime gameTime)
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
            sprite = EnvironmentSpriteFactory.instance.createDoorEHole();
            hitboxes = new List<Rectangle>()
            {
                hitboxOpen1,
                hitboxOpen2
            };

            const string revealAudio = "reveal";
            if (shouldPlaySound)
            {
                AudioManager.PlayFireForget(revealAudio);
            }
        }
    }
}
