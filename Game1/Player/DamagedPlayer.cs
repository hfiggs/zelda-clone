/* Author: Hunter Figgs */

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

        Vector2 damageMove = new Vector2(8,8); //magnitude of damage move per frame. If the player should slide 30 pixels, this value should be (6,6). 
        //That way the player slides 30 pixels over the course of 5 frames.

        const int duration = 1000; // ms
        int timer;
        int flickerTimer;

        const int flickerDuration = 45; // ms

        private Rectangle playerHitbox = new Rectangle(6, 18, 15, 10);
        private Rectangle swordHitbox = new Rectangle(0, 0, 0, 0);

        int frameCounter;
        int slideFrames;
        public DamagedPlayer(Game1 game, IPlayer decoratedPlayer, Vector2 direction)
        {
            this.game = game;
            this.decoratedPlayer = decoratedPlayer;
            damageMove = Vector2.Multiply(damageMove, direction);
            timer = duration;

            flickerTimer = 0;
            frameCounter = 0;
            slideFrames = 4;
        }

        public void Attack()
        {
            if (frameCounter > slideFrames)
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
            if(frameCounter > slideFrames)
                decoratedPlayer.MoveDown();
        }

        public void MoveLeft()
        {
            if(frameCounter > slideFrames)
                decoratedPlayer.MoveLeft();
        }

        public void MoveRight()
        {
           if(frameCounter > slideFrames)
                decoratedPlayer.MoveRight();
        }

        public void MoveUp()
        {
            if(frameCounter > slideFrames)
                decoratedPlayer.MoveUp();
        }

        public void ReceiveDamage(Vector2 direction)
        {
            // does not recieve damage
        }

        public void Update(GameTime time)
        {
            timer -= (int)time.ElapsedGameTime.TotalMilliseconds;
            flickerTimer += (int)time.ElapsedGameTime.TotalMilliseconds;

            if (timer <= 0)
                RemoveDecorator();

            if (frameCounter <= slideFrames)
                decoratedPlayer.editPosition(damageMove);

            frameCounter++;
            decoratedPlayer.Update(time);
        }

        public void UseItem(int item)
        {
            if(frameCounter > slideFrames)
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

        public void editPosition(Vector2 amount)
        {
            decoratedPlayer.editPosition(amount);
        }

        public Rectangle GetPlayerHitbox()
        {
            return new Rectangle(playerHitbox.Location + decoratedPlayer.GetLocation().Location, playerHitbox.Size);
        }

        public Rectangle GetSwordHitbox()
        {
            return new Rectangle(swordHitbox.Location + decoratedPlayer.GetLocation().Location, swordHitbox.Size);
        }

        public void SetSwordHitbox(Rectangle newHitbox)
        {
            swordHitbox = newHitbox;
        }
    }
}
