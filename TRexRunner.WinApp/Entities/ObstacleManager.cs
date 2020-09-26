using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TRexRunner.WinApp.Entities
{
    public class ObstacleManager : IGameEntity
    {
        private const float MIN_SPAWN_DISTANCE = 20;

        private const int MIN_OBSTACLE_DISTANCE = 10;
        private const int MAX_OBSTACLE_DISTANCE = 50;
        private const int OBSTABLE_DISTANCE_SPEED_TOLERANCE = 5;
        private const int LARGE_CACTUS_POS_Y = 80;
        private const int SMALL_CACTUS_POS_Y = 94;
        private const int OBSTACLE_DRAW_ORDER = 12;
        private const int OBSTACLE_DESPAWN_POS_X = -200;

        private double _lastSpawnScore = -1;
        private double _currentTargetDistance;

        private readonly EntityManager _entityManager;
        private readonly TRex _trex;
        private readonly ScoreBoard _scoreBoard;
        private readonly Random _random;

        private Texture2D _spriteSheet;

        public int DrawOrder => 0;
        public bool IsEnabled { get; set; }

        public bool CanSpawnObstacles => IsEnabled && _scoreBoard.Score >= MIN_SPAWN_DISTANCE;

        public ObstacleManager(EntityManager entityManager, TRex trex, ScoreBoard scoreBoard, Texture2D spriteSheet)
        {
            _entityManager = entityManager;
            _trex = trex;
            _scoreBoard = scoreBoard;
            _random = new Random();
            _spriteSheet = spriteSheet;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime)
        {
            if (!IsEnabled)
                return;

            if (CanSpawnObstacles && (_lastSpawnScore <= 0 || (_scoreBoard.Score - _lastSpawnScore >= _currentTargetDistance)))
            {
                _currentTargetDistance = _random.NextDouble() * (MAX_OBSTACLE_DISTANCE - MIN_OBSTACLE_DISTANCE) + MIN_OBSTACLE_DISTANCE;

                _currentTargetDistance += (_trex.Speed - TRex.START_SPEED) / (TRex.MAX_SPEED - TRex.START_SPEED) * OBSTABLE_DISTANCE_SPEED_TOLERANCE;

                _lastSpawnScore = _scoreBoard.Score;

                SpawnRandomObstacle();
            }

            foreach (Obstacle obstacle in _entityManager.GetEntitiesOfType<Obstacle>())
            {
                if (obstacle.Position.X < OBSTACLE_DESPAWN_POS_X)
                    _entityManager.RemoveEntity(obstacle);
            }
        }

        private void SpawnRandomObstacle()
        {
            Obstacle obstacle;

            CactusGroup.GroupSize randomGroupSize = (CactusGroup.GroupSize)_random.Next((int)CactusGroup.GroupSize.Small, (int)CactusGroup.GroupSize.Large + 1);

            bool isLarge = _random.NextDouble() > 0.5f;
            float posY = isLarge ? LARGE_CACTUS_POS_Y : SMALL_CACTUS_POS_Y;

            obstacle = new CactusGroup(_spriteSheet, isLarge, randomGroupSize, _trex, new Vector2(TRexRunnerGame.WINDOW_WIDTH, posY));
            obstacle.DrawOrder = OBSTACLE_DRAW_ORDER;

            _entityManager.AddEntity(obstacle);
        }
    }
}
