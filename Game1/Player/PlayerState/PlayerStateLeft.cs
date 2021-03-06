﻿/* Authors: 
 * Hunter Figgs
 * Jared Perkins
 */

using Game1.Sprite;
using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class PlayerStateLeft : IPlayerState
    {
        private IPlayer player;
        public ISprite Sprite { get; private set; }

        private bool isMoving;
        public Vector2 position { get; set; }

        private float timeUntilNextFrame; // ms

        const float x = -1.33f;
        private Vector2 moveSpeed = new Vector2(-1.33f, 0);
        private const float animationTime = 150f; // ms per frame

        public PlayerStateLeft(IPlayer player, Vector2 position)
        {
            this.player = player;

            if (player.GetType() == typeof(Player1)) {
                Sprite = PlayerSpriteFactory.Instance.CreateWalkLeftSprite();
            } else {
                Sprite = PlayerSpriteFactory.Instance.CreateZeldaWalkLeftSprite();
            }


            isMoving = false;
            timeUntilNextFrame = animationTime;

            this.position = position;
        }

        public void Attack()
        {
            player.SetState(new PlayerStateLeftAttack(player, position));
        }

        public void MoveDown()
        {
            if (!isMoving)
                player.SetState(new PlayerStateDown(player, position));
        }

        public void MoveLeft()
        {
            isMoving = true;
        }

        public void MoveRight()
        {
            if (!isMoving)
                player.SetState(new PlayerStateRight(player, position));
        }

        public void MoveUp()
        {
            if (!isMoving)
                player.SetState(new PlayerStateUp(player, position));
        }

        public void UseItem()
        {
            player.SetState(new PlayerStateLeftUse(player, position));
        }

        public void Update(GameTime time)
        {
            if (isMoving)
            {
                timeUntilNextFrame -= (float)time.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }

                position += moveSpeed;
            }

            isMoving = false;

        }

        public char GetDirection()
        {
            const char west = 'W';
            return west;
        }
    }
}
