﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

 
 // a class for representing the Tetris playing grid
  
class TetrisGrid
{
    public TetrisGrid(Texture2D b)
    {
        gridblock = b;
        position = Vector2.Zero; // is dit de begin positie?
        this.Clear();
    }
    protected bool[,] maingrid = new bool[12, 20];

    // sprite for representing a single grid block
      
    Texture2D gridblock;

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
    public void setGridPositions(int x, int y, bool set)
    {
        // setter
        maingrid[x, y] = set;
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
                    if(y + block.OffsetY < 0 || y + block.OffsetY >= GridHeight)
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
        TetrisGame.Variables.score += 10;
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
    
    public void LineisFull()
    {// hier nog een check als de rij vol is.
        int n = 20;
        for(int y = n; y > GridHeight; y--)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                maingrid[x, y + 1] = maingrid[x, y];
            }        
        }
        TetrisGame.Variables.score += 100; 
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
