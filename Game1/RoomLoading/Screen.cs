using Game1.Collision_Handling;
using Game1.CollisionDetection;
using Game1.Enemy;
using Game1.Environment;
using Game1.HUD;
using Game1.Item;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        //DELETEME DELETE ME TEMP HUD operations (lock mouse and hud position.)
        private const int yPosition = -136;
        public Vector2 HudPosition = new Vector2(0,yPosition);
        public bool lockMouse = true;

        private Game1 game;

        public Dictionary<(char, int), Room> RoomsDict { get; set; }
        public LinkedList<Room> RoomsList;
        public Room CurrentRoom { get; set; }
        private CollisionDetector detector;
        private HUDInterface HUD;
        public Screen(Game1 game, char x, int y)
        {
            this.game = game;
            this.RoomsList = new LinkedList<Room>();
            this.RoomsDict = new Dictionary<(char, int), Room>();
            this.ProjectileList = new List<IProjectile>();
            const int playerStartingXAndYPosition = 80;
            this.Player = new Player1(game, new Vector2(playerStartingXAndYPosition, playerStartingXAndYPosition));
            this.HUD = new HUDInterface(Player.PlayerInventory, this);
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
            const char startingLetter = 'F';
            const int startingNumber = 2;
            this.CurrentRoom = RoomsDict[(startingLetter,startingNumber)];
            this.ProjectileList = new List<IProjectile>();
            detector = new CollisionDetector(this);

        }
        public void Update(GameTime gameTime)
        {

            foreach (IProjectile projectile in ProjectileList)
            {
               projectile.Update(gameTime);
            }

            ProjectileList.RemoveAll(p => p.ShouldDelete());
            
            //DELETEME DELETE ME
           // HUD.Update(gameTime);
           // if(lockMouse)
          //  {
           //     Mouse.SetPosition(150, 150);
           // }


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
          //  HUD.Draw(spriteBatch,HudPosition, Color.White);
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
