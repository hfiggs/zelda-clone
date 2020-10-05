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
    class PlayerStateDown : IPlayerState
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

        public PlayerStateDown(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite();

            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position; 
        }

        public void Attack()
        {
            player.SetState(new PlayerStateDownAttack(player, position));
        }

        public void MoveDown()
        {
            isMoving = true;
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
            if (!isMoving)
                player.SetState(new PlayerStateUp(player, position));
        }

        public void UseItem()
        {
            Boomerang testBoomerang = new Boomerang('W', player);
            BombProjectile testBomb = new BombProjectile(new Vector2(0, 0));

            cantBomb = player.CantUseProjectile(testBomb);
            cantBoomerang = player.CantUseProjectile(testBoomerang);

            if ((!(player.GetItem() == 3 && cantBomb)) && (!(player.GetItem() == 2 && cantBoomerang)))
                player.SetState(new PlayerStateDownUse(player, position));
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

                position.Y += moveSpeed;
            }

            isMoving = false;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'S';
        }
    }
}
