using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        private Texture2D _spriteSheetTexture;
        private SoundEffect _sfxHit;
        private SoundEffect _sfxButtonPress;
        private SoundEffect _sfxScoredReached;

        public TRexRunnerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _spriteSheetTexture = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);
            _sfxHit = Content.Load<SoundEffect>(ASSET_NAME_SFX_HIT);
            _sfxButtonPress = Content.Load<SoundEffect>(ASSET_NAME_SFX_BUTTON_PRESS);
            _sfxScoredReached = Content.Load<SoundEffect>(ASSET_NAME_SFX_SCORE_REACHED);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spriteSheetTexture, new Vector2(10, 10), new Rectangle(848, 0, 44, 52), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
