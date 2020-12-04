﻿/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStatePortalDown : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms

        private const float y = 1.33f;
        private Vector2 moveSpeed = new Vector2(0, y);
        private const float animationTime = 150f; // ms per frame

        private PortalColor portalColor;

        public PlayerStatePortalDown(IPlayer player, Vector2 position, PortalColor portalColor = PortalColor.Blue)
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
            player.SetState(new PlayerStateDownAttack(player, position));
        }

        public void MoveDown()
        {
            isMoving = true;
        }

        public void MoveLeft()
        {
            if (!isMoving)
                player.SetState(new PlayerStatePortalLeft(player, position, portalColor));
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
                player.SetState(new PlayerStateDownUse(player, position));
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
            const char south = 'S';
            return south;
        }

        private void FirePortal()
        {
            player.SpawnProjectile(new PortalProjectile(Util.CompassDirection.South, position, player, portalColor));
            portalColor = portalColor == PortalColor.Blue ? PortalColor.Orange : PortalColor.Blue;
            SetSprite();
        }

        private void SetSprite()
        {
            if (player is Player1)
            {
                Sprite = portalColor == PortalColor.Blue ? PlayerSpriteFactory.Instance.CreateLinkPortalBlueDownSprite() : PlayerSpriteFactory.Instance.CreateLinkPortalOrangeDownSprite();
            }
            else
            {
                Sprite = portalColor == PortalColor.Blue ? PlayerSpriteFactory.Instance.CreateZeldaPortalBlueDownSprite() : PlayerSpriteFactory.Instance.CreateZeldaPortalOrangeDownSprite();
            }
        }
    }
}
