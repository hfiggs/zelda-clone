/* Author: Hunter Figgs */

using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class PlayerStateRightUse : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }
        private IProjectile projectile;

        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 150f; // ms per frame
        private const int animationFrames = 3;

        public PlayerStateRightUse(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateUseItemRightSprite();

            this.position = position;

            frameCount = 0;
            timeUntilNextFrame = animationTime;

            switch (player.GetItem())
            {
                case 1:
                    projectile = new Arrow('E', new Vector2(position.X + 50, position.Y + 40));
                    break;
                case 2:
                    projectile = new Boomerang('E', player);
                    break;
                case 3:
                    projectile = new BombProjectile(new Vector2(position.X + 60 ,position.Y + 40));
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
                player.spawnProjectile(projectile);
                player.SetState(new PlayerStateRight(player, position));
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'E';
        }
    }
}
