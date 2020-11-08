using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    public interface IEnemy
    {
        void ReceiveDamage(float amount, Vector2 direction);

        void Update(GameTime gameTime, Rectangle drawingLimits5);

        void Draw(SpriteBatch spriteBatch, Color color);

        void EditPosition(Vector2 amount);

        bool ShouldRemove();
        
        void SetState(IEnemyState state);

        int StunnedTimer { get; set; } // ms (0 -> not stunned, Int.MaxValue -> perma stunned)

        List<Rectangle> GetHitboxes();
    }
}
