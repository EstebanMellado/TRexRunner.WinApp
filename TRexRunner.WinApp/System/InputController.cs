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
            bool isJumpKeyPressed = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space);
            bool wasJumpKeyPressed = _previousKeyboardState.IsKeyDown(Keys.Up) || _previousKeyboardState.IsKeyDown(Keys.Space);

            if (!wasJumpKeyPressed && isJumpKeyPressed)
            {
                if (_tRex.State != TRexState.Jumping)
                    _tRex.BeginJump();
            }
            else if (_tRex.State == TRexState.Jumping && !isJumpKeyPressed)
            {
                _tRex.CancelJump();
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (_tRex.State == TRexState.Jumping || _tRex.State == TRexState.Falling)
                    _tRex.Drop();
                else
                    _tRex.Duck();
            }
            else if (_tRex.State == TRexState.Ducking && !keyboardState.IsKeyDown(Keys.Down))
                _tRex.GetUp();

            _previousKeyboardState = keyboardState;
        }
    }
}
