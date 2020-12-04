/* Author: Hunter Figgs */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStateLeftUse : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }
        private IProjectile projectile;

        private ItemEnum item;

        public Vector2 position { get; set; }

        private float timeUntilNextFrame = 0f; // ms
        private int frameCount = 0;

        private const float animationTime = 125f; // ms per frame
        private const int animationFrames = 3;

        private const int projXOffset = -4, projYOffset = 12;

        private const int bluePotionHalfHearts = 26; // 16 full hearts (max from all heart pieces)

        public PlayerStateLeftUse(IPlayer player, Vector2 position)
        {
            this.player = player;

            this.position = position;

            item = player.PlayerInventory.EquippedItem;
            if (player.PlayerInventory.IsItemInUse(item) && item == ItemEnum.Boomerang)
            {
                item = 0;
            }

            if (item != ItemEnum.PortalGun)
                player.PlayerInventory.SetItemInUse(item, true);

            const char west = 'W';

            if (player.GetType() == typeof(Player1)) {
                Sprite = PlayerSpriteFactory.Instance.CreateUseItemLeftSprite();
            } else {
                Sprite = PlayerSpriteFactory.Instance.CreateZeldaUseItemLeftSprite();
            }

            switch (item)
            {
                case ItemEnum.Bow:
                    player.PlayerInventory.SubRupees(1);
                    projectile = new Arrow(west, new Vector2(position.X, position.Y), player);
                    break;
                case ItemEnum.Boomerang:
                    projectile = new Boomerang(west, player);
                    break;
                case ItemEnum.Bomb:
                    player.PlayerInventory.SubBomb();
                    projectile = new BombProjectile(new Vector2(position.X, position.Y), player);
                    break;
                case ItemEnum.BluePotion:
                    player.PlayerInventory.SubBluePotion();
                    player.PlayerInventory.AddHealth(bluePotionHalfHearts);
                    if (player.GetType() == typeof(Player1)) {
                        Sprite = PlayerSpriteFactory.Instance.CreateIdleLeftSprite();
                    } else {
                        Sprite = PlayerSpriteFactory.Instance.CreateZeldaIdleLeftSprite();
                    }
                    break;
                case ItemEnum.BlueCandle:
                    projectile = new CandleFire(west, position + new Vector2(projXOffset, projYOffset), player);
                    break;
                case ItemEnum.PortalGun:
                default:
                    break;
            }
        }

        public void Attack() { }
        public void MoveDown() { }
        public void MoveLeft() { }
        public void MoveRight() { }
        public void MoveUp() { }
        public void UseItem() { }

        public void Update(GameTime time)
        {
            timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;
            const int bluePotion = 5;

            if (timeUntilNextFrame <= 0 && frameCount < animationFrames)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
                frameCount++;
            }
            else if (frameCount == animationFrames)
            {
                if (item != 0 && item != (ItemEnum)bluePotion && item != ItemEnum.PortalGun)
                    player.SpawnProjectile(projectile);
                player.SetState(new PlayerStateLeft(player, position));
            }

            if (item == ItemEnum.PortalGun)
            {
                player.SetState(new PlayerStatePortalLeft(player, position));
            }
        }

        public char GetDirection()
        {
            const char west = 'W';
            return west;
        }
    }
}
