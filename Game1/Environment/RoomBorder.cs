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
    class RoomBorder : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        //Top West North West
        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 72);

        private Rectangle hitbox2 = new Rectangle(32, 0, 80, 32);
        private Rectangle hitbox3 = new Rectangle(144, 0, 80, 32);
        private Rectangle hitbox4 = new Rectangle(224, 0, 32, 72);
        private Rectangle hitbox5 = new Rectangle(0, 104, 32, 72);
        private Rectangle hitbox6 = new Rectangle(32, 144, 80, 32);
        private Rectangle hitbox7 = new Rectangle(144, 144, 80, 32);
        private Rectangle hitbox8 = new Rectangle(224, 104, 32, 72);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public RoomBorder(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createRoom();
            this.position = position;
            hitboxes.Add(hitbox1);
            hitboxes.Add(hitbox2);
            hitboxes.Add(hitbox3);
            hitboxes.Add(hitbox4);
            hitboxes.Add(hitbox5);
            hitboxes.Add(hitbox6);
            hitboxes.Add(hitbox7);
            hitboxes.Add(hitbox8);
        }
public void BehaviorUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException("For collision mechanics later");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
