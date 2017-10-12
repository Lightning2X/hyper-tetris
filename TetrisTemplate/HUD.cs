using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class HUD
{
    private GameWorld currentgameworld;
    public HUD(GameWorld currentgameworld)
    {
        this.currentgameworld = currentgameworld;
    }
    protected /*public mogelijk */ int Score()
    {
        int score = 0;
        if(/* new block neer gezet*/true)
            {
            score += 10;
            }
        if(/* lineisfull*/ true)
        {
            score += 100;

        }
        if(/* reset*/ true)
        {
            score = 0;
        }
        return score;
    }
    public NextPiece()
    {

        return Block.Blocktype;
    }
    protected void Menu()
    {

    }
    protected void Additionaleffects()
    {

    }
}