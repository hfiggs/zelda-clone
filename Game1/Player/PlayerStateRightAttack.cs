/* Author: Hunter Figgs */

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

        private const float animationTime = 0f; // ms per frame
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
                        //attacking sprite 2, sword is out
                        Sprite.Update();
                        timeUntilNextFrame += 175.0f;
                        frameCount++;
                        player.SetSwordHitbox(new Rectangle(21, 19, 12, 5));
                        break;
                    case 3:
                        //player looped back to start
                        player.SetState(new PlayerStateRight(player, position));
                        player.SetSwordHitbox(new Rectangle(0, 0, 0, 0));
                        break;
                }
            }
        }

        public char GetDirection()
        {
            return 'E';
        }
    }
}
