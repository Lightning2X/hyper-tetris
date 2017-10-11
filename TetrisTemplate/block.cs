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

    public static Block RandomBlock()
    {
        int r = GameWorld.RandomNumber(0, 8);
        if (r == 0)
            //     Bouw blok I (streep)
            return new BlockI();
        else if (r.Equals(2))
        {
            //    Bouw blok J
            return new BlockJ();
        }
        else if (r.Equals(3))
        {
            //     Bouw blok L
            return new BlockL();
        }
        else if (r.Equals(4))
        {
            //    Bouw blok O (vierkant)
            return new BlockO();
        }
        else if (r.Equals(5))
        {
            //  Bouw blok S
            return new BlockS();
        }
        else if (r.Equals(6))
        {
            // Bouw blok T
            return new BlockT();

        }
        else if (r.Equals(7))
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