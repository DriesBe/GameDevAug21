using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    class MouseReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
            {
                Vector2 position = new Vector2(state.X, state.Y);
                return position;
            }
            else return new Vector2(0,0);
                
        }
    }
}
