using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TRexRunner.WinApp.Entities;
using TRexRunner.WinApp.System;

namespace TRexRunner.WinApp
{
    public class TRexRunnerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const string ASSET_NAME_SPRITESHEET = "trexSpriteSheet";
        private const string ASSET_NAME_SFX_HIT = "hit";
        private const string ASSET_NAME_SFX_SCORE_REACHED = "scoreReached";
        private const string ASSET_NAME_SFX_BUTTON_PRESS = "buttonPress";

        public const int WINDOW_WIDTH = 600;
        public const int WINDOW_HEIGHT = 150;

        public const int TREX_START_POS_X = 1;
        public const int TREX_START_POS_Y = WINDOW_HEIGHT - 16;
        private const float FADE_IN_ANIMATION_SPEED = 820f;
        private const int SCORE_BOARD_POS_X = WINDOW_WIDTH - 130;
        private const int SCORE_BOARD_POS_y = 10;

        private Texture2D _spriteSheetTexture;
        private Texture2D _fadeInTexture;

        private float _fadeInTexturePosX;

        private SoundEffect _sfxHit;
        private SoundEffect _sfxButtonPress;
        private SoundEffect _sfxScoredReached;

        private TRex _tRex;
        private InputController _inputController;
        private EntityManager _entityManager;
        private GroundManager _groundManager;
        private ScoreBoard _scoreBoard;

        private KeyboardState _previousKeyboardState;

        public GameState State { get; private set; }

        public TRexRunnerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();
            State = GameState.Initial;
            _fadeInTexturePosX = TRex.GetWidth();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _sfxHit = Content.Load<SoundEffect>(ASSET_NAME_SFX_HIT);
            _sfxButtonPress = Content.Load<SoundEffect>(ASSET_NAME_SFX_BUTTON_PRESS);
            _sfxScoredReached = Content.Load<SoundEffect>(ASSET_NAME_SFX_SCORE_REACHED);

            _spriteSheetTexture = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);
            _fadeInTexture = new Texture2D(GraphicsDevice, 1, 1);
            _fadeInTexture.SetData(new Color[] { Color.White });

            _tRex = new TRex(_spriteSheetTexture, new Vector2(TREX_START_POS_X, TREX_START_POS_Y - TRex.GetHeight()), _sfxButtonPress);
            _tRex.DrawOrder = 10;
            _tRex.JumpComplete += tRex_JumpComplete;

            _scoreBoard = new ScoreBoard(_spriteSheetTexture, new Vector2(SCORE_BOARD_POS_X, SCORE_BOARD_POS_y), _tRex);

            _inputController = new InputController(_tRex);

            _groundManager = new GroundManager(_spriteSheetTexture, _entityManager, _tRex);

            _entityManager.AddEntity(_tRex);
            _entityManager.AddEntity(_groundManager);
            _entityManager.AddEntity(_scoreBoard);

            _groundManager.Initialize();
        }

        private void tRex_JumpComplete(object sender, EventArgs e)
        {
            if(State == GameState.Transition)
            {
                State = GameState.Playing;
                _tRex.Initialize();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            if (State == GameState.Playing)
                _inputController.ProcessControls(gameTime);
            else if (State == GameState.Transition)
                _fadeInTexturePosX += (float)gameTime.ElapsedGameTime.TotalSeconds * FADE_IN_ANIMATION_SPEED;
            else if (State == GameState.Initial)
            {
                bool isStartKeyPressed = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space);
                bool wasStartKeyPressed = _previousKeyboardState.IsKeyDown(Keys.Up) || _previousKeyboardState.IsKeyDown(Keys.Space);

                if (isStartKeyPressed && !wasStartKeyPressed)
                {
                    StartGame();
                }
            }

            _entityManager.Update(gameTime);

            _previousKeyboardState = keyboardState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _entityManager.Draw(_spriteBatch, gameTime);

            if (State == GameState.Initial || State == GameState.Transition)
            {
                _spriteBatch.Draw(_fadeInTexture, new Rectangle((int)Math.Round(_fadeInTexturePosX), 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool StartGame()
        {
            if (State != GameState.Initial)
                return false;

            State = GameState.Transition;
            _tRex.BeginJump();

            return true;
        }
    }
}
