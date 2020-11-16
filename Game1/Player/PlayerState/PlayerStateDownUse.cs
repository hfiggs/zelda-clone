﻿/* Author: Hunter Figgs */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStateDownUse : IPlayerState
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

        private const int bluePotionHalfHearts = 26; // 16 full hearts (max from all heart pieces)

        public PlayerStateDownUse(IPlayer player, Vector2 position)
        {
            this.player = player;

            this.position = position;

            item = player.PlayerInventory.EquippedItem;
            if(player.PlayerInventory.IsItemInUse(item) && item == ItemEnum.Boomerang)
            {
                item = 0;
            }
            player.PlayerInventory.SetItemInUse(item, true);
            const char south = 'S';

            switch (item)
            {
                case ItemEnum.Bow:
                    player.PlayerInventory.SubRupees(1);
                    projectile = new Arrow(south, new Vector2(position.X,position.Y), player);
                    Sprite = PlayerSpriteFactory.Instance.CreateUseItemDownSprite();
                    break;
                case ItemEnum.Boomerang:
                    projectile = new Boomerang(south, player);
                    Sprite = PlayerSpriteFactory.Instance.CreateUseItemDownSprite();
                    break;
                case ItemEnum.Bomb:
                    player.PlayerInventory.SubBomb();
                    projectile = new BombProjectile(new Vector2(position.X, position.Y), player);
                    Sprite = PlayerSpriteFactory.Instance.CreateUseItemDownSprite();
                    break;
                case ItemEnum.BluePotion:
                    player.PlayerInventory.SubBluePotion();
                    player.PlayerInventory.AddHealth(bluePotionHalfHearts);
                    Sprite = PlayerSpriteFactory.Instance.CreateIdleDownSprite();
                    break;
                case ItemEnum.BlueCandle:
                    const int xModifier = 12, yModifier = 30;
                    projectile = new CandleFire(south, position + new Vector2(xModifier, yModifier), player);
                    Sprite = PlayerSpriteFactory.Instance.CreateUseItemDownSprite();
                    break;
                default:
                    Sprite = PlayerSpriteFactory.Instance.CreateUseItemDownSprite();
                    break;
            }
        }

        public void Attack()
        {
            // Do nothing
        }

        public void MoveDown()
        {
            // Do nothing
        }

        public void MoveLeft()
        {
            // Do nothing
        }

        public void MoveRight()
        {
            // Do nothing
        }

        public void MoveUp()
        {
            // Do nothing
        }
        public void UseItem()
        {
            // Do nothing
        }
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
            else if(frameCount == animationFrames)
            {
                if(item != 0 && item != (ItemEnum)bluePotion)
                    player.SpawnProjectile(projectile);
                player.SetState(new PlayerStateDown(player, position));
            }
        }

        public char GetDirection()
        {
            const char south = 'S';
            return south;
        }
    }
}
