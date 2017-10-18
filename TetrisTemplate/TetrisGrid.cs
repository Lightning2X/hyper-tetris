using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

 
 // a class for representing the Tetris playing grid
  
class TetrisGrid
{
    bool isthereablock;
    public TetrisGrid(Texture2D b)
    {
        gridblock = b;
        position = Vector2.Zero;
        this.Clear();
    }
    protected bool[,] gridlock = new bool[12, 20];

    public int AVL = 0;
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
        return gridlock[x, y]; 
    }
    public void setGridPositions(int x, int y, bool set)
    {
        // setter
        gridlock[x, y] = set;
    }

     // clears the grid
      
    public void Clear()
    {
        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                gridlock[x, y] = false;
            }
        }
    }

    public void LineisFull()
    {
        int full = 0;
        for(int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                if (gridlock[x, y])
                {
                    full++;
                }
            }
        }
        TetrisGame.Variables.score += 100; 
    }

    public bool IsThereABlock
    {
        get { return isthereablock; }
        set { isthereablock = value; }
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
                if (gridlock[x, y])
                {
                    s.Draw(block, new Vector2(x + block.Width, y + block.Height), Color.White);     
                }
            }
        }
    }
}
