using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infinite1942
{
    public class InputManager
    {
        private KeyboardState _keyboardState;
        private MouseState _mouseState;
        private GamePadState _gamePadState;

        public void Update()
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            _gamePadState = GamePad.GetState(0);
        }
    }
}
