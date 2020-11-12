using Game1.Controller;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameState
{
    class SkyrimState : IGameState
    {
        Video skyrim;
        VideoPlayer player;
        Game1 game;
        public SkyrimState(Game1 game)
        {
            this.game = game;
            skyrim = game.Content.Load<Video>("Skyrim");
            player = new VideoPlayer();
            player.Play(skyrim);
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            Texture2D videoTexture = null;

            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();
            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, new Rectangle(0, 0, resolutionManager.GetBaseResolution().X * resolutionManager.GetResolutionScale(), resolutionManager.GetBaseResolution().Y * resolutionManager.GetResolutionScale()), Color.White);
                spriteBatch.End();
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
