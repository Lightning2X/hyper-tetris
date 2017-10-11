using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Block
{
    public int blocktype;
    private int rblocks;
    public Block()
    {

    }

    protected void BlockEvent(InputHelper inputhelper)
    {
        if (inputhelper.KeyPressed(Keys.A))
        {
            // Linksom draaien
        }
        if (inputhelper.KeyPressed(Keys.D, false))
        {
            // Rechtsom draaien
        }
        if (inputhelper.KeyPressed(Keys.S, false))
        {
            // Sneller naar beneden
        }
        if (inputhelper.KeyPressed(Keys.W, false))
        {
            // 180* draaien
        }
        if (inputhelper.KeyPressed(Keys.Left, false))
        {
            // 1 naar links op grid
        }
        if (inputhelper.KeyPressed(Keys.Right, false))
        {
            // 1 naar rechts op grid
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
}

class BlockI : Block
{

}
class BlockJ : Block
{
    
}

class BlockL : Block
{

}
class BlockO : Block
{

}
class BlockS : Block
{

}

class BlockT : Block
{

}

class BlockZ : Block
{

}