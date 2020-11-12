/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class VolumeUpCommand : ICommand
    {
        private const float volumeStep = 0.01f;
        private const float maxVolume = 1.0f;
        private readonly Stopwatch stopWatch;
        private Game1 game;
        private const float cooldown = 10.0f;

        public VolumeUpCommand(Game1 game)
        {
            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (AudioManager.GetVolumeMaster() < maxVolume - volumeStep)
                {
                    AudioManager.SetVolumeMaster(AudioManager.GetVolumeMaster() + volumeStep);
                }
                else
                {
                    AudioManager.SetVolumeMaster(maxVolume);
                }
                stopWatch.Restart();
            }
        }
    }
}
