using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorEHole : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        const int width = 32, height = 8, yDiff = 24;
        private Rectangle hitbox1 = new Rectangle(0, 0, width, height);
        private Rectangle hitbox2 = new Rectangle(0, yDiff, width, height);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorEHole(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorEHole();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitbox2.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            hitboxes.Add(hitbox2);
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
    }
}
