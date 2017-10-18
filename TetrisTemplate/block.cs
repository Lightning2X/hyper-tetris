//using System;
//using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Block
{
    protected int offsetcorrector, nexttick, tick;
    public int blocktype;
    protected Point offset;
    protected int rotation, previousrotation;
    protected bool[,] blockposition = new bool[4, 4];
    protected bool[,] testposition = new bool[4, 4];
    public Block()
    {
        offset.Y = 0;
        offset.X = 0;
        nexttick = 0;
        tick = 3;
        rotation = GameWorld.RandomNumber(0, 4);
        blocktype = GameWorld.RandomNumber(0, 7);

    }

    public void HandleInput(InputHelper inputhelper)
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
        if (inputhelper.KeyPressed(Keys.Left, true))
        {
            MoveLeft();
        }
        if (inputhelper.KeyPressed(Keys.Right, true))
        {
            MoveRight();
        }
        if (inputhelper.KeyPressed(Keys.Space, true))
        {
            //pauze;
        }
    }
   
    public int OffsetCorrectorRight()
    {
        int space = 0;
        offsetcorrector = 0;
        for (int x = 3; x > 0; x--)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockposition[x, y])
                {
                    space++;
                }
            }
            if (space > 0)
            {
                if(x == 1)
                {
                    offsetcorrector = 2;
                }
                if(x == 2)
                {
                    offsetcorrector = 1;
                }
                break;
            }
        }
        return offsetcorrector;
    }
    public int OffsetCorrectorLeft()
    {
        int space = 0;
        offsetcorrector = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockposition[x, y])
                {
                    space++;
                }
            }
            if (space > 0)
            {
                offsetcorrector = x;
                break;
            }
        }
        return offsetcorrector;
    }
    public int OffsetCorrectorDown()
    {
        int space = 0;
        offsetcorrector = 0;
        for (int y = 3; y > 0; y--)
        {
            for (int x = 0; x < 4; x++)
            {
                if (blockposition[x, y])
                {
                    space++;
                }
            }
            if (space > 0)
            {
                if(y == 0)
                {
                    offsetcorrector = 3;
                }
                if (y == 1)
                {
                    offsetcorrector = 2;
                }
                if (y == 2)
                {
                    offsetcorrector = 1;
                }
                break;
            }
        }
        return offsetcorrector;
    }
    protected virtual void MoveRight()
    {
        offset.X++;
        if (TetrisGrid.IsValid(this))
        {
            return;
        }
        else offset.X--;
    }
    protected virtual void MoveLeft()
    {
        offset.X--;
        if (TetrisGrid.IsValid(this))
        {
            return;
        }
        else offset.X++;
    }
    
    protected virtual void MoveDown()
    {
        offset.Y++;
    }
    public virtual void Clock(GameTime gameTime)
    {
        nexttick += (int)gameTime.ElapsedGameTime.TotalSeconds;
        if (nexttick == tick)
        {
            nexttick = 0;
            offset.Y++;
        }

    }
    
    protected virtual void RotateRight()
    {
        // Decreases rotation on button press
        previousrotation = rotation;
        if (rotation <= 2)
        {
            rotation++;
        }
        else
            rotation = 0;
        if (TetrisGrid.IsValid(this))
        {
            return;
        }
        else
        {
            rotation = previousrotation;
        }

    }
    protected virtual void RotateLeft()
    {
        // Increases rotation on button press
        previousrotation = rotation;
        if (rotation >= 1)
        {
            rotation--;
        }
        else
        {
            rotation = 3;
        }

        if (TetrisGrid.IsValid(this))
        {
            return;
        }
        else
        {
            rotation = previousrotation;
        }
    }
    public virtual void BlockPosition()
    {
        
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
        // xd
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
    public void Update(GameTime gameTime)
    {
        BlockPosition();
        Clock(gameTime);
    }
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
    public bool[,] BlockGrid
    {
        get { return blockposition; }
    }
    public Point Offset
    {
        get { return offset; }
        set { offset = value; }
    }
}

class BlockI : Block
{
    public BlockI()
    {

    }
    public override void BlockPosition()
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

    public override void BlockPosition()
    {
        ResetBlockPosition();
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
    public BlockL()
    {

    }
    public override void BlockPosition()
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
            blockposition[2, 0] = true;
        }
    }
}
class BlockO : Block
{
    public BlockO()
    {

    }
    public override void BlockPosition()
    {
        ResetBlockPosition();
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
    public override void BlockPosition()
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
    public override void BlockPosition()
    {
        // verkeerde block roteert atm
        ResetBlockPosition();
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
    public BlockZ()
    {

    }
    public override void BlockPosition()
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
