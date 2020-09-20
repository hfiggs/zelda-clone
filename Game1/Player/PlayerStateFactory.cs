/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class PlayerStateFactory
    {
        private IPlayerState state;
        private SpriteBatch spriteBatch;

        public static PlayerStateFactory Instance { get; } = new PlayerStateFactory();

        private PlayerStateFactory()
        {
        }

        public void Initialize(SpriteBatch spriteBatch)
        {
            state = new PlayerStateRight(this);

            this.spriteBatch = spriteBatch;
        }

        public void SetState(IPlayerState state)
        {
            this.state = state;
        }

        public void Draw(Vector2 position)
        {
            state.Sprite.Draw(spriteBatch, position);
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

        public void UseItem()
        {
            state.UseItem();
        }

        public void Attack()
        {
            state.Attack();
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
        }

        public void Update(GameTime time)
        {
            state.Update(time);
        }

    }
}
