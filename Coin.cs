using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    class Coin : ICollisionObj
    {
        public Texture2D texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle collisionRectangle { get; set; }

        Rectangle tekenRectangle;

        public Coin(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            tekenRectangle = new Rectangle(Convert.ToInt32(Position.X)+10, Convert.ToInt32(Position.Y)+15, 20, 20);
            collisionRectangle = tekenRectangle;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, tekenRectangle, Color.AliceBlue);
        }
    }
}
