/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class VolumeDownCommand : ICommand
    {
        private const float volumeStep = 0.01f;
        private const float minVolume = 0.0f;
        private readonly Stopwatch stopWatch;
        private Game1 game;
        private const float cooldown = 10.0f;

        public VolumeDownCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (AudioManager.GetVolumeMaster() > volumeStep)
                {
                    AudioManager.SetVolumeMaster(AudioManager.GetVolumeMaster() - volumeStep);
                }
                else
                {
                    AudioManager.SetVolumeMaster(minVolume);
                }
                stopWatch.Restart();
            }
        }
    }
}
