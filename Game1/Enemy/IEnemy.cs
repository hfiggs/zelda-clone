using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    public interface IEnemy
    {
        void ReceiveDamage(float amount, Vector2 direction);

        void Update(GameTime gameTime, Rectangle drawingLimits5);

        void Draw(SpriteBatch spriteBatch, Color color);

        void editPosition(Vector2 amount);

        bool shouldRemove();
        
        void SetState(IEnemyState state);

        Rectangle GetHitbox();
    }
}
