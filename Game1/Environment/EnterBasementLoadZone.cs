/* Author: Hunter Figgs.3 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class EnterBasementLoadZone : IEnvironment
    {
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();

        private const int hitboxShortDim = 1;
        private const int hitboxLongDim = 16;

        private readonly Point verticalHitbox = new Point(hitboxShortDim, hitboxLongDim);

        public EnterBasementLoadZone(Vector2 position)
        {
            hitboxes.Add(new Rectangle(position.ToPoint(), verticalHitbox));
        }

        public void Update(GameTime gameTime)
        {
            // Do nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            // Do nothing
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}