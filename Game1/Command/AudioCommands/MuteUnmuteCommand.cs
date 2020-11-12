/* Author: Hunter Figgs.3 */

using Game1.Audio;
using Game1.GameState;
using System.Diagnostics;

namespace Game1.Command
{
    class MuteUnmuteCommand : ICommand
    {
        private static bool muted;
        private static float lastVolume;
        private readonly Stopwatch stopWatch;
        private Game1 game;
        private const float cooldown = 250.0f;

        public MuteUnmuteCommand(Game1 game)
        {
            muted = false;
            lastVolume = AudioManager.GetVolumeMaster();

            this.game = game;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void Execute()
        {
            if (stopWatch.ElapsedMilliseconds >= cooldown)
            {
                if (muted)
                {
                    AudioManager.SetVolumeMaster(lastVolume);
                    muted = false;
                }
                else
                {
                    lastVolume = AudioManager.GetVolumeMaster();
                    AudioManager.SetVolumeMaster(0.0f);
                    muted = true;
                }
                stopWatch.Restart();
            }
        }
    }
}
