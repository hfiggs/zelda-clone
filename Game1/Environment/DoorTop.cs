using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class DoorTop : IEnvironment
    {
        private SpriteSheet spriteSheet;
        public enum DoorType { Blank, Open, Locked, Blocked, Hole};

        private DoorType doorType;
        

        public DoorTop(SpriteSheet spriteSheet, DoorType doorType)
        {
            this.spriteSheet = spriteSheet;
            this.doorType = doorType;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            Rectangle sourceRect = spriteSheet.PickSprite(0, 0);
            Rectangle destRect = new Rectangle(position.ToPoint(), new Point(sourceRect.Width, sourceRect.Height));
            spritebatch.Draw(spriteSheet.GetTexture(), destRect, sourceRect, Color.White);
        }

        public void Update()
        {
            //Extensible later
        }
    }
}
