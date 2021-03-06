﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class Aquamentus : IEnemy
    {
        public ISprite Sprite { get; private set; }
        public int StunnedTimer { get; set; } = 0;

        private float health;
        private IEnemyState state;
        private Vector2 position;
        private Game1 game;

        public Aquamentus(Game1 game, Vector2 position) {
            this.game = game;
            this.position = position;
            state = new EnemyStateSpawning(this.position, this, new AquamentusWalkLeft(game, this, position));
            health = 12f;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, new Vector2(0,0), game);
            game.Screen.CurrentRoom.DecoratedEnemyList.Add(decorator);

            if (health <= 0)
            {
                state = new EnemyStateDying(this, state.GetPosition());
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);

            StunnedTimer -= (StunnedTimer == int.MaxValue) ? 0: (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            StunnedTimer = Math.Max(0, StunnedTimer);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public void EditPosition(Vector2 amount)
        {
            if (StunnedTimer <= 0)
            {
                state.editPosition(amount);
            }
        }

        public Vector2 GetPosition()
        {
            return state.GetPosition();
        }
        public bool ShouldRemove()
        {
            if (health <= 0)
            {
                game.Screen.CurrentRoom.RoomMeta.StopRoomAmbience();
                game.Screen.CurrentRoom.RoomMeta.SetAmbienceVolume(0.0f);
                RoomLoading.Room roomBelow;

                const char handRoomChar = 'C';
                const int handRoomInt = 4;
                game.Screen.RoomsDict.TryGetValue((handRoomChar, handRoomInt), out roomBelow);

                roomBelow.RoomMeta.SetAmbienceVolume(0.0f);
            }
            if(state.GetType() == typeof(EnemyStateDying))
            {
                EnemyStateDying temp = (EnemyStateDying)state;
                return temp.dead;
            }
            else
            {
                return false;
            }
        }

        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }
    }
}
