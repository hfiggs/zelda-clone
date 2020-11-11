﻿/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class PlayerStateLeftAttack : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

  public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 0f; // ms per frame

        public PlayerStateLeftAttack(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateAttackLeftSprite();

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
                        const float timeToNextFrame1 = 75f;
                        timeUntilNextFrame += timeToNextFrame1;
                        frameCount++;
                        break;
                    case 2:
                    case 3:
                        //player looped back to start
                        Sprite.Update();
                        const float timeToNextFrame2 = 175f;
                        timeUntilNextFrame += timeToNextFrame2;
                        const int yDiff = 16, widthAndHeight = 12;
                        player.SetSwordHitbox(new Rectangle(0, yDiff, widthAndHeight, widthAndHeight));
                        frameCount++;
                        break;
                    case 4:
                        Sprite.Update();
                        const float timeToNextFrame3 = 175f;
                        timeUntilNextFrame += timeToNextFrame3;
                        player.SetSwordHitbox(new Rectangle());
                        player.SetState(new PlayerStateLeft(player, position));
                        break;
                }
            }
        }

        public char GetDirection()
        {
            const char west = 'W';
            return west;
        }
    }
}
