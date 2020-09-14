using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Sprite;

namespace Game1.Command
{
    class SpriteNotAnimatedNotMovingCommand : ICommand
    {
        private Game1 game;

        public SpriteNotAnimatedNotMovingCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Sprite = new NotAnimatedNotMovingSprite(game.Content.Load<Texture2D>("Images/guyRun"), 3, 3, new Vector2(336, 200));
        }
    }
}
