/* Author: Hunter Figgs */

using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class DamagedPlayer : IPlayer
    {
        Game1 game;
        IPlayer decoratedPlayer;
        Color[] flickers = { Color.LightBlue, Color.Orange, Color.Red };
        int currentFlicker = 0;
        Color damageColor = Color.White;

        const int duration = 1000; // ms
        int timer;
        int flickerTimer;

        const int flickerDuration = 62; // ms

        public DamagedPlayer(Game1 game, IPlayer decoratedPlayer)
        {
            this.game = game;
            this.decoratedPlayer = decoratedPlayer;
            timer = duration;
            flickerTimer = 0;
        }

        public void Attack()
        {
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
            decoratedPlayer.MoveDown();
        }

        public void MoveLeft()
        {
            decoratedPlayer.MoveLeft();
        }

        public void MoveRight()
        {
            decoratedPlayer.MoveRight();
        }

        public void MoveUp()
        {
            decoratedPlayer.MoveUp();
        }

        public void ReceiveDamage()
        {
            // does not recieve damage
        }

        public void Update(GameTime time)
        {
            timer -= (int)time.ElapsedGameTime.TotalMilliseconds;
            flickerTimer += (int)time.ElapsedGameTime.TotalMilliseconds;

            if (timer <= 0)
                RemoveDecorator();

            decoratedPlayer.Update(time);
        }

        public void UseItem(int item)
        {
            decoratedPlayer.UseItem(item);
        }

        private void RemoveDecorator()
        {
            game.Player = decoratedPlayer;
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
            game.SpawnProjectile(projectile);
        }

        public void setItemUsable(int item)
        {
            decoratedPlayer.setItemUsable(item);
        }

        public void setItemNotUsable()
        {
            decoratedPlayer.setItemNotUsable();
        }
    }
}
