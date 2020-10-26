using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game1.RoomLoading
{
    public class Screen
    {
        public IPlayer Player { get; set; }
        public List<IProjectile> ProjectileList { get; set; }

        private Game1 game;

        //private Dictionary<(char, int), Room> Rooms { get; set; }
        public LinkedList<Room> Rooms;
        public Room CurrentRoom { get; set; }
        public Screen(Game1 game, char x, int y)
        {
            this.game = game;
            this.Rooms = new LinkedList<Room>();
            this.ProjectileList = new List<IProjectile>();
            this.Player = new Player1(game, new Vector2(80, 80));
        }

        public void LoadAllRooms()
        {
            foreach (string file in Directory.EnumerateFiles(game.Content.RootDirectory + "/RoomXML", "*.xml"))
            {
                //var identifer = Regex.Match(file, @"\w{2}(?=\.\w+$)");
                //String identiferStr = identifer.Value;
                //Rooms.Add((identiferStr[0], (int)char.GetNumericValue(identiferStr[1])), new Room(game, file));
                Rooms.AddLast(new Room(game, file));
            }
            //this.currentRoom = Rooms[('F',2)];
            this.CurrentRoom = Rooms.First();
        }
        public void Update(GameTime gameTime)
        {

            LinkedList<IProjectile> projectilesToRemove = new LinkedList<IProjectile>();

            foreach (IProjectile projectile in ProjectileList)
            {
                if (projectile.Update(gameTime))
                {
                    projectilesToRemove.AddFirst(projectile);
                }
            }

            foreach (IProjectile projectile in projectilesToRemove)
            {
                ProjectileList.Remove(projectile);
            }

            CurrentRoom.Update(gameTime);

            Player.Update(gameTime);
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            CurrentRoom.Draw(spriteBatch);

            Player.Draw(spriteBatch, Color.White);

            foreach (IProjectile projectile in ProjectileList)
            {
                projectile.Draw(spriteBatch, Color.White);
            }
        }

        public void SpawnProjectile(IProjectile projectile)
        {
            ProjectileList.Add(projectile);
        }

        public void SpawnItem(IItem item)
        {
            CurrentRoom.ItemList.Add(item);
        }

        public Rectangle GetPlayerRectangle()
        {
            return Player.GetLocation();
        }
    }
}
