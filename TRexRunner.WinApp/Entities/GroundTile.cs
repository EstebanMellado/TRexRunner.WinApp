using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRexRunner.WinApp.Graphics;

namespace TRexRunner.WinApp.Entities
{
    public class GroundTile : IGameEntity
    {
        public GroundTile(float positionX, float positionY, Sprite sprite)
        {
            PositionX = positionX;
            Sprite = sprite;
            _positionY = positionY;
        }

        public float PositionX { get; set; }
        public Sprite Sprite { get; }
        public int DrawOrder { get; set; }
        private float _positionY;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, new Vector2(PositionX, _positionY));
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
