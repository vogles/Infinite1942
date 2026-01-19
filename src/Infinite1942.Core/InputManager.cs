using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infinite1942
{
    public class InputManager(Game game) : GameComponent(game)
    {
        private KeyboardState _keyboardState;
        private MouseState _mouseState;
        private GamePadState _gamePadState;

        public override void Update(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            _gamePadState = GamePad.GetState(0);
            
            base.Update(gameTime);
        }
    }
}
