using Game1.Player;
using Game1.Player.PlayerInventory;
using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    public enum PortalColor
    {
        Blue = 0,
        Orange = 1
    }

    class PortalProjectile : IProjectile
    {
        private ISprite sprite;
        private Vector2 position;

        public PortalColor PortalColor { get; set; }
        private readonly CompassDirection direction;

        private const int hitboxOffset = 15, hitboxDiameter = 10;

        private readonly Vector2 moveVector;
        private const float moveSpeed = 0.15f;
        private bool shouldDelete = false;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 100f; // ms per frame

        private float timeUntilDespawn; // ms
        private const float aliveTime = 2000; // ms

        public IPlayer Player { get; private set; }

        public PortalProjectile(CompassDirection direction, Vector2 position, IPlayer player, PortalColor portalColor)
        {
            this.position = position;
            Player = player;
            Player.PlayerInventory.SetItemInUse(ItemEnum.PortalGun, true);

            PortalColor = portalColor;

            this.direction = direction;

            SetSprite();

            moveVector = Vector2.Multiply(CompassDirectionUtil.GetDirectionVector(direction), moveSpeed);

            timeUntilNextFrame = animationTime;
            timeUntilDespawn = aliveTime;
        }

        public void Update(GameTime gameTime)
        {
            position = Vector2.Add(position, Vector2.Multiply(moveVector, (float)gameTime.ElapsedGameTime.TotalMilliseconds));

            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }

            timeUntilDespawn -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilDespawn <= 0)
            {
                BeginDespawn();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Rectangle GetHitbox()
        {

            return new Rectangle((int)position.X + hitboxOffset, (int)position.Y + hitboxOffset, hitboxDiameter, hitboxDiameter);
        }

        public bool ShouldDelete()
        {
            return shouldDelete;
        }

        public void BeginDespawn()
        {
            shouldDelete = true;
            Player.PlayerInventory.SetItemInUse(ItemEnum.PortalGun, false);
        }

        public void EditPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }

        private void SetSprite()
        {
            switch (direction)
            {
                case CompassDirection.North:
                    sprite = PortalColor == PortalColor.Blue ? ProjectileSpriteFactory.Instance.CreatePortalBlueUpProjectileSprite() : ProjectileSpriteFactory.Instance.CreatePortalOrangeUpProjectileSprite();
                    break;
                case CompassDirection.East:
                    sprite = PortalColor == PortalColor.Blue ? ProjectileSpriteFactory.Instance.CreatePortalBlueRightProjectileSprite() : ProjectileSpriteFactory.Instance.CreatePortalOrangeRightProjectileSprite();
                    break;
                case CompassDirection.South:
                    sprite = PortalColor == PortalColor.Blue ? ProjectileSpriteFactory.Instance.CreatePortalBlueDownProjectileSprite() : ProjectileSpriteFactory.Instance.CreatePortalOrangeDownProjectileSprite();
                    break;
                case CompassDirection.West:
                    sprite = PortalColor == PortalColor.Blue ? ProjectileSpriteFactory.Instance.CreatePortalBlueLeftProjectileSprite() : ProjectileSpriteFactory.Instance.CreatePortalOrangeLeftProjectileSprite();
                    break;
            }
        }
    }
}
