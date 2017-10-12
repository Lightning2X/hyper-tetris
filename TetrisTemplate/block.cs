using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Block
{
    public int blocktype, offsetx, offsety;
    protected int rotation;
    protected bool[,] blockposition = new bool[4, 4];
    public Block()
    {
        offsety = 0;
        offsetx = 0;
        rotation = GameWorld.RandomNumber(0, 4);
        blocktype = GameWorld.RandomNumber(0, 7);
    }

    protected void BlockEvent(InputHelper inputhelper)
    {
        if (inputhelper.KeyPressed(Keys.A, false))
        {
            RotateLeft();
        }
        if (inputhelper.KeyPressed(Keys.D, false))
        {
            RotateRight();
        }
        if (inputhelper.KeyPressed(Keys.S, true))
        {
            MoveDown();
        }
        if (inputhelper.KeyPressed(Keys.Left, false))
        {
            MoveLeft();
        }
        if (inputhelper.KeyPressed(Keys.Right, false))
        {
            MoveRight();
        }
        // gofaster is set back to false if S isn't pressed
    }
    protected virtual void MoveRight()
    {
        if(offsetx <= (TetrisGrid.GridWidth - 4))
        {
            offsetx++;
        }
    }
    protected virtual void MoveLeft()
    {
        if(offsetx >= 0)
        {
            offsetx--;
        }
    }
    public int Yoffset
    {
        get { return offsety; }
        set { offsety = value; }
    }
    protected virtual void MoveDown()
    {
        // TODO: add an integer that increases y speed
        offsety++;
    }
    protected virtual void RotateRight()
    {
        // Decreases rotation on button press
        if (rotation >= 0)
        {
            rotation--;
        }
        else
            rotation = 3;
    }
    protected virtual void RotateLeft()
    {
        // Increases rotation on button press
        if (rotation <= 3)
        {
            rotation++;
        }
        else
            rotation = 0;

    }
    public bool GetBlockPosition(int x, int y)
    {
        return blockposition[x, y];
    }
    protected virtual void BlockPosition()
    {
        // deze functie bouwt voor elk blok een andere versie van 4 arrays van 4 x 4, daarom is hij hier leeg
    }
    protected void ResetBlockPosition()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                blockposition[x, y] = false;
            }
        }
    }
    public int NextBlock()
    {
        blocktype = GameWorld.RandomNumber(0, 7);
        return blocktype;
    }
    public Block RandomBlock()
    {
        if (blocktype == 0)
            //     Bouw blok I (streep)
            return new BlockI();
        else if (blocktype == 1)
        {
            //    Bouw blok J
            return new BlockJ();
        }
        else if (blocktype == 2)
        {
            //     Bouw blok L
            return new BlockL();
        }
        else if (blocktype == 3)
        {
            //    Bouw blok O (vierkant)
            return new BlockO();
        }
        else if (blocktype == 4)
        {
            //  Bouw blok S
            return new BlockS();
        }
        else if (blocktype == 5)
        {
            // Bouw blok T
            return new BlockT();

        }
        else if (blocktype == 6)
        {
            // Bouw blok Z
            return new BlockZ();
        }
        else
            return null;
    }
    public int Blocktype
    {
        get { return blocktype; }
    }
    public void Draw(GameTime gameTime, SpriteBatch s, Texture2D block)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockposition[x, y])
                {
                    s.Draw(block, new Vector2(offsetx + (x + block.Width), (offsety + (y + block.Height))), Color.White);
                }
            }
        }
    }
}

class BlockI : Block
{
    public BlockI()
    {

    }
    protected override void BlockPosition()
    {
        ResetBlockPosition();
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
    public BlockJ()
    {
        
    }

    protected override void BlockPosition()
    {
        ResetBlockPosition();
        if (rotation == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                blockposition[1, i] = true;
            }
            blockposition[0, 3] = true;
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
    public BlockL()
    {

    }
    protected override void BlockPosition()
    {
        ResetBlockPosition();
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
            blockposition[3, 0] = true;
        }
    }
}
class BlockO : Block
{
    public BlockO()
    {

    }
    protected override void BlockPosition()
    {
        blockposition[0, 0] = true;
        blockposition[1, 0] = true;
        blockposition[0, 1] = true;
        blockposition[1, 1] = true;
    }
}
class BlockS : Block
{
    public BlockS()
    {

    }
    protected override void BlockPosition()
    {
        ResetBlockPosition();
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
    public BlockT()
    {

    }
    protected override void BlockPosition()
    {
        ResetBlockPosition();
        if (rotation == 0)
        {
            for (int i = 0; i < 3; i++)
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
    public BlockZ()
    {

    }
    protected override void BlockPosition()
    {
        ResetBlockPosition();
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
