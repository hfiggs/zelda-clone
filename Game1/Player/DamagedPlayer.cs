/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Player
{
    class DamagedPlayer : IPlayer
    {
        private readonly Game1 game;
        private readonly IPlayer decoratedPlayer;
        private readonly ColorIterator colorIterator;

        private const float xAndYMovementMagnitude = 0.33f;
        private Vector2 damageMove = new Vector2(xAndYMovementMagnitude,xAndYMovementMagnitude);
        private readonly Color[] flickerColors = { Color.LightBlue, Color.Orange, Color.Red };
        private const int flickerDuration = 45; // ms
        private const int duration = 1000; // ms
        private int timer;
        private const int timerMax = 875;

        public bool stillSlide;

        public IPlayerInventory PlayerInventory { get => decoratedPlayer.PlayerInventory;}

        public int playerID { get => decoratedPlayer.playerID; }

        public bool requesting { get => decoratedPlayer.requesting; set => decoratedPlayer.requesting = value; }

        private const float volume = 0.25f;

        public DamagedPlayer(Game1 game, IPlayer decoratedPlayer, Vector2 direction)
        {
            this.game = game;
            this.decoratedPlayer = decoratedPlayer;
            damageMove = Vector2.Multiply(damageMove, direction);
            timer = duration;
            stillSlide = true;
            const string linkHurtAudio = "linkHurt";
            AudioManager.PlayFireForget(linkHurtAudio, 0.0f, volume);

            colorIterator = new ColorIterator(new List<Color>(flickerColors), flickerDuration);
        }

        public void Attack()
        {
            if (timer <= timerMax || !stillSlide)
                decoratedPlayer.Attack();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            decoratedPlayer.Draw(spriteBatch, colorIterator.GetColor(color));
        }

        public char GetDirection() => decoratedPlayer.GetDirection();

        public void MoveDown()
        {
            if (timer <= timerMax || !stillSlide)
                decoratedPlayer.MoveDown();
        }

        public void MoveLeft()
        {
            if (timer <= timerMax || !stillSlide)
                decoratedPlayer.MoveLeft();
        }

        public void MoveRight()
        {
            if (timer <= timerMax || !stillSlide)
                decoratedPlayer.MoveRight();
        }

        public void MoveUp()
        {
            if (timer <= timerMax || !stillSlide)
                decoratedPlayer.MoveUp();
        }

        public void ReceiveDamage(int halfHearts, Vector2 direction) { /* cannot not recieve damage while decorated */ }

        public void Update(GameTime time)
        {
            timer -= (int)time.ElapsedGameTime.TotalMilliseconds;
            if (timer <= 0)
                RemoveDecorator();

            if (timer >= timerMax && stillSlide)
                decoratedPlayer.EditPosition(Vector2.Multiply(damageMove, (float)(time.ElapsedGameTime.TotalMilliseconds)));
            else
                stillSlide = false;

            decoratedPlayer.Update(time);

            colorIterator.Update(time);
        }

        public void UseItem()
        {
            if(timer >= 0)
                decoratedPlayer.UseItem();
        }

        private void RemoveDecorator() => game.Screen.Players[playerID - 1] = decoratedPlayer;

        public void SetState(IPlayerState state) => decoratedPlayer.SetState(state);

        public void SpawnProjectile(IProjectile projectile) => game.Screen.CurrentRoom.SpawnProjectile(projectile);

        public void EditPosition(Vector2 amount) => decoratedPlayer.EditPosition(amount);

        public Rectangle GetPlayerHitbox() => decoratedPlayer.GetPlayerHitbox();

        public Rectangle GetSwordHitbox() => decoratedPlayer.GetSwordHitbox();

        public void SetSwordHitbox(Rectangle newHitbox) => decoratedPlayer.SetSwordHitbox(newHitbox);

        public void StopKnockback(Vector2 possibleCorrections)
        {
            stillSlide = false;
            Vector2 correction = possibleCorrections;

            if (damageMove.X > 0)
                correction = Vector2.Multiply(correction, new Vector2(-1,0));
            else if(damageMove.X < 0)
                correction = Vector2.Multiply(correction, new Vector2(1,0));
            else if (damageMove.Y > 0)
                correction = Vector2.Multiply(correction, new Vector2(0,-1));
            else if (damageMove.Y < 0)
                correction = Vector2.Multiply(correction, new Vector2(0,1));

            decoratedPlayer.EditPosition(correction);
        }
    }
}
