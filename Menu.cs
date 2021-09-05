using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    class Menu
    {
        IInputReader reader;
        public Vector2 clickLocation;
        Texture2D resumeGame;
        Texture2D quitGame;
        public Rectangle resumeBox;
        public Rectangle quitBox;
        public bool resume = false;
        public bool quit = false;
        Point location;
        public Menu(IInputReader inputReader,Texture2D resume, Texture2D quit)
        {
            reader = inputReader;
            resumeGame = resume;
            quitGame = quit;
            resumeBox = new Rectangle(200, 215, 150, 50);
            quitBox = new Rectangle(450, 215, 150, 50);
            location = new Point();

        }
        public void Update()
        {
            resume = false;
            quit = false;
            clickLocation = reader.ReadInput();
            location.X = (int)clickLocation.X;
            location.Y = (int)clickLocation.Y;
            if (resumeBox.Contains(location))
            {
                resume = true;
            }
            else if (quitBox.Contains(location))
            {
                quit = true;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(resumeGame, resumeBox, Color.White);
            spriteBatch.Draw(quitGame, quitBox, Color.White);

        }
    }
}
