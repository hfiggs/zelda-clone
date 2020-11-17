using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class EnemySpriteFactory
    {
        private Texture2D enemySpritesheet;
        private Texture2D spikeTrapSpritesheet;
        private Texture2D oldManSpritesheet;
        private Texture2D merchantSpritesheet;
        private Texture2D batSpritesheet;
        private Texture2D snakeSpriteSheet;
        private Texture2D dodongoSpriteSheet;
        private Texture2D aquamentusSpriteSheet;
        private Texture2D enemySpawnSpriteSheet;
        private Texture2D enemyDeathSpriteSheet;
        private const string enemySpriteFilePath = "images/Enemy/enemies", spikeTrapSpriteFilePath = "images/Enemy/spiketrap", oldManSpriteFilePath = "images/Enemy/oldman", merchantSpriteFilePath = "images/Enemy/Merchant", enemyDeathSpriteFilePath = "images/Enemy/enemydeath";
        private const string batSpriteFilePath = "images/Enemy/bat", snakeSpriteFilePath = "images/Enemy/snake", dodongoSpriteFilePath = "images/Enemy/dodongo", aquamentusSpriteFilePath = "images/Enemy/Aquamentus", enemySpawnSpriteFilePath = "images/Enemy/enemyspawn";

        private const int SpikeTrapAndOldManColumn = 0, SpikeTrapAndOldManRow = 0, SpikeTrapAndOldManMaxColumns = 1, SpikeTrapAndOldManMaxRows = 1, SpikeTrapAndOldManTotalFrames = 1;
        private const int SkeletonColumn = 14, SkeletonRow = 4, SkeletonMaxColumns = 15, SkeletonMaxRows = 8, SkeletonTotalFrames = 2;
        private const int MerchantColumn = 0, MerchantRow = 0, MerchantMaxColumns = 1, MerchantMaxRows = 1, MerchantTotalFrames = 0;
        private const int JellyColumn = 14, JellyRow = 6, JellyMaxColumns = 15, JellyMaxRows = 8, JellyTotalFrames = 2;
        private const int BatColumn = 0, BatRow = 0, BatMaxColumns = 1, BatMaxRows = 2, BatTotalFrames = 2;
        private const int HandColumn = 8, HandRow = 0, HandMaxColumns = 15, HandMaxRows = 8, HandTotalFrames = 2;
        private const int GoriyaDownColumn = 0, GoriyaDownRow = 2, GoriyaLeftColumn = 1, GoriyaLeftRow = 2, GoriyaRightColumn = 3, GoriyaRightRow = 2, GoriyaUpColumn = 2, GoriyaUpRow = 2, GoriyaMaxColumns = 15, GoriyaMaxRows = 8, GoriyaTotalFrames = 2;
        private const int SnakeLeftColumn = 1, SnakeRow = 0, SnakeRightColumn = 0, SnakeMaxColumns = 2, SnakeMaxRows = 2, SnakeTotalFrames = 2;
        private const int DodongoLeftColumn = 2, DodongoLeftRow = 0, DodongoLeftDeadColumn = 2, DodongoLeftDeadRow = 2, DodongoRightColumn = 3, DodongoRightRow = 0, DodongoRightDeadColumn = 3, DodongoRightDeadRow = 2, DodongoUpColumn = 1;
        private const int DodongoUpRow = 0, DodongoUpDeadColumn = 1, DodongoUpDeadRow = 2, DodongoDownColumn = 0, DodongoDownRow = 0, DodongoDownDeadColumn = 0, DodongoDownDeadRow = 2, DodongoMaxColumns = 4, DodongoMaxRows = 3, DodongoTotalFrames = 2, DodongoDeadTotalFrames = 1;
        private const int AquamentusColumn = 1, AquamentusRow = 0, AquamentusMaxColumns = 2, AquamentusMaxRows = 2, AquamentusTotalFrames = 2;
        private const int AttackAquamentusColumn = 0, AttackAquamentusRow = 0, AttackAquamentusMaxColumns = 2, AttackAquamentusMaxRows = 2, AttackAquamentusTotalFrames = 2;
        private const int EnemySpawningColumn = 0, EnemySpawningRow = 0, EnemySpawningMaxColumns = 1, EnemySpawningMaxRows = 4, EnemySpawningTotalFrames = 4;
        private const int EnemyDyingColumn = 0, EnemyDyingRow = 0, EnemyDyingMaxColumns = 1, EnemyDyingMaxRows = 6, EnemyDyingTotalFrames = 6;


        private static EnemySpriteFactory instance = new EnemySpriteFactory();


        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpritesheet = content.Load<Texture2D>(enemySpriteFilePath);
            spikeTrapSpritesheet = content.Load<Texture2D>(spikeTrapSpriteFilePath);
            oldManSpritesheet = content.Load<Texture2D>(oldManSpriteFilePath);
            merchantSpritesheet = content.Load<Texture2D>(merchantSpriteFilePath);
            batSpritesheet = content.Load<Texture2D>(batSpriteFilePath);
            snakeSpriteSheet = content.Load<Texture2D>(snakeSpriteFilePath);
            dodongoSpriteSheet = content.Load<Texture2D>(dodongoSpriteFilePath);
            aquamentusSpriteSheet = content.Load<Texture2D>(aquamentusSpriteFilePath);
            enemySpawnSpriteSheet = content.Load<Texture2D>(enemySpawnSpriteFilePath);
            enemyDeathSpriteSheet = content.Load<Texture2D>(enemyDeathSpriteFilePath);
        }

        public ISprite CreateSkeletonSprite()
        {
            return new EnemySprite(enemySpritesheet, SkeletonColumn, SkeletonRow, SkeletonMaxColumns, SkeletonMaxRows, SkeletonTotalFrames);
        }

        public ISprite CreateSpikeTrapSprite()
        {
            return new EnemySprite(spikeTrapSpritesheet, SpikeTrapAndOldManColumn, SpikeTrapAndOldManRow, SpikeTrapAndOldManMaxColumns, SpikeTrapAndOldManMaxRows, SpikeTrapAndOldManTotalFrames);
        }

        public ISprite CreateOldManSprite()
        {
            return new EnemySprite(oldManSpritesheet, SpikeTrapAndOldManColumn, SpikeTrapAndOldManRow, SpikeTrapAndOldManMaxColumns, SpikeTrapAndOldManMaxRows, SpikeTrapAndOldManTotalFrames);
        }

        public ISprite CreateMerchantSprite()
        {
            return new EnemySprite(merchantSpritesheet, MerchantColumn, MerchantRow, MerchantMaxColumns, MerchantMaxRows, MerchantTotalFrames);
        }

        public ISprite CreateJellySprite()
        {
            return new EnemySprite(enemySpritesheet, JellyColumn, JellyRow, JellyMaxColumns, JellyMaxRows, JellyTotalFrames);
        }
        
        
        public ISprite CreateBatSprite()
        {
            return new EnemySprite(batSpritesheet, BatColumn, BatRow, BatMaxColumns, BatMaxRows, BatTotalFrames);
        }

        public ISprite CreateHandSprite()
        {
            return new EnemySprite(enemySpritesheet, HandColumn, HandRow, HandMaxColumns, HandMaxRows, HandTotalFrames);
        }

        public ISprite CreateGoriyaDownSprite()
        {
            return new EnemySprite(enemySpritesheet, GoriyaDownColumn, GoriyaDownRow, GoriyaMaxColumns, GoriyaMaxRows, GoriyaTotalFrames);
        }

        public ISprite CreateGoriyaLeftSprite()
        {
            return new EnemySprite(enemySpritesheet, GoriyaLeftColumn, GoriyaLeftRow, GoriyaMaxColumns, GoriyaMaxRows, GoriyaTotalFrames);
        }

        public ISprite CreateGoriyaRightSprite()
        {
            return new EnemySprite(enemySpritesheet, GoriyaRightColumn, GoriyaRightRow, GoriyaMaxColumns, GoriyaMaxRows, GoriyaTotalFrames);
        }

        public ISprite CreateGoriyaUpSprite()
        {
            return new EnemySprite(enemySpritesheet, GoriyaUpColumn, GoriyaUpRow, GoriyaMaxColumns, GoriyaMaxRows, GoriyaTotalFrames);
        }

        public ISprite CreateSnakeLeftSprite()
        {
            return new EnemySprite(snakeSpriteSheet, SnakeLeftColumn, SnakeRow, SnakeMaxColumns, SnakeMaxRows, SnakeTotalFrames);
        }

        public ISprite CreateSnakeRightSprite()
        {
            return new EnemySprite(snakeSpriteSheet, SnakeRightColumn, SnakeRow, SnakeMaxColumns, SnakeMaxRows, SnakeTotalFrames);
        }
        
        public ISprite CreateDodongoLeftSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoLeftColumn, DodongoLeftRow, DodongoMaxColumns, DodongoMaxRows, DodongoTotalFrames);
        }

        public ISprite CreateDodongoLeftDeadSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoLeftDeadColumn, DodongoLeftDeadRow, DodongoMaxColumns, DodongoMaxRows, DodongoDeadTotalFrames);
        }

        public ISprite CreateDodongoRightSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoRightColumn, DodongoRightRow, DodongoMaxColumns, DodongoMaxRows, DodongoTotalFrames);
        }

        public ISprite CreateDodongoRightDeadSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoRightDeadColumn, DodongoRightDeadRow, DodongoMaxColumns, DodongoMaxRows, DodongoDeadTotalFrames);
        }

        public ISprite CreateDodongoUpSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoUpColumn, DodongoUpRow, DodongoMaxColumns, DodongoMaxRows, DodongoTotalFrames);
        }

        public ISprite CreateDodongoUpDeadSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoUpDeadColumn, DodongoUpDeadRow, DodongoMaxColumns, DodongoMaxRows, DodongoDeadTotalFrames);
        }

        public ISprite CreateDodongoDownSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoDownColumn, DodongoDownRow, DodongoMaxColumns, DodongoMaxRows, DodongoTotalFrames);
        }

        public ISprite CreateDodongoDownDeadSprite()
        {
            return new EnemySprite(dodongoSpriteSheet, DodongoDownDeadColumn, DodongoDownDeadRow, DodongoMaxColumns, DodongoMaxRows, DodongoDeadTotalFrames);
        }
        
        public ISprite CreateAquamentusSprite()
        {
            return new EnemySprite(aquamentusSpriteSheet, AquamentusColumn, AquamentusRow, AquamentusMaxColumns, AquamentusMaxRows, AquamentusTotalFrames);
        }

        public ISprite CreateAttackAquamentusSprite()
        {
            return new EnemySprite(aquamentusSpriteSheet, AttackAquamentusColumn, AttackAquamentusRow, AttackAquamentusMaxColumns, AttackAquamentusMaxRows, AttackAquamentusTotalFrames);
        }

        public ISprite CreateEnemySpawningSprite()
        {
            return new EnemySprite(enemySpawnSpriteSheet, EnemySpawningColumn, EnemySpawningRow, EnemySpawningMaxColumns, EnemySpawningMaxRows, EnemySpawningTotalFrames);
        }
        public ISprite CreateEnemyDyingSprite()
        {
            return new EnemySprite(enemyDeathSpriteSheet, EnemyDyingColumn, EnemyDyingRow, EnemyDyingMaxColumns, EnemyDyingMaxRows, EnemyDyingTotalFrames);
        }
    }
}

