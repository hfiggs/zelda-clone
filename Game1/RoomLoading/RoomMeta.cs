using Game1.Audio;
using System.Collections.Generic;

namespace Game1.RoomLoading
{
    public class RoomMeta
    {
        private readonly List<AmbientSound> ambienceList;
        private readonly AmbientSound music;
        private float ambienceVolume = 1.0f;
        private const float musicVolume = 1.0f;

        public bool IsLit { get; private set; }

        public RoomMeta(RoomParser roomParser)
        {
            ambienceList = roomParser.GetAmbienceNode();
            music = roomParser.GetMusicNode(musicVolume);

            IsLit = roomParser.IsLit();
        }

        public void StopRoomAmbience()
        {
            ambienceList.ForEach(sound => sound.Stop());
        }

        public void PlayRoomAmbience()
        {
            ambienceList.ForEach(sound => sound.Play(ambienceVolume));
        }

        public void SetAmbienceVolume(float vol)
        {
            ambienceVolume = vol;
        }

        public void PlayMusic(float delay = 0.0f)
        {
            music.Play(musicVolume, delay);
        }
    }
}
