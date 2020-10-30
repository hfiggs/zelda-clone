using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class AudioManager
    {
        private static Dictionary<string, Song> songMap = new Dictionary<string, Song>();
        public static void LoadContent(ContentManager content)
        {
            songMap.Add("dungeon", content.Load<Song>("audio/music/dungeon"));
        }

        public static void PlayMusic(string music)
        {
            Song toPlay;
            if (songMap.TryGetValue(music, out toPlay))
            {
                MediaPlayer.Play(toPlay);
            }
            else
            {
                throw new NotImplementedException(music + "is not a supported music name.");
            }
        }

        public static void PlaySound(string sound)
        {

        }
    }
}
