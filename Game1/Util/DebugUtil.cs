/* Author: Jeff Gaydos.33 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Util
{
    class DebugUtil
    {
        public static void ShowHitbox(Rectangle hitbox, SpriteBatch spriteBatch, Game1 game)
        {
            //Stack overflow: https://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch
            if (hitbox.Width > 0)
            {
                Texture2D rect = new Texture2D(game.GraphicsDevice, hitbox.Width, hitbox.Height);

                Color[] data = new Color[hitbox.Width * hitbox.Height];
                for (int i = 0; i < data.Length; ++i) data[i] = new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.5f));
                rect.SetData(data);

                Vector2 coor = new Vector2(hitbox.X, hitbox.Y);
                //spriteBatch.Draw(rect, coor, Color.White);
                spriteBatch.Draw(rect, new Rectangle(coor.ToPoint(), hitbox.Size), new Rectangle(0, 0, hitbox.Width, hitbox.Height), Color.White, 0f, new Vector2(0.0f, 0.0f), SpriteEffects.None, SpriteLayerUtil.topLayer);
            }
        }
}
}
