using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRexRunner.WinApp.Graphics;

namespace TRexRunner.WinApp.Entities
{
    public class GroundManager : IGameEntity
    {
        private const int SPRITE_WIDTH = 600;
        private const int SPRITE_HEIGHT = 14;

        private const int SPRITE_POS_X = 2;
        private const int SPRITE_POS_Y = 54;

        private const float GORUND_TILE_POS_Y = 119;

        private Texture2D _spriteSheet;
        private readonly List<GroundTile> _groundTiles;
        private EntityManager _entityManager;
        private Sprite _regularSprite;
        private Sprite _bumpySprite;

        private TRex _tRex;

        private Random _random;

        public int DrawOrder { get; set; }

        public GroundManager(Texture2D spriteSheet, EntityManager entityManager, TRex tRex)
        {
            _spriteSheet = spriteSheet;
            _groundTiles = new List<GroundTile>();
            _entityManager = entityManager;
            _regularSprite = new Sprite(spriteSheet, SPRITE_POS_X, SPRITE_POS_Y, SPRITE_WIDTH, SPRITE_HEIGHT);
            _bumpySprite = new Sprite(spriteSheet, SPRITE_POS_X + SPRITE_WIDTH, SPRITE_POS_Y, SPRITE_WIDTH, SPRITE_HEIGHT);
            _tRex = tRex;
            _random = new Random();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (_groundTiles.Any())
            {
                float maxPosX = _groundTiles.Max(g => g.PositionX);

                if (maxPosX < 0)
                    SpawnTile(maxPosX);
            }

            List<GroundTile> tilesToRemove = new List<GroundTile>();

            foreach (GroundTile groundTile in _groundTiles)
            {
                groundTile.PositionX -= _tRex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (groundTile.PositionX < -SPRITE_WIDTH)
                {
                    _entityManager.RemoveEntity(groundTile);
                    tilesToRemove.Add(groundTile);
                }
            }

            foreach (GroundTile ground in tilesToRemove)
            {
                _groundTiles.Remove(ground);
            }
        }

        public void Initialize()
        {
            _groundTiles.Clear();

            GroundTile groundTile = CreateRegularTile(0);
            _groundTiles.Add(groundTile);

            _entityManager.AddEntity(groundTile);
        }

        private GroundTile CreateRegularTile(float positionX)
        {
            GroundTile groundTile = new GroundTile(positionX, GORUND_TILE_POS_Y, _regularSprite);
            return groundTile;
        }

        private GroundTile CreateBumpyTile(float positionX)
        {
            GroundTile groundTile = new GroundTile(positionX, GORUND_TILE_POS_Y, _bumpySprite);
            return groundTile;
        }

        private void SpawnTile(float maxPosX)
        {
            double randomNumber = _random.NextDouble();
            GroundTile groundTile;
            float posX = maxPosX + SPRITE_WIDTH;

            if (randomNumber > 0.5)
            {
                groundTile = CreateBumpyTile(posX);
            }
            else
            {
                groundTile = CreateRegularTile(posX);
            }

            _entityManager.AddEntity(groundTile);
            _groundTiles.Add(groundTile);
        }
    }
}
