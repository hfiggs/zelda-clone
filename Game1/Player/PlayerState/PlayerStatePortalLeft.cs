/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStatePortalLeft : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
         public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms

        const float x = -1.33f;
        private Vector2 moveSpeed = new Vector2(-1.33f, 0);
        private const float animationTime = 150f; // ms per frame

        private PortalColor portalColor;

        public PlayerStatePortalLeft(IPlayer player, Vector2 position, PortalColor portalColor = PortalColor.Blue)
        {
            this.player = player;

            this.portalColor = portalColor;

            SetSprite();

            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position;
        }

        public void Attack()
        {
            player.SetState(new PlayerStateLeftAttack(player, position));
        }

        public void MoveDown()
        {
            if (!isMoving)
                player.SetState(new PlayerStatePortalDown(player, position, portalColor));
        }

        public void MoveLeft()
        {
            isMoving = true;
        }

        public void MoveRight()
        {
            if (!isMoving)
                player.SetState(new PlayerStatePortalRight(player, position, portalColor));
        }

        public void MoveUp()
        {
            if (!isMoving)
                player.SetState(new PlayerStatePortalUp(player, position, portalColor));
        }

        public void UseItem()
        {
            if (player.PlayerInventory.EquippedItem != ItemEnum.PortalGun)
            {
                player.SetState(new PlayerStateLeftUse(player, position));
            }
            else if (!player.PlayerInventory.IsItemInUse(ItemEnum.PortalGun))
            {
                FirePortal();
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
            const char west = 'W';
            return west;
        }

        private void FirePortal()
        {
            player.SpawnProjectile(new PortalProjectile(Util.CompassDirection.West, position, player, portalColor));
            portalColor = portalColor == PortalColor.Blue ? PortalColor.Orange : PortalColor.Blue;
            SetSprite();
        }

        private void SetSprite()
        {
            if (player is Player1)
            {
                Sprite = portalColor == PortalColor.Blue ? PlayerSpriteFactory.Instance.CreateLinkPortalBlueLeftSprite() : PlayerSpriteFactory.Instance.CreateLinkPortalOrangeLeftSprite();
            }
            else
            {
                Sprite = portalColor == PortalColor.Blue ? PlayerSpriteFactory.Instance.CreateZeldaPortalBlueLeftSprite() : PlayerSpriteFactory.Instance.CreateZeldaPortalOrangeLeftSprite();
            }
        }
    }
}
