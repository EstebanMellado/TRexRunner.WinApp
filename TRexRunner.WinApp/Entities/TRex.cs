using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TRexRunner.WinApp.Graphics;

namespace TRexRunner.WinApp.Entities
{
    public class TRex : IGameEntity
    {
        private const int TREX_DEFAULT_SPRITE_POS_X = 848;
        private const int TREX_DEFAULT_SPRITE_POS_Y = 0;
        private const int TREX_DEFAULT_SPRITE_POS_WIDTH = 44;
        private const int TREX_DEFAULT_SPRITE_POS_HEIGHT = 52;

        public int DrawOrder { get; set; }
        public Vector2 Position { get; set; }
        public Sprite Sprite { get; private set; }
        public TRexState State { get; private set; }
        public bool IsAlive { get; private set; }
        public float Speed { get; private set; }

        public TRex(Texture2D spriteBatch, Vector2 position)
        {
            Sprite = new Sprite(spriteBatch, TREX_DEFAULT_SPRITE_POS_X, TREX_DEFAULT_SPRITE_POS_Y, TREX_DEFAULT_SPRITE_POS_WIDTH, TREX_DEFAULT_SPRITE_POS_HEIGHT);
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        internal static int GetHeight()
        {
            return TREX_DEFAULT_SPRITE_POS_HEIGHT;
        }
    }
}
