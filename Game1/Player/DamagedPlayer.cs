/* Author: Hunter Figgs */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Policy;

namespace Game1.Player
{
    class DamagedPlayer : IPlayer
    {
        Game1 game;
        IPlayer decoratedPlayer;
        Color[] flickers = { Color.LightBlue, Color.Orange, Color.Red };
        int currentFlicker = 0;
        Color damageColor = Color.White;

        Vector2 damageMove = new Vector2(1f,1f); 

        const int duration = 1000; // ms
        int timer;
        int flickerTimer;

        const int flickerDuration = 45; // ms

        private Rectangle playerHitbox = new Rectangle(13, 20, 15, 10);
        private Rectangle swordHitbox = new Rectangle();

        public bool stillSlide;

        public IPlayerInventory PlayerInventory { get => decoratedPlayer.PlayerInventory;}

        public DamagedPlayer(Game1 game, IPlayer decoratedPlayer, Vector2 direction)
        {
            this.game = game;
            this.decoratedPlayer = decoratedPlayer;
            damageMove = Vector2.Multiply(damageMove, direction);
            timer = duration;
            stillSlide = true;
            flickerTimer = 0;
        }

        public void Attack()
        {
            if (timer <= 950 || !stillSlide)
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
            if (timer <= 950 || !stillSlide)
                decoratedPlayer.MoveDown();
        }

        public void MoveLeft()
        {
            if (timer <= 950 || !stillSlide)
                decoratedPlayer.MoveLeft();
        }

        public void MoveRight()
        {
            if (timer <= 950 || !stillSlide)
                decoratedPlayer.MoveRight();
        }

        public void MoveUp()
        {
            if (timer <= 950 || !stillSlide)
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

            if (timer >= 950 && stillSlide)
                decoratedPlayer.editPosition(Vector2.Multiply(damageMove, (float)(time.ElapsedGameTime.TotalMilliseconds)));
            else
                stillSlide = false;

            decoratedPlayer.Update(time);
        }

        public void UseItem(int item)
        {
            if(timer >= 0)
                decoratedPlayer.UseItem(item);
        }

        private void RemoveDecorator()
        {
            game.Screen.Player = decoratedPlayer;
        }

        public void SetState(IPlayerState state)
        {
            decoratedPlayer.SetState(state);
        }

        public int GetItem()
        {
            return decoratedPlayer.GetItem();
        }

        public void spawnProjectile(IProjectile projectile)
        {
            game.Screen.SpawnProjectile(projectile);
        }

        public void setItemUsable(int item)
        {
            decoratedPlayer.setItemUsable(item);
        }

        public void setItemNotUsable()
        {
            decoratedPlayer.setItemNotUsable();
        }

        public void editPosition(Vector2 amount)
        {
                decoratedPlayer.editPosition(amount);
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
            swordHitbox = newHitbox;
        }

        public void stopKnockback(Vector2 possibleCorrections)
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

            decoratedPlayer.editPosition(correction);
        }
    }
}
