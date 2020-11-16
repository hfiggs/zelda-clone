/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class PlayerStateDownAttack : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

  public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 0f; // ms per frame - 0 to skip first 2 frames

        public PlayerStateDownAttack(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateAttackDownSprite();

            this.position = position;

            frameCount = 0;
            timeUntilNextFrame = animationTime;

            const string swordAudio = "sword";
            AudioManager.PlayFireForget(swordAudio);
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
            const int skipFramesBeforeAttackCase = 0, attackSpriteOneCase = 1, playerLoopedBackCase1 = 2, playerLoopedBackCase2 = 3, playerSwordOutCase = 4;

            if (timeUntilNextFrame <= 0)
            {
                switch(frameCount)
                {
                    case skipFramesBeforeAttackCase:
                        //frames before the attacking sprites are skipped
                        Sprite.Update();
                        frameCount++;
                        break;
                    case attackSpriteOneCase:
                        //attcking sprite 1, sword is not out
                        Sprite.Update();
                        const float timeToNextFrame1 = 75f;
                        timeUntilNextFrame += timeToNextFrame1;
                        frameCount++;
                        break;
                    case playerLoopedBackCase1:
                    case playerLoopedBackCase2:
                        //player looped back to start
                        Sprite.Update();
                        const float timeToNextFrame2 = 175f;
                        timeUntilNextFrame += timeToNextFrame2;
                        const int xDiff = 16, yDiff = 28, widthAndHeight = 12;
                        player.SetSwordHitbox(new Rectangle(xDiff, yDiff, widthAndHeight, widthAndHeight));
                        frameCount++;
                        break;
                    case playerSwordOutCase:
                        Sprite.Update();
                        const float timeToNextFrame3 = 175f;
                        timeUntilNextFrame += timeToNextFrame3;
                        player.SetSwordHitbox(new Rectangle());
                        player.SetState(new PlayerStateDown(player, position));
                        break;
                }
            }
        }

        public char GetDirection()
        {
            const char south = 'S';
            return south;
        }
    }
}
