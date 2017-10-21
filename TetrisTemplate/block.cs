//using System;
//using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Block
{
    // make a Point for the current offset of the block grid, I use a Point because grids are only integer values and a Vector2
    // will only result in me casting it to int constantly.
    private Point offset;
    // Declare a member variable to measure the current rotation
    protected int rotation;
    // Maka a 4x4 array that will represent a block
    protected bool[,] blockposition = new bool[4, 4];
    public Block(int x, int y)
    {
        //Build the current block grid
        BuildBlockGrid();
        // assign the constructor variables to the offset y and offset x
        offset.Y = y;
        offset.X = x;        
    }

    // Method to move a block right
    public void MoveRight()
    {
        offset.X++;
    }
    // Method to make a block move left
    public void MoveLeft()
    {
        offset.X--;
    }
    // Method to make a block move down (by increasing the y offset)
    public virtual void MoveDown()
    {
        offset.Y++;
    }
    // Method to move a block up, this one is only used in checking if a block is valid on a location
    public void MoveUp()
    {
        offset.Y--;
    }
    // This method increases the rotation of a block if it is called
    public virtual void RotateRight()
    {
        // if the rotation reaches above 2, go to 0 because this is the next logical rotation
        if (rotation <= 2)
        {
            rotation++;
        }
        else
        {
            rotation = 0;
        }
        //Rebuild the grid so it updates to the new rotation
        BuildBlockGrid();
    }
    // This method decreases the rotation of a block if it is called
    public virtual void RotateLeft()
    {
        // Same story as with the method RotateRight() but now the rotation is reset to 3 if it was 0 before
        if (rotation >= 1)
        {
            rotation--;
        }
        else
        {
            rotation = 3;
        }
        //Rebuild the grid so it updates to the new rotation
        BuildBlockGrid();
    }
    //Default BuildBlockGrid only contains the reset method because every block has different rotations, and this is the only thing
    //handled in BuildBlockGrid
    protected virtual void BuildBlockGrid()
    {
        ResetBlockGrid();
    }
    // Method to reset the block grid, it goes over every position in the 4x4 array and replaces it by false (empty)
    private void ResetBlockGrid()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                blockposition[x, y] = false;
            }
        }
    }
    // This method randomizes the blocktype, this Method isn't incorporated into CreateBlock because it is needed to display the next
    // block on the HUD
    public static int RandomiseBlockType()
    {
        int blocktype = GameWorld.RandomNumber(0, 7);
        return blocktype;
    }
    // this Method returns a new Block based on the block type given
    public static Block CreateBlock(int x, int y, int blocktype)
    {
        if (blocktype == 0)
            // Build Block I (stripe)
            return new BlockI(x , y);
        else if (blocktype == 1)
        {
            // Build Block J 
            return new BlockJ(x, y);
        }
        else if (blocktype == 2)
        {
            // Build Block L
            return new BlockL(x, y);
        }
        else if (blocktype == 3)
        {
            // Build Block O (square)
            return new BlockO(x, y);
        }
        else if (blocktype == 4)
        {
            // Build Block S
            return new BlockS(x, y);
        }
        else if (blocktype == 5)
        {
            // Build a T block
            return new BlockT(x, y);
        }
        else if (blocktype == 6)
        {
            // Build Block Z
            return new BlockZ(x, y);
        }
        else
            return null;
        // in case of an invalid block type given, the method will return null
    }

    // Draw the block
    public void Draw(GameTime gameTime, SpriteBatch s, Texture2D block)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                //only draw a block if it is true because we do not want to draw empty blocks
                if (blockposition[x, y])
                {
                    // the block is drawn with its grid and offset in mind, so it correctly displays in the game
                    s.Draw(block, new Vector2(((offset.X + x) * block.Width), ((offset.Y + y) * block.Height)), Color.White);
                }
            }
        }
    }

    //Function that draws the block if it specified as a "nextblock" (for the HUD)
    public void DrawNextBlock(GameTime gameTime, SpriteBatch s, Texture2D block, Point hudoffset)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockposition[x, y])
                {
                    s.Draw(block, new Vector2(hudoffset.X + (x * block.Width), hudoffset.Y + (y * block.Height)), Color.White);
                }
            }
        }
    }

    // Read-only property that returns the current array of the block
    public bool[,] BlockGrid
    {
        get { return blockposition; }
    }
    // Read-only property that returns the current horizontal offset
    public int OffsetY
    {
        get { return offset.Y; }
    }
    // Read-only property that returns the current vertical offset
    public int OffsetX
    {
        get { return offset.X; }
    }
}

class BlockI : Block
{
    public BlockI(int x, int y) : base(x, y)
    {

    }
    protected override void BuildBlockGrid()
    {
        // Block building method for Block I, with the 4 rotations already pre coded, it will insert the values true so that 
       // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        if (rotation == 0)
        {
            for(int i = 0; i < 4; i++)
            {
                blockposition[1, i] = true;
            }
        }
        else if(rotation == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                blockposition[i , 1] = true;
            }
        }
        else if(rotation == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                blockposition[2, i] = true;
            }
        }
        else if(rotation == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                blockposition[i, 2] = true;
            }
        }
    }
}
class BlockJ : Block
{
    public BlockJ(int x, int y) : base(x, y)
    {
        
    }

    protected override void BuildBlockGrid()
    {
        // Block building method for Block J, with the 4 rotations already pre coded, it will insert the values true so that 
        // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        if (rotation == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[0, 2] = true;
        }
        else if (rotation == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[i, 1] = true;
            }
            blockposition[0, 0] = true;
        }
        else if (rotation == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[2, 0] = true;
        }
        else if (rotation == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[i, 1] = true;
            }
            blockposition[2, 2] = true;
        }
    }
}


class BlockL : Block
{
    public BlockL(int x, int y) : base(x, y)
    {

    }
    protected override void BuildBlockGrid()
    {
        // Block building method for Block L, with the 4 rotations already pre coded, it will insert the values true so that 
        // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        if (rotation == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[2, 2] = true;
        }
        else if (rotation == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[i, 1] = true;
            }
            blockposition[0, 2] = true;
        }
        else if (rotation == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[0, 0] = true;
        }
        else if (rotation == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[i, 1] = true;
            }
            blockposition[2, 0] = true;
        }
    }
}
class BlockO : Block
{
    public BlockO(int x, int y) : base(x, y)
    {

    }
    protected override void BuildBlockGrid()
    {
        // Block building method for Block O, with the 4 rotations already pre coded, it will insert the values true so that 
        // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        blockposition[0, 0] = true;
        blockposition[1, 0] = true;
        blockposition[0, 1] = true;
        blockposition[1, 1] = true;
    }

}
class BlockS : Block
{
    public BlockS(int x, int y) : base(x, y)
    {

    }
    protected override void BuildBlockGrid()
    {
        // Block building method for Block S, with the 4 rotations already pre coded, it will insert the values true so that 
        // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        if (rotation == 0)
        {
            blockposition[0, 2] = true;
            blockposition[1, 1] = true;
            blockposition[1, 2] = true;
            blockposition[2, 1] = true;
        }
        else if (rotation == 1)
        {
            blockposition[0, 0] = true;
            blockposition[0, 1] = true;
            blockposition[1, 1] = true;
            blockposition[1, 2] = true;
        }
        else if (rotation == 2)
        {
            blockposition[0, 1] = true;
            blockposition[1, 0] = true;
            blockposition[1, 1] = true;
            blockposition[2, 0] = true;
        }
        else if (rotation == 3)
        {
            blockposition[1, 0] = true;
            blockposition[1, 1] = true;
            blockposition[2, 1] = true;
            blockposition[2, 2] = true;
        }
    }
}

class BlockT : Block
{
    public BlockT(int x, int y) : base(x, y)
    {

    }
    protected override void BuildBlockGrid()
    {
        // Block building method for Block T, with the 4 rotations already pre coded, it will insert the values true so that 
        // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        if (rotation == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                blockposition[i, 1] = true;
            }
            blockposition[1, 2] = true;
        }
        else if (rotation == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[0, 1] = true;
        }
        else if (rotation == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[i, 1] = true;
            }
            blockposition[1, 0] = true;
        }
        else if (rotation == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[2, 1] = true;
        }
        
    }
}

class BlockZ : Block
{
    public BlockZ(int x, int y) : base(x, y)
    {

    }
    protected override void BuildBlockGrid()
    {
        // Block building method for Block Z, with the 4 rotations already pre coded, it will insert the values true so that 
        // the block is "build" according to it's current rotation
        base.BuildBlockGrid();
        if (rotation == 0)
        {
            blockposition[0, 1] = true;
            blockposition[1, 1] = true;
            blockposition[1, 2] = true;
            blockposition[2, 2] = true;
        }
        else if (rotation == 1)
        {
            blockposition[0, 1] = true;
            blockposition[0, 2] = true;
            blockposition[1, 0] = true;
            blockposition[1, 1] = true;
        }
        else if (rotation == 2)
        {
            blockposition[0, 0] = true;
            blockposition[1, 0] = true;
            blockposition[1, 1] = true;
            blockposition[2, 1] = true;
        }
        else if (rotation == 3)
        {
            blockposition[1, 1] = true;
            blockposition[1, 2] = true;
            blockposition[2, 0] = true;
            blockposition[2, 1] = true;
        }
    }
}
