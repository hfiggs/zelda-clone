using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class SpikeTrapStateAttackEast : IEnemyState
    {
        private IEnemy spiketrap;
        Game1 game;

        private Vector2 homePosition;
        private Vector2 currentPosition;

        private int verticalRange;
        private int horizontalRange;

        private const int advanceSpeed = 2;
        private const int retreatSpeed = 1;

        private bool isAdvancing;

        public ISprite Sprite { get; private set; }

        public SpikeTrapStateAttackEast(Game1 game, IEnemy spiketrap, Vector2 homePosition, int verticalRange, int horizontalRange)
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
            return new Vector2(1,0);
        }

        public List<Rectangle> GetHitboxes()
        {
            List<Rectangle> hitboxList = new List<Rectangle>();
            const int widthAndHeight = 14, xAndYOffset = 1;
            hitboxList.Add(new Rectangle((int)currentPosition.X + xAndYOffset, (int)currentPosition.Y + xAndYOffset, widthAndHeight, widthAndHeight));
            return hitboxList;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if(isAdvancing)
            {
                currentPosition.X += advanceSpeed;

                if (currentPosition.X >= (homePosition.X + horizontalRange))
                {
                    isAdvancing = false;
                }
            }
            else
            {
                currentPosition.X -= retreatSpeed;

                if (currentPosition.X <= homePosition.X)
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
            //can't be edited, doesn't need to be move on
        }
    }
}
