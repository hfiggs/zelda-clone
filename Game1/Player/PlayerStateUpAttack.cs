/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class PlayerStateUpAttack : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

         public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 0f; // ms per frame
        private const int animationFrames = 4;

        public PlayerStateUpAttack(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateAttackUpSprite();

            this.position = position;

            frameCount = 0;
            timeUntilNextFrame = animationTime;

            AudioManager.PlayFireForget("sword");
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

            if (timeUntilNextFrame <= 0)
            {
                switch (frameCount)
                {
                    case 0:
                        //frames before the attacking sprites are skipped
                        Sprite.Update();
                        frameCount++;
                        break;
                    case 1:
                        //attcking sprite 1, sword is not out
                        Sprite.Update();
                        timeUntilNextFrame += 75.0f;
                        frameCount++;
                        break;
                    case 2:
                    case 3:
                        //player looped back to start
                        Sprite.Update();
                        timeUntilNextFrame += 175.0f;
                        player.SetSwordHitbox(new Rectangle(14, 1, 12, 12));
                        frameCount++;
                        break;
                    case 4:
                        Sprite.Update();
                        timeUntilNextFrame += 175.0f;
                        player.SetSwordHitbox(new Rectangle());
                        player.SetState(new PlayerStateUp(player, position));
                        break;
                }
            }
        }

        public char GetDirection()
        {
            return 'N';
        }
    }
}
