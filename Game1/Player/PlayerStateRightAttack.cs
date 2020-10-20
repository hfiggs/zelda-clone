﻿/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class PlayerStateRightAttack : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

  public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms
        private int frameCount;

        private const float animationTime = 150f; // ms per frame
        private const int animationFrames = 4;

        public PlayerStateRightAttack(IPlayer player, Vector2 position)
        {
            this.player = player;
            Sprite = PlayerSpriteFactory.Instance.CreateAttackRightSprite();

            this.position = position;

            frameCount = 0;
            timeUntilNextFrame = animationTime;
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
                player.SetState(new PlayerStateRight(player, position));
            }
        }

        public char GetDirection()
        {
            return 'E';
        }
    }
}
