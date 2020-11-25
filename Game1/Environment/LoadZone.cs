/* Author: Hunter Figgs.3 */

using System;
using System.Collections.Generic;
using Game1.Particle;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class LoadZone : IEnvironment
    {
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();
        private readonly CompassDirection transitionDirection;
        private readonly Vector2 position;

        private const int hitboxShortDim = 1;
        private const int hitboxLongDim = 32;

        private readonly Point northSouthHitbox = new Point(hitboxLongDim, hitboxShortDim);
        private readonly Point eastWestHitbox = new Point(hitboxShortDim, hitboxLongDim);

        private bool waiting = false;
        private IParticle waitingP;
        private Vector2 particlePosition;
        private readonly Vector2 particleNOffset = new Vector2(-20.0f, 14.0f);
        private readonly Vector2 particleEOffset = new Vector2(-89.0f, 4.0f);
        private readonly Vector2 particleSOffset = new Vector2(-20.0f, -35.0f);
        private readonly Vector2 particleWOffset = new Vector2(15.0f, 4.0f);

        public LoadZone(Vector2 position, CompassDirection transitionDirection)
        {
            switch(transitionDirection)
            {
                case CompassDirection.North:
                    hitboxes.Add(new Rectangle(position.ToPoint(), northSouthHitbox));
                    particlePosition = position + particleNOffset;
                    break;
                case CompassDirection.South:
                    hitboxes.Add(new Rectangle(position.ToPoint(), northSouthHitbox));
                    particlePosition = position + particleSOffset;
                    break;
                case CompassDirection.East:
                    hitboxes.Add(new Rectangle(position.ToPoint(), eastWestHitbox));
                    particlePosition = position + particleEOffset;
                    break;
                case CompassDirection.West:
                    hitboxes.Add(new Rectangle(position.ToPoint(), eastWestHitbox));
                    particlePosition = position + particleWOffset;
                    break;
            }

            this.transitionDirection = transitionDirection;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            if(waiting)
            {
                waitingP.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if(waiting)
            {
                waitingP.Draw(spriteBatch, color);
            }
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public CompassDirection GetTransitionDirection()
        {
            return transitionDirection;
        }

        public void SetWaiting(int playerID)
        {
            if(!waiting)
                waitingP = new PlayerWaiting(particlePosition, playerID, transitionDirection);
            waiting = true;
        }

        public void SetNotWaiting(int playerID)
        {
            waiting = false;
            Console.WriteLine("SetNotWaiting");
        }
    }
}