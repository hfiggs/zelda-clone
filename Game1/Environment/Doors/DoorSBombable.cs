﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorSBombable : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public bool open = false;

        private const float topLayer = 1f;

        //private Rectangle openHitbox1 = new Rectangle(0, 0, 8, 32);
        //private Rectangle openHitbox2 = new Rectangle(24, 0, 8, 32);

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorSBombable(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorSBlank();
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
            sprite = EnvironmentSpriteFactory.instance.createDoorSHole();
            hitboxes = new List<Rectangle>();
            //hitboxes.Add(openHitbox1);
            //hitboxes.Add(openHitbox2);
            AudioManager.PlayFireForget("reveal");
        }
    }
}
