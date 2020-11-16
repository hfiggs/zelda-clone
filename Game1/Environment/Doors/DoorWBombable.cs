﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;

namespace Game1.Environment
{
    class DoorWBombable : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public bool open = false;

        private const float topLayer = 1f;

        //private Rectangle openHitbox1 = new Rectangle(0, 0, 32, 8);
        //private Rectangle openHitbox2 = new Rectangle(0, 24, 32, 8);

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorWBombable(Vector2 position, bool isOpen)
        {
            sprite = EnvironmentSpriteFactory.instance.CreateDoorWBlank();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);

            if (isOpen)
            {
                OpenDoor(false);
            }
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if(open)
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

        public void OpenDoor(bool shouldPlaySound)
        {
            open = true;
            sprite = EnvironmentSpriteFactory.instance.CreateDoorWHole();
            hitboxes = new List<Rectangle>();
            // hitboxes.Add(openHitbox1);
            // hitboxes.Add(openHitbox2);

            if (shouldPlaySound)
            {
                AudioManager.PlayFireForget("reveal");
            }
        }
    }
}
