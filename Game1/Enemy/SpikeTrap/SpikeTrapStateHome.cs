using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private const int viewWidth = 20;

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

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)homePosition.X, (int)homePosition.Y, 16, 16);
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            Rectangle playerRect = game.Screen.GetPlayerRectangle();

            Vector2 windowDims = game.GetWindowDimensions();

            
            if(playerRect.Intersects(new Rectangle((int)(homePosition.X - windowDims.X), (int)homePosition.Y, (int)windowDims.X, viewWidth))) // Spike sees player west
            {
                spiketrap.SetState(new SpikeTrapStateAttackWest(game, spiketrap, homePosition, verticalRange, horizontalRange));
            }
            else if(playerRect.Intersects(new Rectangle((int)homePosition.X, (int)homePosition.Y, (int)windowDims.X, viewWidth))) // Spike sees player east
            {
                spiketrap.SetState(new SpikeTrapStateAttackEast(game, spiketrap, homePosition, verticalRange, horizontalRange));
            }
            else if(playerRect.Intersects(new Rectangle((int)homePosition.X, (int)(homePosition.Y - windowDims.Y), viewWidth, (int)windowDims.Y))) // Spike sees player north
            {
                spiketrap.SetState(new SpikeTrapStateAttackNorth(game, spiketrap, homePosition, verticalRange, horizontalRange));
            }
            else if (playerRect.Intersects(new Rectangle((int)homePosition.X, (int)homePosition.Y, viewWidth, (int)windowDims.Y))) // Spike sees player south
            {
                spiketrap.SetState(new SpikeTrapStateAttackSouth(game, spiketrap, homePosition, verticalRange, horizontalRange));
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, homePosition, Color.White);
        }
    }
}
