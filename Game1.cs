using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonogameAug21
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        enum GameState
        {
            menu,
            running
        }

        Hero hero;
        Blok blokje;
        GameState gameState;

        Menu menu;

        Level level1;
        Byte[,] level1Array;
        Level level2;
        int level1CoinAmount = 0;
        Byte[,] level2Array;
        int level2CoinAmount = 0;
        List<Level> levels;
        List<int> coins;
        int currentLevel = 0;

        List<Blok> colObj;

        Texture2D heroTexture;
        Texture2D resumeTexture;
        Texture2D quitTexture;

        KeyBoardReader keyboard;
        MouseReader mouseReader;

        CollisionManager collisionManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            gameState = GameState.running;
            // TODO: Add your initialization logic here
            keyboard = new KeyBoardReader();
            mouseReader = new MouseReader();
            collisionManager = new CollisionManager();
            //TODO: make level arrays
            level1Array = new Byte[12, 20]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                { 2, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                { 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 0, 0, 0, 2, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 3, 3},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };
            foreach (var item in level1Array)
            {
                if (item == 2)
                {
                    level1CoinAmount++;
                }
            }
            level2Array = new Byte[12, 20]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2},
                { 1, 0, 0, 1, 0, 0, 0, 0, 3, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                { 0, 0, 0, 1, 2, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 0, 3, 3, 3, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };
            foreach (var item in level2Array)
            {
                if (item == 2)
                {
                    level2CoinAmount++;
                }
            }
            coins = new List<int>();
            coins.Add(level1CoinAmount);
            coins.Add(level2CoinAmount);

            level1 = new Level(Content, level1Array);
            level2 = new Level(Content, level2Array);

            levels = new List<Level>();
            levels.Add(level1);
            levels.Add(level2);

            colObj = new List<Blok>();

            foreach (Level item in levels)
            {
                item.CreateWorld();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            heroTexture = Content.Load<Texture2D>("player");
            resumeTexture = Content.Load<Texture2D>("resume");
            quitTexture = Content.Load<Texture2D>("quit");

            InitializeGameObj();
            
        }

        private void InitializeGameObj()
        {
            hero = new Hero(heroTexture, keyboard);
            menu = new Menu(mouseReader, resumeTexture, quitTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameState = GameState.menu;

            // TODO: Add your update logic here
            if (gameState == GameState.running)
            {

                for (int x = 0; x < 12; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        if (currentLevel == levels.Count)
                        {
                            break;
                        }
                        if (levels[currentLevel].coinArray[x, y] != null)
                        {

                            if (collisionManager.CheckCollision(hero.collisionRectangle, levels[currentLevel].coinArray[x, y].collisionRectangle))
                            {
                                levels[currentLevel].coinArray[x, y] = null;
                                coins[currentLevel]--;
                                if (coins[currentLevel] == 0)
                                {
                                    currentLevel++;
                                    if (currentLevel == levels.Count)
                                    {
                                        Exit();
                                        break;
                                    }
                                    hero.Reset();
                                }
                            }

                        }
                        if (levels[currentLevel].spikeArray[x, y] != null)
                        {
                            if (collisionManager.CheckCollision(hero.collisionRectangle, levels[currentLevel].spikeArray[x, y].collisionRectangle))
                            {
                                hero.Reset();
                            }
                        }

                        if (levels[currentLevel].blokArray[x, y] != null)
                        {
                            colObj.Add(levels[currentLevel].blokArray[x, y]);
                        }


                    }
                }

                hero.Update(gameTime, colObj, collisionManager);
                colObj.Clear();



            }
            else if (gameState == GameState.menu)
            {
                menu.Update();
                if (menu.resume)
                {
                    gameState = GameState.running;
                }
                if (menu.quit)
                {
                    Exit();
                }
            }
              
            
            base.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (gameState == GameState.running)
            {
                levels[currentLevel].DrawWorld(_spriteBatch);
            
            
                hero.Draw(_spriteBatch);

            }
            else if (gameState == GameState.menu)
            {
                menu.Draw(_spriteBatch);
            }

            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
