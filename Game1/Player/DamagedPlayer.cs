/* Author: Hunter Figgs */

using Game1.Audio;
using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class DamagedPlayer : IPlayer
    {
        private Game1 game;
        private IPlayer decoratedPlayer;
        private Color[] flickers = { Color.LightBlue, Color.Orange, Color.Red };
        private int currentFlicker = 0;
        private Color damageColor = Color.White;

        private const float xAndYMovementMagnitude = 0.66f;
        private Vector2 damageMove = new Vector2(xAndYMovementMagnitude,xAndYMovementMagnitude); 

        private const int duration = 1000; // ms
        private int timer;
        private int flickerTimer;
        private const int timerMax = 925;

        private const int flickerDuration = 45; // ms

        public bool stillSlide;

        public IPlayerInventory PlayerInventory { get => decoratedPlayer.PlayerInventory;}

        private const float volume = 0.25f;

        public DamagedPlayer(Game1 game, IPlayer decoratedPlayer, Vector2 direction)
        {
            this.game = game;
            this.decoratedPlayer = decoratedPlayer;
            damageMove = Vector2.Multiply(damageMove, direction);
            timer = duration;
            stillSlide = true;
            flickerTimer = 0;
            AudioManager.PlayFireForget("linkHurt", 0.0f, volume);
        }

        public void Attack()
        {
            if (timer <= timerMax || !stillSlide)
                decoratedPlayer.Attack();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            
            if (flickerTimer >= flickerDuration)
            {
                flickerTimer = 0;
                if (currentFlicker >= flickers.Length)
                {
                    currentFlicker = 0;
                    damageColor = color;
                }
                else
                    damageColor = flickers[currentFlicker];
               
                currentFlicker++;
            }
            decoratedPlayer.Draw(spriteBatch, damageColor);
        }

        public char GetDirection()
        {
            return decoratedPlayer.GetDirection();
        }

        public Rectangle GetLocation()
        {
            return decoratedPlayer.GetLocation();
        }

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

        public void ReceiveDamage(int halfHearts, Vector2 direction)
        {
            // cannot not recieve damage while decorated
        }

        public void Update(GameTime time)
        {
            timer -= (int)time.ElapsedGameTime.TotalMilliseconds;
            flickerTimer += (int)time.ElapsedGameTime.TotalMilliseconds;
            if (timer <= 0)
                RemoveDecorator();

            if (timer >= timerMax && stillSlide)
                decoratedPlayer.EditPosition(Vector2.Multiply(damageMove, (float)(time.ElapsedGameTime.TotalMilliseconds)));
            else
                stillSlide = false;

            decoratedPlayer.Update(time);
        }

        public void UseItem()
        {
            if(timer >= 0)
                decoratedPlayer.UseItem();
        }

        private void RemoveDecorator()
        {
            game.Screen.Player = decoratedPlayer;
        }

        public void SetState(IPlayerState state)
        {
            decoratedPlayer.SetState(state);
        }

        public void SpawnProjectile(IProjectile projectile)
        {
            game.Screen.CurrentRoom.SpawnProjectile(projectile);
        }

        public void EditPosition(Vector2 amount)
        {
             decoratedPlayer.EditPosition(amount);
        }

        public Rectangle GetPlayerHitbox()
        {
            return decoratedPlayer.GetPlayerHitbox();
        }

        public Rectangle GetSwordHitbox()
        {
            return decoratedPlayer.GetSwordHitbox();
        }

        public void SetSwordHitbox(Rectangle newHitbox)
        {
            decoratedPlayer.SetSwordHitbox(newHitbox);
        }

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

        public bool getBoomerangOut()
        {
            return decoratedPlayer.getBoomerangOut();
        }

        public void setBoomerangOut(bool val)
        {
            decoratedPlayer.setBoomerangOut(val);
        }
    }
}
