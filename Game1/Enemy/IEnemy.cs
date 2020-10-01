using Microsoft.Xna.Framework;


namespace Game1.Enemy
{
    public interface IEnemy
    {
        void Attack();

        void ReceiveDamage();

        void Update(GameTime gameTime, Rectangle drawingLimits5);

        void Draw();
    }
}
