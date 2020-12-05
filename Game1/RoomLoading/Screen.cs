using Game1.Collision_Handling;
using Game1.CollisionDetection;
using Game1.Player;
using Game1.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Game1.Util;

namespace Game1.RoomLoading
{
    public class Screen
    {
        public List<IPlayer> Players;
        private List<Rectangle> PlayerHitboxes;

        private readonly Vector2 playerPosition = new Vector2(100.0f, 88.0f);
        private readonly Vector2 nextPlayerOffset = new Vector2(20.0f, 0.0f);
        public AIPlayerController AIPlayerControl;
        public Dictionary<(char, int), Room> RoomsDict { get; set; }
        public Room CurrentRoom { get { return RoomsDict[CurrentRoomKey]; } private set { CurrentRoom = value; } }
        public (char, int) CurrentRoomKey { get; set; }

        private readonly Game1 game;
        private CollisionDetector detector;
        private CollisionHandler handler;
        public PortalManager PortalManager { get; private set; }
        private readonly List<Color> clockColor;
        private const char startingLetter = 'G';
        private const int startingNumber = 2;
        private const float clockDuration = 45f;
        private ColorIterator iterator;

        public Screen(Game1 game)
        {
            this.game = game;
            RoomsDict = new Dictionary<(char, int), Room>();
            Players = new List<IPlayer>();
            PlayerHitboxes = new List<Rectangle>();
            HandleGameMode();
            clockColor = new List<Color>();
            clockColor.Add(Color.Red);
            clockColor.Add(Color.LightBlue);
            clockColor.Add(Color.White);
            iterator = new ColorIterator(clockColor, clockDuration);
            PortalManager = new PortalManager(this);
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
            if (CurrentRoom.Clocked)
            {
                iterator.Update(gameTime);
            }

            CurrentRoom.Update(gameTime);

            for (int i = 0; i < Players.Count; i++) {
                Players[i].Update(gameTime);
            }

            if (game.Mode == 2)
            {
                AIPlayerControl.Update(gameTime);
            }

            handler.HandleCollisions(detector.GetCollisionList());

            PortalManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            CurrentRoom.Draw(spriteBatch, color);

           foreach(IPlayer player in Players)
            {
                if (!CurrentRoom.Clocked)
                    player.Draw(spriteBatch, Color.White);
                else
                    player.Draw(spriteBatch, iterator.GetColor(Color.White));
            }
        }

        public List<Rectangle> GetPlayerRectangle()
        {
            PlayerHitboxes.Clear();
            foreach(IPlayer p in Players)
            {
                PlayerHitboxes.Add(p.GetPlayerHitbox());
            }
            return PlayerHitboxes;
        }

        public void HandleGameMode()
        {
            Players.Clear();
            IPlayer player;
            IPlayer player2;
            switch (game.Mode)
            {
                case 0:
                    //singleplayer
                    player = new Player1(game, playerPosition);
                    Players.Add(player);
                    PlayerHitboxes.Add(player.GetPlayerHitbox());
                    break;
                case 1:
                    //multiplayer
                    player = new Player1(game, playerPosition);
                    Players.Add(player);
                    player2 = new Player2(game, playerPosition + nextPlayerOffset);
                    Players.Add(player2);
                    foreach(IPlayer p in Players)
                    {
                        PlayerHitboxes.Add(p.GetPlayerHitbox());                        
                    }
                    break;
                case 2:
                    //AI
                    player = new Player1(game, playerPosition);
                    Players.Add(player);
                    PlayerHitboxes.Add(player.GetPlayerHitbox());
                    //Add AI-based constructor here
                    player2 = new Player2(game, playerPosition + nextPlayerOffset);
                    Players.Add(player2);
                    AIPlayerControl = new AIPlayerController(player, player2, this);
                    break;
            }

            game.HUD = new HUDInterface(Players, game.Screen);
        }
    }
}
