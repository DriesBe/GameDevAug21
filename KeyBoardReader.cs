using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    class KeyBoardReader : IInputReader
    {   
        public Vector2 ReadInput()
        {
            var direction = Vector2.Zero;
            direction.Y = 1;
            
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
            {
                direction.X = -5;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction.X = 5;
            }
            // only after touching the ground
            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y = -140;
                
            }
            return direction;
        }
    }
}
