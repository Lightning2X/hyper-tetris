using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class Block
    {
        GameWorld currentgameworld;
        public Block(Texture2D blocksprite, GameWorld currentgameworld)
        {
            this.currentgameworld = currentgameworld;

        }

        protected void BlockEvent(InputHelper inputhelper)
        {
            if (inputhelper.KeyPressedA(Keys.A, false))
            {
                // Linksom draaien
            }

            if (inputhelper.KeyPressedD(Keys.D, false))
            {
                // Rechtsom draaien
            }
            if (inputhelper.KeyPressedS(Keys.S, false))
            {
                // Sneller naar beneden
            }
            if (inputhelper.KeyPressedW(Keys.W, false))
            {
                // 180* draaien
            }
        }

        protected void BlockBuilder()
        {
            currentgameworld.RandomNumber(0, 10);
            Random rblocks = new Random();
            rblocks.Next(0, 10);
            if(rblocks.Equals(1))
            {
           ///     Blockbuild blockL.
            }
            else if(rblocks.Equals(2))
            {
                ///     Blockbuild blockZ.
            }
            else if (rblocks.Equals(3))
            {
                ///     Blockbuild blockS.
            }
            else if (rblocks.Equals(4))
            {
                ///     Blockbuild blockOmgL.
            }
            else if (rblocks.Equals(5))
            {
                ///     Blockbuild block4Kant.
            }
            else if (rblocks.Equals(6))
            {
                ///     Blockbuild blockI.
            }
            else if (rblocks.Equals(7))
            {
                ///     Blockbuild blockBom.
            }
            else if (rblocks.Equals(8))
            {
                ///     Blockbuild blockY.
            }
            else if (rblocks.Equals(9))
            {
                ///     Blockbuild blockR.
            }

        }
    }
    class Blockbuild : Block
    {
        public Blockbuild(Texture2D blocksprite, GameWorld currentgameworld) : base(blocksprite, currentgameworld)
        {

        }
        protected void BlockL()
        {
           // maak block met vorm L
        }
        protected void BlockZ()
        {
            // maak block met vorm Z
        }
        protected void BlockS()
        {
            // maak block met vorm S
        }
        protected void BlockOmgL()
        {
            // maak block met vorm  omgekeerde L
        }
        protected void Block4Kant()
        {
            // maak block met vorm van een vierkant
        }
        protected void BlockI()
        {
            // maak block met vorm I (4 blocken recht aan elkaar)
        }
        protected void BlockBom()
        {
            // maak block met grote 1, die als ie staat gelijk weer verdwijnt en de directe aangesloten blockjes meeneemt (opblaast)
        }
        protected void BlockY()
        {
            // maak block met grote 1, die alle gaten in de rij waarin ie neerkomt vult
        }
        protected void BlockR()
        {
            // maak block met grote 1, die 1 gat in de rij (of de rij er boven of de rij eronder) waarin ie neerkomt vult
        }
    }
}
