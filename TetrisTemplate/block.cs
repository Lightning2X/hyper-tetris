//using System;
//using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Block
{
    private Point offset;
    protected int rotation;
    protected bool[,] blockposition = new bool[4, 4];
    public Block(int x, int y)
    {
        BuildBlockGrid();
        offset.Y = y;
        offset.X = x;        
    }

    public void MoveRight()
    {
        offset.X++;
    }
    public void MoveLeft()
    {
        offset.X--;
    }
    
    public virtual void MoveDown()
    {
        offset.Y++;
    }
    public void MoveUp()
    {
        offset.Y--;
    }
    
    public virtual void RotateRight()
    {
        // Decreases rotation on button press
        if (rotation <= 2)
        {
            rotation++;
        }
        else
        {
            rotation = 0;
        }
        BuildBlockGrid();
    }
    public virtual void RotateLeft()
    {
        // Increases rotation on button press
        if (rotation >= 1)
        {
            rotation--;
        }
        else
        {
            rotation = 3;
        }
        BuildBlockGrid();
    }
    protected virtual void BuildBlockGrid()
    {
        ResetBlockGrid();
    }
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
    public static int RandomiseBlockType()
    {
        int blocktype = GameWorld.RandomNumber(0, 7);
        return blocktype;
    }
    public static Block CreateBlock(int x, int y, int blocktype)
    {
        if (blocktype == 0)
            //     Bouw blok I (streep)
            return new BlockI(x , y);
        else if (blocktype == 1)
        {
            //    Bouw blok J
            return new BlockJ(x, y);
        }
        else if (blocktype == 2)
        {
            //     Bouw blok L
            return new BlockL(x, y);
        }
        else if (blocktype == 3)
        {
            //    Bouw blok O (vierkant)
            return new BlockO(x, y);
        }
        else if (blocktype == 4)
        {
            //  Bouw blok S
            return new BlockS(x, y);
        }
        else if (blocktype == 5)
        {
            // Bouw blok T
            return new BlockT(x, y);

        }
        else if (blocktype == 6)
        {
            // Bouw blok Z
            return new BlockZ(x, y);
        }
        else
            return null;
    }

    // Draw the block
    public void Draw(GameTime gameTime, SpriteBatch s, Texture2D block)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockposition[x, y])
                {
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

    public bool[,] BlockGrid
    {
        get { return blockposition; }
    }
    public int OffsetY
    {
        get { return offset.Y; }
    }
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
        // verkeerde block roteert atm
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
