using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Sprite;

namespace Game1.Command
{
    class SpriteAnimatedNotMovingCommand : ICommand
    {
        private Game1 game;

        public SpriteAnimatedNotMovingCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Sprite = new AnimatedNotMovingSprite(game.Content.Load<Texture2D>("Images/guyRun"), 3, 3, 8, new Vector2(336, 200));
        }
    }
}
