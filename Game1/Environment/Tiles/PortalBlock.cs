using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Environment
{
    enum PortalBlockState
    {
        Normal = 0,
        Blue = 1,
        Orange = 2
    }

    class PortalBlock : IEnvironment
    {
        private ISprite sprite;
        private readonly Vector2 position;

        private PortalBlockState state;

        const int widthAndHeight = 16;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();

        private float timeUntilNextFrame; // ms
        private const float animationTime = 100f; // ms per frame

        public PortalBlock(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.CreatePortalBlock();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);

            timeUntilNextFrame = animationTime;

            state = PortalBlockState.Normal;
        }

        public void Update(GameTime gameTime)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color, SpriteLayerUtil.envBelowPlayerLayer2);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public PortalBlockState State
        {
            get => state;
            set
            {
                switch (value)
                {
                    case PortalBlockState.Blue:
                        sprite = EnvironmentSpriteFactory.instance.CreateBluePortal();
                        break;
                    case PortalBlockState.Orange:
                        sprite = EnvironmentSpriteFactory.instance.CreateOrangePortal();
                        break;
                    case PortalBlockState.Normal:
                    default:
                        sprite = EnvironmentSpriteFactory.instance.CreatePortalBlock();
                        break;
                }
            }
        }
    }
}
