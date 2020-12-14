using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.RoomLoading.Puzzle
{
    public interface IPuzzle
    {
        void Update(GameTime gameTime, Room room);
    }
}
