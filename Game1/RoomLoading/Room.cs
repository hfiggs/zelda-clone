using Game1.AudioManagement;
using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Game1.Projectile;
using Game1.Item.ItemDropper;
using Game1.RoomLoading.Puzzle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Game1.RoomLoading
{
    public class Room
    {
        public List<IItem> ItemList { get; set; }
        public List<IProjectile> ProjectileList { get; set; }
        public List<IEnvironment> NonInteractEnviornment { get; set; }
        public List<IEnvironment> InteractEnviornment { get; set; }
        public List<IEnemy> EnemyList { get; set; }
        public List<IEnemy> DecoratedEnemyList { get; set; }
        public List<IEnemy> UnDecoratedEnemyList { get; set; }

        private readonly IPuzzle puzzle;
        private readonly ItemDropper itemDrops;

        private readonly List<AmbientSound> soundList;

        private const int roomWidth = 256;
        private const int roomHeight = 176;

        public Room(Game1 game, string file)
        {
            RoomParser parser = new RoomParser(game, this, file);

            ItemList = new List<IItem>(parser.GetItems());

            ProjectileList = new List<IProjectile>();
            
            NonInteractEnviornment = new List<IEnvironment>(parser.GetNonInteractableEnvinornment());
            InteractEnviornment = new List<IEnvironment>(parser.GetInteractableEnvinornment());
            
            EnemyList = new List<IEnemy>(parser.GetEnemies());
            DecoratedEnemyList = new List<IEnemy>();
            UnDecoratedEnemyList = new List<IEnemy>();

            soundList = parser.GetAmbienceNode();

            itemDrops = new ItemDropper(game.Screen);
            puzzle = parser.GetPuzzle();
        }

        public void Update(GameTime gameTime)
        {

            ItemList.ForEach(item => item.Update(gameTime));

            CheckClocksAndStunEnemies();

            ItemList.RemoveAll(p => p.ShouldDelete);

            ProjectileList.ForEach(proj => proj.Update(gameTime));

            ProjectileList.RemoveAll(p => p.ShouldDelete());

            EnemyList.ForEach(enemy => enemy.Update(gameTime, new Rectangle(0, 0, roomWidth, roomHeight)));

            foreach (IEnemy decoratedEnemy in DecoratedEnemyList)
            {
                EnemyList.Remove(((EnemyDamageDecorator)decoratedEnemy).original);
                EnemyList.Add(decoratedEnemy);
            }
            DecoratedEnemyList.Clear();

            foreach (IEnemy unDecoratedEnemy in UnDecoratedEnemyList)
            {
                EnemyList.Remove((EnemyDamageDecorator)unDecoratedEnemy);
                EnemyList.Add(((EnemyDamageDecorator)unDecoratedEnemy).original);
            }
            UnDecoratedEnemyList.Clear();

            itemDrops.SpawnDrops(EnemyList.Where(p => p.ShouldRemove()).ToList());

            EnemyList.RemoveAll(p => p.ShouldRemove());

            InteractEnviornment.ForEach(env => env.Update(gameTime));

            NonInteractEnviornment.ForEach(env => env.Update(gameTime));

            puzzle?.Check(gameTime, this);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            NonInteractEnviornment.ForEach(env => env.Draw(spriteBatch, color));

            InteractEnviornment.ForEach(env => env.Draw(spriteBatch, color));

            EnemyList.ForEach(enemy => enemy.Draw(spriteBatch, color));

            ItemList.ForEach(item => item.Draw(spriteBatch, color));

            ProjectileList.ForEach(proj => proj.Draw(spriteBatch, color));
        }

        public void SpawnProjectile(IProjectile projectile)
        {
            ProjectileList.Add(projectile);
        }

        public void SpawnItem(IItem item)
        {
            ItemList.Add(item);
        }

        private void CheckClocksAndStunEnemies()
        {
            foreach(IItem item in ItemList)
            {
                if(item.GetType() == typeof(Clock) && item.ShouldDelete)
                {
                    foreach(IEnemy enemy in EnemyList)
                    {
                        enemy.StunnedTimer = int.MaxValue;
                    }
                    break;
                }
            }
        }

        public void StopRoomAmbience()
        {
            soundList.ForEach(sound => sound.Stop());
        }

        public void PlayRoomAmbience()
        {
            soundList.ForEach(sound => sound.Play());
        }
    }
}
