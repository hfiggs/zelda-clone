using Game1.Audio;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Environment
{
    class BlackSelfDestruct : IEnvironment
    {
        private ISprite sprite;
        Vector2 position;

        const int widthAndHeight = 16;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        private float existTime; //ms
        private bool exists = true;

        public BlackSelfDestruct(Vector2 position, float existTime)
        {
            sprite = EnvironmentSpriteFactory.instance.CreateBlack();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            this.existTime = existTime;
        }

        public void Update(GameTime gameTime)
        {
            const string letterAppearSound = "linkPop";

            if(exists && (existTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds) <= 0)
            {
                exists = false;
                AudioManager.PlayFireForget(letterAppearSound);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (exists)
            {
                sprite.Draw(spriteBatch, position, color, Util.SpriteLayerUtil.topLayer);
            }
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
