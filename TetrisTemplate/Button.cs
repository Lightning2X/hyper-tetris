using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Button
{
    Texture2D button;
    Vector2 position;
    public Button(Texture2D button, Vector2 position)
    {
        this.button = button;
        this.position = position;
        
    }

    public bool IsClicked(InputHelper inputHelper)
    {
        Rectangle boundingbox = new Rectangle((int)position.X, (int)position.Y, button.Width, button.Height);
        if (boundingbox.Contains(inputHelper.MousePosition))
        {
            return true;
        }
        else
            return false;
    }

    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        s.Draw(button, position, Color.White);
    }
}

