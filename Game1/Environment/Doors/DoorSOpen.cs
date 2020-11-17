using System;
using Game1.Sprite;
using Game1.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorSOpen : IEnvironment
    {
        private ISprite spriteBelow;
        private ISprite spriteAbove;
        private Vector2 position;

        private const float topLayer = 1f;

        private const int width = 8, height = 32, xDiff = 24;
        private Rectangle hitbox1 = new Rectangle(0, 0, width, height);
        private Rectangle hitbox2 = new Rectangle(xDiff, 0, width, height);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorSOpen(Vector2 position)
        {
            spriteBelow = EnvironmentSpriteFactory.instance.createDoorSOpenBelow();
            spriteAbove = EnvironmentSpriteFactory.instance.createDoorSOpenAbove();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitbox2.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            hitboxes.Add(hitbox2);
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
