using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    public class MoveCommand : IGameCommand
    {

        public MoveCommand()
        {

        }
        
        public void Execute(ITransform transform, Vector2 dir)
        {

            transform.positie += dir;
        }
    }
}
