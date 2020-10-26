﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class SpikeTrapStateAttackSouth : IEnemyState
    {
        private IEnemy spiketrap;
        private Game1 game;

        private Vector2 homePosition;
        private Vector2 currentPosition;

        private int verticalRange;
        private int horizontalRange;

        private const int advanceSpeed = 4;
        private const int retreatSpeed = 1;

        private bool isAdvancing;

        public ISprite Sprite { get; private set; }

        public SpikeTrapStateAttackSouth(Game1 game, IEnemy spiketrap, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSpikeTrapSprite();

            this.game = game;
            this.spiketrap = spiketrap;

            this.homePosition = homePosition;
            currentPosition = homePosition;

            this.verticalRange = verticalRange;
            this.horizontalRange = horizontalRange;

            isAdvancing = true;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            return currentPosition;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(0, 1);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)currentPosition.X, (int)currentPosition.Y, 16, 16);
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if(isAdvancing)
            {
                currentPosition.Y += advanceSpeed;

                if (currentPosition.Y >= (homePosition.Y + verticalRange))
                {
                    isAdvancing = false;
                }
            }
            else
            {
                currentPosition.Y -= retreatSpeed;

                if (currentPosition.Y <= homePosition.Y)
                {
                    spiketrap.SetState(new SpikeTrapStateHome(game, spiketrap, homePosition, verticalRange, horizontalRange));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, currentPosition, Color.White);
        }

        public void editPosition(Vector2 amount)
        {
            //This shouldn't need its position edited blah blah
        }
    }
}
