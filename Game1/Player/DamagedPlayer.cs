/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;

namespace Game1.Player
{
    class DamagedPlayer : IPlayer
    {
        Game1 game;
        IPlayer decoratedPlayer;

        const int duration = 1000; // ms
        int timer;

        public DamagedPlayer(Game1 game, IPlayer decoratedPlayer)
        {
            this.game = game;
            this.decoratedPlayer = decoratedPlayer;

            timer = duration;
        }

        public void Attack()
        {
            decoratedPlayer.Attack();
        }

        public void Draw(Color color)
        {
            decoratedPlayer.Draw(Color.Red);
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
    }
}
