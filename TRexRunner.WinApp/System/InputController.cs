using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TRexRunner.WinApp.Entities;

namespace TRexRunner.WinApp.System
{
    public class InputController
    {
        private TRex _tRex;
        private KeyboardState _previousKeyboardState;

        public InputController(TRex tRex)
        {
            _tRex = tRex;
        }

        public void ProcessControls(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!_previousKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Up))
            {
                if (_tRex.State != TRexState.Jumping)
                    _tRex.BeginJump();
            }
            else if (_tRex.State == TRexState.Jumping && !keyboardState.IsKeyDown(Keys.Up))
                _tRex.CancelJump();
            _previousKeyboardState = keyboardState;
        }
    }
}
