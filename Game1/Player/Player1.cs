/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        private bool isLowHealth = false;
        private bool[] itemsHeld = { true, true, true }; //Bow, Boomerang, Bomb

        //prevents the player from animating to "catch" the boomerang while it is in the air
        private bool boomerangOut;

        private Rectangle playerHitbox = new Rectangle(13, 20, 15, 10);
        private Rectangle swordHitbox = new Rectangle();

        private readonly float deathSoundLength = 2.5f;
        private readonly float gameOverDealy = 4.5f;
        private readonly float gameOverVolume = 0.6f;
        SoundEffectInstance lowHealthSound;

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

        public void UseItem()
        {
            ItemEnum item = PlayerInventory.EquippedItem;

            if (PlayerInventory.HasItem(item))
            {
                if (!PlayerInventory.IsItemInUse(item))
                {
                    if ((item == ItemEnum.Bow && PlayerInventory.RupeeCount >= 1) || (item == ItemEnum.Boomerang) || (item == ItemEnum.Bomb && PlayerInventory.BombCount >= 1))
                    {
                        state.UseItem();
                        return;
                    }
                }
                else if (item == ItemEnum.Boomerang)
                {
                    state.UseItem();
                }
            }
        }

        public void Attack()
        {
            state.Attack();

            if(timeUntilNextSwordBeam <= 0 && isFullHealth)
            {
                game.Screen.SpawnProjectile(new SwordBeam(state.GetDirection(), state.position));

                timeUntilNextSwordBeam = swordBeamCooldown;
            }
        }

        public void ReceiveDamage(int halfHearts, Vector2 direction)
        {
            // wrap damage decorator around this
            game.Screen.Player = new DamagedPlayer(game, this, direction);
            PlayerInventory.SubHealth(1);
            isFullHealth = false;
            //should be slightly modified once we have health mechanics - GameState?
            if(PlayerInventory.HalfHeartCount <= 0)
            {
                AudioManager.StopAllMusic();
                AudioManager.StopAllSound();
                AudioManager.PlayFireForget("death");
                AudioManager.PlayFireForget("linkPop", deathSoundLength);
                AudioManager.PlayLooped("gameOver", gameOverDealy, gameOverVolume);
            }
            if(PlayerInventory.HalfHeartCount <= 2 && !isLowHealth)
            {
                lowHealthSound = AudioManager.PlayLooped("lowHealth");
                isLowHealth = true;
            }
        }

        public void Update(GameTime time)
        {
            state.Update(time);

            timeUntilNextSwordBeam -= time.ElapsedGameTime.TotalMilliseconds;

            if (PlayerInventory.HalfHeartCount > 2 && isLowHealth)
            {
                AudioManager.StopSound(lowHealthSound);
                isLowHealth = false;
            }
        }

        public Rectangle GetLocation()
        {
            return new Rectangle(playerHitbox.Location + state.position.ToPoint(), playerHitbox.Size);
        }

        public char GetDirection()
        {
            return state.GetDirection();
        }

        public void SetState(IPlayerState state)
        {
            this.state = state;
        }

        public void SpawnProjectile(IProjectile projectile)
        {
            game.Screen.SpawnProjectile(projectile);
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
            return new Rectangle(swordHitbox.Location + state.position.ToPoint(), swordHitbox.Size);
        }

        public void SetSwordHitbox(Rectangle newHitbox)
        {
            swordHitbox = newHitbox;
        }

        public bool getBoomerangOut()
        {
            return boomerangOut;
        }

        public void setBoomerangOut(bool val)
        {
            boomerangOut = val;
        }
    }
}
