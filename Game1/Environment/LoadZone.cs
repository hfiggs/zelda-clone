/* Author: Hunter Figgs.3 */

using System.Collections.Generic;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class LoadZone : IEnvironment
    {
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();
        private readonly CompassDirection transitionDirection;

        private const int hitboxShortDim = 1;
        private const int hitboxLongDim = 32;

        private readonly Point northSouthHitbox = new Point(hitboxLongDim, hitboxShortDim);
        private readonly Point eastWestHitbox = new Point(hitboxShortDim, hitboxLongDim);

        public LoadZone(Vector2 position, CompassDirection transitionDirection)
        {
            switch(transitionDirection)
            {
                case CompassDirection.North:
                case CompassDirection.South:
                    hitboxes.Add(new Rectangle(position.ToPoint(), northSouthHitbox));
                    break;
                case CompassDirection.East:
                case CompassDirection.West:
                    hitboxes.Add(new Rectangle(position.ToPoint(), eastWestHitbox));
                    break;
            }

            this.transitionDirection = transitionDirection;
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

        public CompassDirection GetTransitionDirection()
        {
            return transitionDirection;
        }
    }
}