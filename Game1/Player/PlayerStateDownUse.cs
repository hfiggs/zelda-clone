/* Author: Hunter Figgs */

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

  public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 150f; // ms per frame
        private const int animationFrames = 3;

        public PlayerStateDownUse(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateUseItemDownSprite();

            this.position = position;

            frameCount = 0;
            timeUntilNextFrame = animationTime;

            ItemEnum item = player.PlayerInventory.EquippedItem;
            player.PlayerInventory.SetItemInUse(item, true);
            const char south = 'S';

            switch (item)
            {
                case ItemEnum.Bow:
                    player.PlayerInventory.SubRupees(1);
                    projectile = new Arrow(south, new Vector2(position.X,position.Y), player);
                    break;
                case ItemEnum.Boomerang:
                    projectile = new Boomerang(south, player);
                    break;
                case ItemEnum.Bomb:
                    player.PlayerInventory.SubBomb();
                    projectile = new BombProjectile(new Vector2(position.X, position.Y), player);
                    break;
                default:
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

            if(timeUntilNextFrame <= 0 && frameCount < animationFrames)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
                frameCount++;
            }
            else if(frameCount == animationFrames)
            {
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
