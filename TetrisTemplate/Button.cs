using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Button
{
    // Define a button texture and a button position
    Texture2D button;
    Vector2 position;
    public Button(Texture2D button, Vector2 position)
    {
        // assign the given button position and texture to the local variables.
        this.button = button;
        this.position = position;
    }

    // Checks if the mouse coördinates are inside the current button
    public bool IsClicked(InputHelper inputHelper)
    {
        // make a rectangle called "boundingbox" around the current button
        Rectangle boundingbox = new Rectangle((int)position.X, (int)position.Y, button.Width, button.Height);
        // checks if the mouse position is inside this box
        if (boundingbox.Contains(inputHelper.MousePosition))
        {
            return true;
        }
        else
            return false;
    }

    // Draws the button
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        s.Draw(button, position, Color.White);
    }
}

