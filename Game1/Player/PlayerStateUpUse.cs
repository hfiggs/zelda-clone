/* Author: Hunter Figgs */

using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Player
{
    class PlayerStateUpUse : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }
        private IProjectile projectile;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 150f; // ms per frame
        private const int animationFrames = 3;

        public PlayerStateUpUse(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateUseItemUpSprite();

            this.position = position;
            frameCount = 0;
            timeUntilNextFrame = animationTime;

            switch (player.GetItem())
            {
                case 1:
                    projectile = new Arrow('N', new Vector2(position.X + 20, position.Y));
                    break;
                case 2:
                    projectile = new Boomerang('N', player);
                    break;
                case 3:
                    projectile = new BombProjectile(new Vector2(position.X + 20, position.Y));
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
                player.SetState(new PlayerStateUp(player, position));
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public char GetDirection()
        {
            return 'N';
        }
    }
}
