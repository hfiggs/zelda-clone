using System;
using Game1.Sprite;
using Game1.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;

namespace Game1.Environment
{
    class DoorSBombable : IEnvironment
    {
        private ISprite spriteBelow;
        private ISprite spriteAbove;
        private Vector2 position;
        public bool open = false;

        private const float topLayer = 1f;

        private const int width = 8, height = 32, xDiff = 24;
        private Rectangle hitboxOpen1 = new Rectangle(0, 0, width, height);
        private Rectangle hitboxOpen2 = new Rectangle(xDiff, 0, width, height);

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorSBombable(Vector2 position, bool isOpen)
        {
            spriteBelow = EnvironmentSpriteFactory.instance.createDoorSBlankBelow();
            spriteAbove = EnvironmentSpriteFactory.instance.createDoorSBlankAbove();
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
            spriteBelow.Draw(spriteBatch, position, color, SpriteLayerUtil.envBelowPlayerLayer2);
            spriteAbove.Draw(spriteBatch, position, color, SpriteLayerUtil.envAbovePlayerLayer);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
        public void OpenDoor(bool shouldPlaySound)
        {
            open = true;
            spriteBelow = EnvironmentSpriteFactory.instance.createDoorSHoleBelow();
            spriteAbove = EnvironmentSpriteFactory.instance.createDoorSHoleAbove();
            hitboxes = new List<Rectangle>()
            {
                hitboxOpen1,
                hitboxOpen2
            };

            if (shouldPlaySound)
            {
                AudioManager.PlayFireForget("reveal");
            }
        }
    }
}
