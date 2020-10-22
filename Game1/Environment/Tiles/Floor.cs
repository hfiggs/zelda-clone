using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{

    class Floor : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private Rectangle hitbox1 = new Rectangle(0, 0, 0, 0);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public Floor(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createFloor();
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
    }
}
