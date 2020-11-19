/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStateDown : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms

        private const float y = 1.33f;
        private Vector2 moveSpeed = new Vector2(0, y);
        private const float animationTime = 150f; // ms per frame

        public PlayerStateDown(IPlayer player, Vector2 position)
        {
            this.player = player;
            if (player.GetType() == typeof(Player1)) {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite();
            } else {
                Sprite = PlayerSpriteFactory.Instance.CreateZeldaWalkDownSprite();
            }

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

                position += moveSpeed;
            }

            isMoving = false;
        }

        public char GetDirection()
        {
            const char south = 'S';
            return south;
        }
    }
}
