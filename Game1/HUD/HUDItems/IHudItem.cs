using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.HUD
{
    interface IHudItem
    {
        Vector2 location { get; set; }

        void Update(GameTime time);

        void Draw(SpriteBatch spriteBatch, Vector2 movement, Color color);

        IHudItem copyOf();

        Rectangle selectionRectangle { get; }
    }
}
