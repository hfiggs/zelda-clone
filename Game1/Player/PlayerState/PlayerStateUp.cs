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
        public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms

        private const float y = -1.33f;
        private Vector2 moveSpeed = new Vector2(0, y);
        private const float animationTime = 150f; // ms per frame

        public PlayerStateUp(IPlayer player, Vector2 position)
        {
            this.player = player;
            if (player.GetType() == typeof(Player1)) {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkUpSprite();
            } else {
                Sprite = PlayerSpriteFactory.Instance.CreateZeldaWalkUpSprite();
            }

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
            if (!player.getBoomerangOut())
            {
                player.SetState(new PlayerStateUpUse(player, position));
            }
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

                position += moveSpeed;
            }

            isMoving = false;
        }

        public char GetDirection()
        {
            const char north = 'N';
            return north;
        }
    }
}
