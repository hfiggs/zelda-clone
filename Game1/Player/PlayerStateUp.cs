/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateUp : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        private Vector2 position;
        private bool cantBoomerang;
        private bool cantBomb;

        private float timeUntilNextFrame; // ms

        private const int moveSpeed = 2;
        private const float animationTime = 150f; // ms per frame

        public PlayerStateUp(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateWalkUpSprite();

            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position; 
        }

        public void Attack()
        {
            player.SetState(new PlayerStateUpAttack(player, position));
        }

        public void MoveDown()
        {
            if (!isMoving)
                player.SetState(new PlayerStateDown(player, position));
        }

        public void MoveLeft()
        {
            if (!isMoving)
                player.SetState(new PlayerStateLeft(player, position));
        }

        public void MoveRight()
        {
            if (!isMoving)
                player.SetState(new PlayerStateRight(player, position));
        }

        public void MoveUp()
        {
            isMoving = true;
        }

        public void UseItem()
        {
           Boomerang testBoomerang = new Boomerang('W', player);
           BombProjectile testBomb = new BombProjectile(new Vector2(0, 0));

            cantBomb = player.CantUseProjectile(testBomb);
            cantBoomerang = player.CantUseProjectile(testBoomerang);

            if ((!(player.GetItem() == 3 && cantBomb)) && (!(player.GetItem() == 2 && cantBoomerang)))
                player.SetState(new PlayerStateUpUse(player, position));
        }

        public void Update(GameTime time)
        {
            if (isMoving)
            {
                timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }

                position.Y -= moveSpeed;
            }

            isMoving = false;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'N';
        }
    }
}
