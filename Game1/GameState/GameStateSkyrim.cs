using Game1.Audio;
using Game1.Controller;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Game1.GameState
{
    class GameStateSkyrim : IGameState
    {
        Video skyrim;
        VideoPlayer player;
        Game1 game;
        List<IController> controllerList;

        public GameStateSkyrim(Game1 game)
        {
            this.game = game;
            const string SkyrimVideo = "Skyrim";
            skyrim = game.Content.Load<Video>(SkyrimVideo);
            player = new VideoPlayer();
            player.Volume = AudioManager.GetVolumeMaster();
            player.Play(skyrim);
            AudioManager.ResetAudioManager();

            controllerList = new List<IController>
            {
                new KeyboardQuitController(game),
                new GamepadQuitController(game, PlayerIndex.One)
            };
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            Texture2D videoTexture = null;

            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();
            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, new Rectangle(0, 0, (int)(resolutionManager.GetVirtualResolution().X * resolutionManager.GetResolutionScale()), (int)(resolutionManager.GetVirtualResolution().Y * resolutionManager.GetResolutionScale())), Color.White);
                spriteBatch.End();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (player.State == MediaState.Stopped)
            {
                game.SetState(new GameStateStart(game));
            }

            player.Volume = AudioManager.GetVolumeMaster();

            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
        }
    }
}
