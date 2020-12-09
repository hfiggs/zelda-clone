using Game1.Sprite;
using Game1.Util;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;
using Game1.Environment.EnvironmentUtil;

namespace Game1.Environment
{
    class DoorBombable : IEnvironment
    {
        private ISprite spriteBelow;
        private ISprite spriteAbove;
        private Vector2 position;

        private const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private Rectangle hitboxOpen1;
        private Rectangle hitboxOpen2;
        private List<Rectangle> hitboxes = new List<Rectangle>();
        public bool open;

        public CompassDirection direction;

        public DoorBombable(Vector2 position, CompassDirection direction, bool isOpen)
        {
            this.position = position;
            this.direction = direction;

            spriteBelow = DoorUtil.GetBlankDoorSprite(direction);
            spriteAbove = DoorUtil.GetBlankDoorSprite(direction);

            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);

            open = false;

            if (isOpen)
                Open(false);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBelow.Draw(spriteBatch, position, color, SpriteLayerUtil.envBelowPlayerLayer2);
            if (open)
                spriteAbove.Draw(spriteBatch, position, color, SpriteLayerUtil.envAbovePlayerLayer);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public void Open(bool shouldPlaySound)
        {
            open = true;

            DoorUtil.SetHoleDoorSprites(out spriteBelow, out spriteAbove, direction);
            DoorUtil.SetOpenDoorHitboxes(out hitboxOpen1, out hitboxOpen2, direction, position);

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
