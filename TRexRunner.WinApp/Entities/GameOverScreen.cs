﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TRexRunner.WinApp.Graphics;

namespace TRexRunner.WinApp.Entities
{
    public class GameOverScreen : IGameEntity
    {
        private const int GAME_OVER_TEXTURE_POS_X = 655;
        private const int GAME_OVER_TEXTURE_POS_Y = 14;

        public const int GAME_OVER_SPRITE_WIDTH = 192;
        private const int GAME_OVER_SPRITE_HEIGHT = 14;

        private const int BUTTON_TEXTURE_POS_X = 1;
        private const int BUTTON_TEXTURE_POS_Y = 1;
        private const int BUTTON_SPRITE_WIDTH = 38;
        private const int BUTTON_SPRITE_HEIGHT = 34;
        private readonly TRexRunnerGame _game;
        private Sprite _textSprite;
        private Sprite _buttonSprite;

        private Vector2 ButtonPosition => Position + new Vector2(GAME_OVER_SPRITE_WIDTH / 2 - BUTTON_SPRITE_WIDTH / 2, GAME_OVER_SPRITE_HEIGHT + 20);

        private Rectangle ButtonBounds => new Rectangle(ButtonPosition.ToPoint(), new Point(BUTTON_SPRITE_WIDTH, BUTTON_SPRITE_HEIGHT));

        public int DrawOrder => 100;

        public Vector2 Position { get; set; }
        public bool IsEnabled { get; set; }

        public GameOverScreen(Texture2D spriteSheet, TRexRunnerGame game)
        {
            _textSprite = new Sprite(spriteSheet, GAME_OVER_TEXTURE_POS_X, GAME_OVER_TEXTURE_POS_Y, GAME_OVER_SPRITE_WIDTH, GAME_OVER_SPRITE_HEIGHT);
            _buttonSprite = new Sprite(spriteSheet, BUTTON_TEXTURE_POS_X, BUTTON_TEXTURE_POS_Y, BUTTON_SPRITE_WIDTH, BUTTON_SPRITE_HEIGHT);
            _game = game;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsEnabled)
                return;
            _textSprite.Draw(spriteBatch, Position);
            _buttonSprite.Draw(spriteBatch, ButtonPosition);
        }

        public void Update(GameTime gameTime)
        {
            if (!IsEnabled)
                return;

            MouseState mouseState = Mouse.GetState();
            if (ButtonBounds.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _game.Replay();
            }
        }
    }
}
