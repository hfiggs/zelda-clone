﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class SpikeTrapStateHome : IEnemyState
    {
        private IEnemy spiketrap;
        private Game1 game;

        private Vector2 homePosition;

        private int verticalRange;
        private int horizontalRange;

        // The width of the imaginary cross where the spike trap can detect the player
        private const int viewWidth = 16;

        public ISprite Sprite { get; private set; }

        public SpikeTrapStateHome(Game1 game, IEnemy spiketrap, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSpikeTrapSprite();

            this.spiketrap = spiketrap;
            this.game = game;

            this.homePosition = homePosition;

            this.verticalRange = verticalRange;
            this.horizontalRange = horizontalRange;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            return homePosition;
        }

        public Vector2 GetDirection()
        {
            //No direction
            return new Vector2(0, 0);
        }

        public List<Rectangle> GetHitboxes()
        {
            List<Rectangle> hitboxList = new List<Rectangle>();
            const int widthAndHeight = 14, xAndYOffset = 1;
            hitboxList.Add(new Rectangle((int)homePosition.X + xAndYOffset, (int)homePosition.Y + xAndYOffset, widthAndHeight, widthAndHeight));
            return hitboxList;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            List<Rectangle> playerRectList = game.Screen.GetPlayerRectangle();

            Vector2 windowDims = game.GetWindowDimensions();

            foreach (Rectangle playerRect in playerRectList)
            {
                if (playerRect.Intersects(new Rectangle((int)(homePosition.X - windowDims.X), (int)homePosition.Y, (int)windowDims.X, viewWidth))) // Spike sees player west
                {
                    spiketrap.SetState(new SpikeTrapStateAttackWest(game, spiketrap, homePosition, verticalRange, horizontalRange));
                }
                else if (playerRect.Intersects(new Rectangle((int)homePosition.X, (int)homePosition.Y, (int)windowDims.X, viewWidth))) // Spike sees player east
                {
                    spiketrap.SetState(new SpikeTrapStateAttackEast(game, spiketrap, homePosition, verticalRange, horizontalRange));
                }
                else if (playerRect.Intersects(new Rectangle((int)homePosition.X, (int)(homePosition.Y - windowDims.Y), viewWidth, (int)windowDims.Y))) // Spike sees player north
                {
                    spiketrap.SetState(new SpikeTrapStateAttackNorth(game, spiketrap, homePosition, verticalRange, horizontalRange));
                }
                else if (playerRect.Intersects(new Rectangle((int)homePosition.X, (int)homePosition.Y, viewWidth, (int)windowDims.Y))) // Spike sees player south
                {
                    spiketrap.SetState(new SpikeTrapStateAttackSouth(game, spiketrap, homePosition, verticalRange, horizontalRange));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, homePosition, Color.White);
        }

        public void editPosition(Vector2 amount)
        {
            //Do Nothing, These are on a set path and should always move the other sprites it touches.
        }
    }
}
