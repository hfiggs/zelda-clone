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
        const int h1Width = 32, h1Height = 72;
        private Rectangle hitbox1 = new Rectangle(0, 0, h1Width, h1Height);
        const int h2Width = 80, h2Height = 32, h2XDiff = 32;
        private Rectangle hitbox2 = new Rectangle(h2XDiff, 0, h2Width, h2Height);
        const int h3Width = 80, h3Height = 32, h3XDiff = 144;
        private Rectangle hitbox3 = new Rectangle(h3XDiff, 0, h3Width, h3Height);
        const int h4Width = 32, h4Height = 72, h4XDiff = 224;
        private Rectangle hitbox4 = new Rectangle(h4XDiff, 0, h4Width, h4Height);
        const int h5Width = 32, h5Height = 72, h5YDiff = 104;
        private Rectangle hitbox5 = new Rectangle(0, h5YDiff, h5Width, h5Height);
        const int h6Width = 80, h6Height = 32, h6XDiff = 32, h6YDiff = 144;
        private Rectangle hitbox6 = new Rectangle(h6XDiff, h6YDiff, h6Width, h6Height);
        const int h7Width = 80, h7Height = 32, h7XDiff = 144, h7YDiff = 144;
        private Rectangle hitbox7 = new Rectangle(h7XDiff, h7YDiff, h7Width, h7Height);
        const int h8Width = 32, h8Height = 72, h8XDiff = 224, h8YDiff = 104;
        private Rectangle hitbox8 = new Rectangle(h8XDiff, h8YDiff, h8Width, h8Height);

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
