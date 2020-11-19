/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateRight : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
  public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms

        private const float x = 1.33f;
        private Vector2 moveSpeed = new Vector2(x, 0);
        private const float animationTime = 150f; // ms per frame

        public PlayerStateRight(IPlayer player, Vector2  position)
        {
            this.player = player;
            if (player.GetType() == typeof(Player1)) {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkRightSprite();
            } else {
                Sprite = PlayerSpriteFactory.Instance.CreateZeldaWalkRightSprite();
            }   

            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position;
            
        }

        public void Attack()
        {
            player.SetState(new PlayerStateRightAttack(player, position));
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
            isMoving = true;
        }

        public void MoveUp()
        {
            if (!isMoving)
                player.SetState(new PlayerStateUp(player, position));
        }

        public void UseItem()
        {
            if (player.PlayerInventory.HasItem(ItemEnum.Boomerang) && !player.PlayerInventory.IsItemInUse(ItemEnum.Boomerang))
            {
                player.SetState(new PlayerStateRightUse(player, position));
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
            const char east = 'E';
            return east;
        }
    }
}
