/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class Player1 : IPlayer
    {
        Game1 game;

        public Player1(Game1 game, Vector2 position, SpriteBatch spriteBatch)
        {
            this.game = game;

            PlayerStateFactory.Instance.Initialize(spriteBatch, position);
        }

        public void Draw(Color color)
        {
            PlayerStateFactory.Instance.Draw(color);
        }

        public void MoveLeft()
        {
            PlayerStateFactory.Instance.MoveLeft();
        }

        public void MoveRight()
        {
            PlayerStateFactory.Instance.MoveRight();
        }

        public void MoveUp()
        {
            PlayerStateFactory.Instance.MoveUp();
        }

        public void MoveDown()
        {
            PlayerStateFactory.Instance.MoveDown();
        }

        public void UseItem(int item)
        {
            PlayerStateFactory.Instance.UseItem();
        }

        public void Attack()
        {
            PlayerStateFactory.Instance.Attack();
        }

        public void ReceiveDamage()
        {
            // wrap damage decorator around this
            game.Player = new DamagedPlayer(game, this);
        }

        public void Update(GameTime time)
        {
            PlayerStateFactory.Instance.Update(time);
        }

        public Rectangle GetLocation()
        {
            return PlayerStateFactory.Instance.GetLocation();
        }

        public char GetDirection()
        {
            return PlayerStateFactory.Instance.GetDirection();
        }
    }
}
