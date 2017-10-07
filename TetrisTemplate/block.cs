﻿using System;
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
            if (inputhelper.KeyPressed(Keys.A, false))
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
        }

        protected void BlockBuilder()
        {
            currentgameworld.RandomNumber(0, 9);
           /// if(currentgameworld.RandomNumber.Equals(1))
           /// {
           ///     Blockbuild.
           /// }

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
