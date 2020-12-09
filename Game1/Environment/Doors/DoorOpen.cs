using Game1.Sprite;
using Game1.Util;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Environment.EnvironmentUtil;

namespace Game1.Environment
{
    class DoorOpen : IEnvironment
    {
        private ISprite spriteBelow;
        private ISprite spriteAbove;
        private Vector2 position;

        private Rectangle hitbox1;
        private Rectangle hitbox2;
        private List<Rectangle> hitboxes;

        public CompassDirection direction;

        public DoorOpen(Vector2 position, CompassDirection direction)
        {
            this.position = position;
            this.direction = direction;

            DoorUtil.SetOpenDoorSprites(out spriteBelow, out spriteAbove, direction);

            DoorUtil.SetOpenDoorHitboxes(out hitbox1, out hitbox2, direction, position);

            hitboxes = new List<Rectangle>()
            {
                hitbox1,
                hitbox2
            };
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
    }
}
