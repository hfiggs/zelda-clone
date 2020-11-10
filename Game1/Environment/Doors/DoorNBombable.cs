﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorNBombable : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public bool open = false;

        private const float topLayer = 1f;

        const int width = 8, height = 32, xDiff = 24;
        private Rectangle openHitbox1 = new Rectangle(0, 0, width, height);
        private Rectangle openHitbox2 = new Rectangle(xDiff, 0, width, height);

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorNBombable(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNBlank();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (open)
            {
                sprite.Draw(spriteBatch, position, color, topLayer);
            }
            else
            {
                sprite.Draw(spriteBatch, position, color);
            }
        }
        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public void openDoor()
        {
            open = true;
            sprite = EnvironmentSpriteFactory.instance.createDoorNHole();
            hitboxes = new List<Rectangle>();
            hitboxes.Add(openHitbox1);
            hitboxes.Add(openHitbox2);
            AudioManager.PlayFireForget("reveal");
        }
    }
}
