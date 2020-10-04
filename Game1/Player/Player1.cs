/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class Player1 : IPlayer
    {
        Game1 game;
        int currentItem;
        private IPlayerState state;

        public Player1(Game1 game, Vector2 position)
        {
            this.game = game;

            state = new PlayerStateRight(this, position);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Sprite.Draw(spriteBatch,state.GetPosition(),color);
        }

        public void MoveLeft()
        {
            state.MoveLeft();
        }

        public void MoveRight()
        {
           state.MoveRight();
        }

        public void MoveUp()
        {
            state.MoveUp();
        }

        public void MoveDown()
        {
            state.MoveDown();
        }

        public void UseItem(int item)
        {
            currentItem = item;
            state.UseItem();
        }

        public void Attack()
        {
            state.Attack();
        }

        public void ReceiveDamage()
        {
            // wrap damage decorator around this
            game.Player = new DamagedPlayer(game, this);
        }

        public void Update(GameTime time)
        {
            state.Update(time);
        }

        public Rectangle GetLocation()
        {
            Vector2 position = state.GetPosition();
            return new Rectangle((int)position.X,(int)position.Y,50,50);
        }

        public char GetDirection()
        {
            return state.GetDirection();
        }

        public int GetItem()
        {
            return currentItem;
        }

        public void SetState(IPlayerState state)
        {
            this.state = state;
        }

        public int getItem()
        {
            return currentItem;
        }

        public void spawnProjectile(IProjectile projectile)
        {
            game.SpawnProjectile(projectile);
        }
    }
}
