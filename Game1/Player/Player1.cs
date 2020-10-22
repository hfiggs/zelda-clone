/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Player
{
    class Player1 : IPlayer
    {
        Game1 game;
        int currentItem;
        private IPlayerState state;

        private double timeUntilNextSwordBeam;
        private const int swordBeamCooldown = 800; // ms

        private bool isFullHealth;
        private bool[] itemsHeld = { true, true, true };

        private Rectangle playerHitbox = new Rectangle(13, 20, 15, 10);
        private Rectangle swordHitbox = new Rectangle(0, 0, 0, 0);

        public Player1(Game1 game, Vector2 position)
        {
            this.game = game;
            state = new PlayerStateRight(this, position);
            timeUntilNextSwordBeam = -1; // to ensure time is <= 0
            isFullHealth = true;
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
            if (itemsHeld[item - 1])
            {
                state.UseItem();
            }
        }

        public void Attack()
        {
            state.Attack();

            if(timeUntilNextSwordBeam <= 0 && isFullHealth)
            {
                game.Screen.SpawnProjectile(new SwordBeam(state.GetDirection(), state.GetPosition()));

                timeUntilNextSwordBeam = swordBeamCooldown;
            }
        }

        public void ReceiveDamage()
        {
            // wrap damage decorator around this
            game.Screen.Player = new DamagedPlayer(game, this);

            isFullHealth = false;
        }

        public void Update(GameTime time)
        {
            state.Update(time);

            timeUntilNextSwordBeam -= time.ElapsedGameTime.TotalMilliseconds;
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

        public void spawnProjectile(IProjectile projectile)
        {
            game.Screen.SpawnProjectile(projectile);
        }

        public void setItemUsable(int item)
        {
            itemsHeld[item - 1] = true;
        }

        public void setItemNotUsable()
        {
            itemsHeld[currentItem - 1] = false;
        }

        public Rectangle GetPlayerHitbox()
        {
            return new Rectangle(playerHitbox.Location + state.GetPosition().ToPoint(), playerHitbox.Size);
        }

        public Rectangle GetSwordHitbox()
        {
            return new Rectangle(swordHitbox.Location + state.GetPosition().ToPoint(), swordHitbox.Size);
        }

        public void SetSwordHitbox(Rectangle newHitbox)
        {
            swordHitbox = newHitbox;
        }
    }
}
