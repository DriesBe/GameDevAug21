using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameAug21
{
    class Hero :  ITransform, ICollisionObj
    {
        Texture2D heroTexture;
        Animation animation;
        IInputReader inputReader;
        private IGameCommand moveCommand;
        Vector2 direction;
        Vector2 start;
        public Vector2 adjustedPosition;
        public Rectangle lateralCheckRectangle;
        public Rectangle verticalCheckRectangle;
        public Rectangle jumpCheck;
        bool grounded = false;

        public Vector2 positie { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Rectangle _collisionRectangle;

        public Hero(Texture2D texture, IInputReader reader)
        {
            heroTexture = texture;
            inputReader = reader;
            animation = new Animation();
            //Add every frame in order

            for (int i = 0; i < 8; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(i * 46, 150, 46, 50)));
            }
            start = new Vector2(10, 380);
            adjustedPosition = new Vector2(0,0);
            positie = start;
            _collisionRectangle = new Rectangle(Convert.ToInt32(positie.X), Convert.ToInt32(positie.Y), 46, 50);
            collisionRectangle = _collisionRectangle;
            lateralCheckRectangle = collisionRectangle;
            verticalCheckRectangle = collisionRectangle;
            jumpCheck = collisionRectangle;
            

            moveCommand = new MoveCommand();


        }

        public void Update(GameTime gameTime, List<Blok>colBloks, CollisionManager collisionManager)
        {
            direction = inputReader.ReadInput();
            
            
            //jump if on block and wanting to jump
            if (grounded == false)
            {
                direction.Y = 2;
            }
            else if (grounded && direction.Y < 0)
            {
                grounded = false;
            }
            else if (grounded && direction.Y > 0)
            {
                direction.Y = 2;
            }            
            
            
            animation.Update(gameTime);
            lateralCheckRectangle.X += (int)direction.X;
            verticalCheckRectangle.Y += (int)direction.Y;
            

            Debug.WriteLine(positie);
            Debug.WriteLine(lateralCheckRectangle.X);
            Debug.WriteLine(verticalCheckRectangle.Y);




            foreach (Blok item in colBloks)
            {
                //check if it will collide if moved sideways
                if (collisionManager.CheckCollision(lateralCheckRectangle, item.collisionRectangle))
                {
                    //als we te ver naar links gaan
                    if (lateralCheckRectangle.X < item.collisionRectangle.X + item.collisionRectangle.Width)
                    {
                        direction.X = 0;
                    }
                    //als we te ver naar recht sgaan
                    if (lateralCheckRectangle.X + lateralCheckRectangle.Width > item.collisionRectangle.X)
                    {
                        direction.X = 0;
                    }
                }

                for (int y = 0; y > direction.Y; y--)
                {
                    jumpCheck.Y = collisionRectangle.Y + y; // check the height because jumping under a blok is weird
                    if (collisionManager.CheckCollision(jumpCheck, item.collisionRectangle))
                    {
                        direction.Y = y+1;
                        grounded = false;
                        break;
                    }
                }                
                if (collisionManager.CheckCollision(verticalCheckRectangle, item.collisionRectangle))
                {
                    //check if we're on a block
                    if (direction.Y > 0 && verticalCheckRectangle.Y + verticalCheckRectangle.Height > item.collisionRectangle.Y)
                    {
                        direction.Y = 0;
                        grounded = true;
                        Debug.WriteLine("on a block");
                    }
                }

            }

            //fix wrong on block check and going throug a block when holding up(might be because of the first problem)


            if (direction.Y<0)
            {
                grounded = false;
            }
            

            
            
            
            
            //don't go off screen
            if (lateralCheckRectangle.X < 0 || lateralCheckRectangle.X > 800)
            {
                direction.X = 0;
            }




            Debug.WriteLine(grounded);
            Move(direction);
            
            _collisionRectangle.X = (int)positie.X;
            _collisionRectangle.Y = (int)positie.Y;
            collisionRectangle = _collisionRectangle;
            verticalCheckRectangle = collisionRectangle;
            lateralCheckRectangle = collisionRectangle;
            jumpCheck = collisionRectangle;
            
            
            
        }
        public void Reset()
        {
            positie = start;
        }

        private void Move(Vector2 dir)
        {
           
            moveCommand.Execute(this, dir);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(heroTexture, positie, animation.currentFrame.SourceRectangle ,Color.White);
        }
    }
}
