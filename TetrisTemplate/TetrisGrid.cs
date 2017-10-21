using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

 
 // a class for representing the Tetris playing grid
  
class TetrisGrid
{
    public TetrisGrid()
    {
        position = Vector2.Zero; // positie van het grid
        gameover = false;
        this.Clear();
    }
    protected bool[,] maingrid = new bool[12, 20];
    protected bool gameover;
     // the position of the tetris grid
      
    Vector2 position;
     
     // width in terms of grid elements
      
    public static int GridWidth
    {
        get { return 12; }
    }
     
     // height in terms of grid elements
      
    public static int GridHeight
    {
        get { return 20; }
    }
    public bool getGridPositions(int x, int y)
    {
        // getter
        return maingrid[x, y]; 
    }

     // clears the grid
      
    public void Clear()
    {
        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                maingrid[x, y] = false;
            }
        }
    }

    public bool IsValid(Block block)
    {
        bool[,] blockgrid = block.BlockGrid;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (blockgrid[x, y])
                {
                    if(x + block.OffsetX < 0 || x + block.OffsetX >= GridWidth)
                    {
                        return false;
                    }
                    if(y + block.OffsetY >= GridHeight || y + block.OffsetY < 0)
                    {
                        return false;
                    }

                    if(maingrid[x + block.OffsetX, y + block.OffsetY])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public void PlaceBlock(Block block)
    {
        bool[,] blockgrid = block.BlockGrid;
        Score.currentscore += 1;
        // Collision with another block in the field
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (blockgrid[x, y])
                    {
                        maingrid[x+ block.OffsetX, y + block.OffsetY] = blockgrid[x, y];
                    }
                }
            }
    }
    public void MoveRowsDown(int height)
    {
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                maingrid[x, y + 1] = maingrid[x, y];
                maingrid[x, y] = false;
            }
        }
        Score.currentscore += 25;
    }
    public void LineisFull()
    {
        int clearedlines = 0;
        for (int y = 0; y < GridHeight; y++)
        {
            int numberfull = 0;
            for (int x = 0; x < GridWidth; x++)
            {
                if (maingrid[x, y])
                {
                    numberfull++;
                }
                if (numberfull == GridWidth)
                {
                    for(x = 0; x < GridWidth - 1; x++)
                    {
                        maingrid[x, y] = false;
                    }
                    MoveRowsDown(y);
                }

            } 

        }
    }

    public void Update(GameTime gameTime)
    {
        LineisFull();
    }
    public void Draw(GameTime gameTime, SpriteBatch s, Texture2D block)
    {
        for (int x = 0;  x < GridWidth; x++)
        {
            for(int y = 0;  y < GridHeight; y++)
            {
                if (maingrid[x, y])
                {
                    s.Draw(block, new Vector2(x * block.Width, y * block.Height), Color.White);     
                }
            }
        }
    }
}
