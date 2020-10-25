/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Player.PlayerInventory;
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

        private double timeUntilNextSwordBeam;
        private const int swordBeamCooldown = 800; // ms

        private bool isFullHealth;
        private bool[] itemsHeld = { true, true, true }; //Bow, Boomerang, Bomb

        private Rectangle playerHitbox = new Rectangle(13, 20, 15, 10);
        private Rectangle swordHitbox = new Rectangle(0, 0, 0, 0);

        public IPlayerInventory PlayerInventory { get; private set; }

        public Player1(Game1 game, Vector2 position)
        {
            this.game = game;
            state = new PlayerStateRight(this, position);
            timeUntilNextSwordBeam = -1; // to ensure time is <= 0
            isFullHealth = true;

            PlayerInventory = new PlayerInventory1();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Sprite.Draw(spriteBatch,state.position,color);
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
                game.SpawnProjectile(new SwordBeam(state.GetDirection(), state.position));

                timeUntilNextSwordBeam = swordBeamCooldown;
            }
        }

        public void ReceiveDamage(int halfHearts, Vector2 direction)
        {
            // wrap damage decorator around this
            game.Player = new DamagedPlayer(game, this, direction);

            isFullHealth = false;
        }

        public void Update(GameTime time)
        {
            state.Update(time);

            timeUntilNextSwordBeam -= time.ElapsedGameTime.TotalMilliseconds;
        }

        public Rectangle GetLocation()
        {
            Vector2 position = state.position;
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
            game.SpawnProjectile(projectile);
        }

        public void setItemUsable(int item)
        {
            itemsHeld[item - 1] = true;
        }

        public void setItemNotUsable()
        {
            itemsHeld[currentItem - 1] = false;
        }

        public void editPosition(Vector2 amount)
        {
            state.position = Vector2.Add(state.position, amount);
        }

        public Rectangle GetPlayerHitbox()
        {
            return new Rectangle(playerHitbox.Location + state.position.ToPoint(), playerHitbox.Size);
        }

        public Rectangle GetSwordHitbox()
        {
            return new Rectangle(swordHitbox.Location + state.position.ToPoint(), swordHitbox.Size);
        }

        public void SetSwordHitbox(Rectangle newHitbox)
        {
            swordHitbox = newHitbox;
        }
    }
}
