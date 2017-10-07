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
        }

        protected void BlockBuilder()
        {
            currentgameworld.RandomNumber(0, 9);
        }
    }

}
