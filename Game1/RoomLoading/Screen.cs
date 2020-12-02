using Game1.Collision_Handling;
using Game1.CollisionDetection;
using Game1.Player;
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
        private readonly Vector2 playerPosition = new Vector2(100, 88);

        public Dictionary<(char, int), Room> RoomsDict { get; set; }
        public Room CurrentRoom { get { return RoomsDict[CurrentRoomKey]; } private set { CurrentRoom = value; } }
        public (char, int) CurrentRoomKey { get; set; }

        private readonly Game1 game;
        private CollisionDetector detector;
        private CollisionHandler handler;

        private const char startingLetter = 'G';
        private const int startingNumber = 2;
        
        public Screen(Game1 game)
        {
            this.game = game;
            RoomsDict = new Dictionary<(char, int), Room>();
            Player = new Player1(game, playerPosition);
        }

        public void LoadAllRooms(int difficulty)
        {
            const string roomXMLDirectory = "/RoomXML", xmlFileTag = "*.xml";
            foreach (string file in Directory.EnumerateFiles(game.Content.RootDirectory + roomXMLDirectory, xmlFileTag))
            {
                var identifer = Regex.Match(file, @"\w{2}(?=\.\w+$)");
                String identiferStr = identifer.Value;
                Room room = new Room(game, file, difficulty);
                RoomsDict.Add((identiferStr[0], (int)char.GetNumericValue(identiferStr[1])), room);
            }

            CurrentRoomKey = (startingLetter, startingNumber);

            detector = new CollisionDetector(this);
            handler = new CollisionHandler(game);
        }

        public void Update(GameTime gameTime)
        {
            CurrentRoom.Update(gameTime);

            Player.Update(gameTime);

            handler.HandleCollisions(detector.GetCollisionList());
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            CurrentRoom.Draw(spriteBatch, color);

            Player.Draw(spriteBatch, color);
        }

        public Rectangle GetPlayerRectangle()
        {
            return Player.GetPlayerHitbox();
        }
    }
}
