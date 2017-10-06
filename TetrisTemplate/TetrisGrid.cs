using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

 
 // a class for representing the Tetris playing grid
  
class TetrisGrid
{
    public TetrisGrid(Texture2D b)
    {
        gridblock = b;
        position = Vector2.Zero;
        this.Clear();
    }

    protected bool[,] gridlock = new bool[12, 20];
     
     // sprite for representing a single grid block
      
    Texture2D gridblock;

     
     // the position of the tetris grid
      
    Vector2 position;

     
     // width in terms of grid elements
      
    public int GridWidth
    {
        get { return 12; }
    }

     
     // height in terms of grid elements
      
    public int GridHeight
    {
        get { return 20; }
    }

    public bool getGridPositions(int x, int y)
    {
        // getter
        return gridlock[x , y]
        
    }

    public void setGridPositions(int x, int y, bool set)
    {
        gridlock[x, y] = set;
    }

     // clears the grid
      
    public void Clear()
    {
    }

     
     // draws the grid on the screen
      
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        for (int x = 0;  x < GridWidth; x++)
        {
            for(int y = 0;  y < GridHeight; y++)
            {
                if (gridlock[x, y])
                {
                    // Draw the sprite
                    // block.draw ofzo 
                }
            }
        }
    }
}
