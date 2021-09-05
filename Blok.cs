using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    public class Blok : ICollisionObj
    {
        public Texture2D texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle collisionRectangle { get; set; }

        Rectangle tekenRectangle;

        public Blok(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            tekenRectangle = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), 40, 40);
            collisionRectangle = tekenRectangle;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, tekenRectangle, Color.AliceBlue);
        }
    }
}
