/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Audio;
using Game1.GameState;
using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    class Player1 : IPlayer
    {
        private readonly Game1 game;
        private IPlayerState state;

        private double timeUntilNextSwordBeam;
        private const int swordBeamCooldown = 800; // ms

        private bool isLowHealth = false;

        private const int xDiff = 13, yDiff = 20, width = 15, height = 10;
        private Rectangle playerHitbox = new Rectangle(xDiff, yDiff, width, height);
        private Rectangle swordHitbox = new Rectangle();

        private const int lowHealthHalfHearts = 2;
        private SoundEffectInstance lowHealthSound;

        public IPlayerInventory PlayerInventory { get; private set; }

        public Player1(Game1 game, Vector2 position)
        {
            this.game = game;
            state = new PlayerStateDown(this, position);
            timeUntilNextSwordBeam = -1; // to ensure time is <= 0

            PlayerInventory = new PlayerInventory1();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Sprite.Draw(spriteBatch,state.position,color);
        }

        public void MoveLeft() => state.MoveLeft();

        public void MoveRight() => state.MoveRight();

        public void MoveUp() => state.MoveUp();

        public void MoveDown() => state.MoveDown();

        public void UseItem()
        {
            ItemEnum item = PlayerInventory.EquippedItem;

            if (PlayerInventory.HasItem(item) && !PlayerInventory.IsItemInUse(item))
            {
                if ((item == ItemEnum.Bow && PlayerInventory.RupeeCount == 0) || (item == ItemEnum.Bomb && PlayerInventory.BombCount == 0) || (item == ItemEnum.BluePotion && PlayerInventory.BluePotionCount == 0) || (PlayerInventory.HasItem(ItemEnum.Boomerang) && !!PlayerInventory.IsItemInUse(ItemEnum.Boomerang)))
                    return;

                state.UseItem();
            }
        }

        public void Attack()
        {
            state.Attack();

            if(timeUntilNextSwordBeam <= 0 && PlayerInventory.HalfHeartCount == PlayerInventory.MaxHalfHearts)
            {
                game.Screen.CurrentRoom.SpawnProjectile(new SwordBeam(state.GetDirection(), state.position));

                timeUntilNextSwordBeam = swordBeamCooldown;
            }
        }

        public void ReceiveDamage(int halfHearts, Vector2 direction)
        {
            game.Screen.Player = new DamagedPlayer(game, this, direction);
            PlayerInventory.SubHealth(1);

            if(PlayerInventory.HalfHeartCount <= 0)
            {
                game.SetState(new GameStateLose(game));
            }
            if(PlayerInventory.HalfHeartCount <= lowHealthHalfHearts && !isLowHealth)
            {
                const string lowHealthAudio = "lowHealth";
                lowHealthSound = AudioManager.PlayLooped(lowHealthAudio);
                isLowHealth = true;
            }
        }

        public void Update(GameTime time)
        {
            state.Update(time);

            timeUntilNextSwordBeam -= time.ElapsedGameTime.TotalMilliseconds;

            if (PlayerInventory.HalfHeartCount > lowHealthHalfHearts && isLowHealth)
            {
                AudioManager.StopSound(lowHealthSound);
                isLowHealth = false;
            }
        }

        public char GetDirection() => state.GetDirection();

        public void SetState(IPlayerState state) => this.state = state;

        public void SpawnProjectile(IProjectile projectile)
        {
            game.Screen.CurrentRoom.SpawnProjectile(projectile);
        }

        public void EditPosition(Vector2 amount)
        {
            state.position = Vector2.Add(state.position, amount);
        }

        public Rectangle GetPlayerHitbox()
        {
            return new Rectangle(playerHitbox.Location + state.position.ToPoint(), playerHitbox.Size);
        }

        public Rectangle GetSwordHitbox()
        {
            if(swordHitbox.IsEmpty)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            return new Rectangle(swordHitbox.Location + state.position.ToPoint(), swordHitbox.Size);
        }

        public void SetSwordHitbox(Rectangle newHitbox) => swordHitbox = newHitbox;
    }
}
