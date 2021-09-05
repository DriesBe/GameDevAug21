using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    public interface IGameCommand
    {
        void Execute(ITransform transform, Vector2 dir);
    }
}
