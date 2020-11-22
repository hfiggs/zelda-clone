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
        public List<IPlayer> Players;
        public IPlayer Player { get; set; }
        public IPlayer Player2 { get; set; }
        private readonly Vector2 playerPosition = new Vector2(100, 88);
        private readonly Vector2 player2Position = new Vector2(120, 88);

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
            Player2 = new Player2(game, player2Position);
            Players = new List<IPlayer>();
            Players.Add(Player);
            Players.Add(Player2);
        }

        public void LoadAllRooms()
        {
            const string roomXMLDirectory = "/RoomXML", xmlFileTag = "*.xml";
            foreach (string file in Directory.EnumerateFiles(game.Content.RootDirectory + roomXMLDirectory, xmlFileTag))
            {
                var identifer = Regex.Match(file, @"\w{2}(?=\.\w+$)");
                String identiferStr = identifer.Value;
                Room room = new Room(game, file);
                RoomsDict.Add((identiferStr[0], (int)char.GetNumericValue(identiferStr[1])), room);
            }

            CurrentRoomKey = (startingLetter, startingNumber);

            detector = new CollisionDetector(this);
            handler = new CollisionHandler(game);
        }

        public void Update(GameTime gameTime)
        {
            CurrentRoom.Update(gameTime);

            foreach(IPlayer player in Players)
            {
                player.Update(gameTime);
            }
            
            handler.HandleCollisions(detector.GetCollisionList());
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            CurrentRoom.Draw(spriteBatch, color);

           foreach(IPlayer player in Players)
            {
                player.Draw(spriteBatch,Color.White);
            }
        }

        public Rectangle GetPlayerRectangle()
        {
            return Player.GetPlayerHitbox();
        }
    }
}
