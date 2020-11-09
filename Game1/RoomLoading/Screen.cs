using Game1.Collision_Handling;
using Game1.CollisionDetection;
using Game1.Enemy;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Game1.RoomLoading
{
    public class Screen
    {
        public IPlayer Player { get; set; }
        public List<IProjectile> ProjectileList { get; set; }

        private Game1 game;

        public Dictionary<(char, int), Room> RoomsDict { get; set; }
        public LinkedList<Room> RoomsList;
        public Room CurrentRoom { get; set; }
        private CollisionDetector detector;
        
        public Screen(Game1 game, char x, int y)
        {
            this.game = game;
            RoomsList = new LinkedList<Room>();
            RoomsDict = new Dictionary<(char, int), Room>();
            ProjectileList = new List<IProjectile>();
            Player = new Player1(game, new Vector2(80, 80));
        }

        public void LoadAllRooms()
        {
            foreach (string file in Directory.EnumerateFiles(game.Content.RootDirectory + "/RoomXML", "*.xml"))
            {
                var identifer = Regex.Match(file, @"\w{2}(?=\.\w+$)");
                String identiferStr = identifer.Value;
                Room room = new Room(game, file);
                RoomsDict.Add((identiferStr[0], (int)char.GetNumericValue(identiferStr[1])), room);
                RoomsList.AddLast(room);
            }

            CurrentRoom = RoomsDict[('F',2)];
            ProjectileList = new List<IProjectile>();
            detector = new CollisionDetector(this);
        }

        public void Update(GameTime gameTime)
        {

            foreach (IProjectile projectile in ProjectileList)
            {
               projectile.Update(gameTime);
            }

            ProjectileList.RemoveAll(p => p.ShouldDelete());

            CurrentRoom.Update(gameTime);

            Player.Update(gameTime);

            CollisionHandler.HandleCollisions(detector.GetCollisionList());
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
