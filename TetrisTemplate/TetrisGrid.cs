using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

 
 // Class that represents the current Tetris Grid
class TetrisGrid
{
    public TetrisGrid()
    {
        // set the position of the grid to zero, and clear the grid
        position = Vector2.Zero;
        this.Clear();
    }
    // Declare an array of 4x4 that represents the current playing field
    protected bool[,] maingrid = new bool[12, 20];
    // member variable for the position of the grid
    Vector2 position;
     
   // Property for the current width of the Grid
    public static int GridWidth
    {
        get { return 12; }
    }
     
   // Property for the current Height of the grid
    public static int GridHeight
    {
        get { return 20; }
    }

    // Clears the grid (sets everything to false)
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

    // Method for checking if a position of a block is valid
    public bool IsValid(Block block)
    {
        bool[,] blockgrid = block.BlockGrid;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (blockgrid[x, y])
                {
                    // Checks if the block is either out of the right or the left of the playing field and thus it's position is invalid
                    if(x + block.OffsetX < 0 || x + block.OffsetX >= GridWidth)
                    {
                        return false;
                    }
                    // Checks if the block is out of the bottom or the top of the playing field, and returns false if this is true because
                    // the position is invalid if this is the case
                    if(y + block.OffsetY >= GridHeight || y + block.OffsetY < 0)
                    {
                        return false;
                    }
                    // Checks if the block collides with the maingrid (and if this is true the position is invalid)
                    if(maingrid[x + block.OffsetX, y + block.OffsetY])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    // Method for writing the current block to the playing field
    public void PlaceBlock(Block block)
    {
        bool[,] blockgrid = block.BlockGrid;
        // Increase the score
        Score.currentscore += 1;
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
    // Move all the rows down from the specified height
    public void MoveRowsDown(int height)
    {
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                // The row below the current row is set as the current row
                maingrid[x, y + 1] = maingrid[x, y];
                // the current row is then cleared
                maingrid[x, y] = false;
            }
        }
        // give score for clearing a line
        Score.currentscore += 25;
    }
    // Method for checking if a row is full
    public void LineisFull()
    {
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
                        // clears the full row
                        maingrid[x, y] = false;
                    }
                    // moves the rows down above the line that was just cleared
                    MoveRowsDown(y);
                }

            } 

        }
    }
    // update the grid (checks if a row is full)
    public void Update(GameTime gameTime)
    {
        LineisFull();
    }
    // Draw the Grid
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
