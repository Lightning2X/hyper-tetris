using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Gameclock
{
    private Block b;
    public Gameclock(Block b)
    {
        this.b = b;
    }
    public void Update(GameTime gameTime)
    {
        float speedincrease = (float)0.01 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        for (int i = 0; i < 30; i++)
        {
            if (i * gameTime.TotalGameTime.Seconds >= 30)
            {
                b.offsety++;
            }
        }
    }
}

