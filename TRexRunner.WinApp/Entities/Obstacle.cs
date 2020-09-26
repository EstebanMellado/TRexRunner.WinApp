﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRexRunner.WinApp.Entities
{
    public abstract class Obstacle : IGameEntity, ICollidable
    {
        private TRex _trex;

        public abstract Rectangle CollisionBox { get; }

        public int DrawOrder { get; set; }

        public Vector2 Position { get; private set; }

        protected Obstacle(TRex trex, Vector2 position)
        {
            _trex = trex;
            Position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public void Update(GameTime gameTime)
        {
            float posX = Position.X - _trex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position = new Vector2(posX, Position.Y);

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            Rectangle obstacleCollisionBox = CollisionBox;
            Rectangle trexCollisionBox = _trex.CollisionBox;

            if (obstacleCollisionBox.Intersects(trexCollisionBox))
            {
                _trex.Die();
            }
        }
    }
}
