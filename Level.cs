using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace MonogameAug21
{
    class Level
    {
        public Texture2D blokTexture;
        public Texture2D coinTexture;
        public Texture2D spikeTexture;
        public Byte[,] tileArray;

        public Blok[,] blokArray = new Blok[12, 20];
        public Coin[,] coinArray = new Coin[12, 20];
        public Spike[,] spikeArray = new Spike[12, 20];
        private ContentManager content;

        public Level(ContentManager content, Byte[,] levelArray)
        {
            this.content = content;
            tileArray = levelArray;

            InitializeContent();
        }

        private void InitializeContent()
        {
            blokTexture = content.Load<Texture2D>("blok");
            coinTexture = content.Load<Texture2D>("coin3");
            spikeTexture = content.Load <Texture2D>("spike");

        }

        //same in class enemy
        public void CreateWorld()
        {
            for (int x = 0; x < 12; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (tileArray[x,y]  == 1)
                    {
                        blokArray[x, y] = new Blok(blokTexture, new Vector2(y * 40, x * 40));
                    }
                    if (tileArray[x,y] == 2)
                    {
                        coinArray[x, y] = new Coin(coinTexture, new Vector2(y * 40, x * 40));
                    }
                    if (tileArray[x, y] == 3)
                    {
                        spikeArray[x, y] = new Spike(spikeTexture, new Vector2(y * 40, x * 40));
                    }
                    
                    
                }
            }
        }

        public void DrawWorld(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 12; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (blokArray[x,y] != null)
                    {
                        blokArray[x, y].Draw(spriteBatch);
                    }
                    if (coinArray[x,y] != null)
                    {
                        coinArray[x, y].Draw(spriteBatch);
                    }
                    if (spikeArray[x,y] != null)
                    {
                        spikeArray[x, y].Draw(spriteBatch);
                    }

                }
            }
        }
    }
}
