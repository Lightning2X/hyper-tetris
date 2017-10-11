using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

  // A class for representing the game world.
  
class GameWorld
{
      // enum for different game states (playing or game over)
      
    enum GameState     {         Menu, Options, Playing, GameOver     }
    GameState curgamestate = GameState.Menu;
    
    // screen width and height

    int screenWidth, screenHeight;

    //random number generator
    Random random;

    //main game font

    SpriteFont font;
     
      // sprite for representing a single tetris block element
      
    Texture2D block;
     
      // the current game state
      
    GameState gameState;
     
      // the main playing grid
      
    TetrisGrid grid;
    Vector2 lijn1 = new Vector2(100,100);
    Vector2 lijn2 = new Vector2(100, 150);
    Vector2 lijn3 = new Vector2(100, 200);
    Vector2 lijn4 = new Vector2(100, 250);
    Vector2 lijn5 = new Vector2(100, 300);
    Vector2 lijn6 = new Vector2(100, 350);
    Vector2 lijnmenu = new Vector2(100, 50);

    public GameWorld(int width, int height, ContentManager Content)
    {
        screenWidth = width;
        screenHeight = height;
        random = new Random();
        gameState = GameState.Playing;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid(block);
    }
    public void Reset()
    {

    }
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {

    }
    public void Update(GameTime gameTime)
    {
        if (curgamestate == GameState.Menu)// begin status van het spel
        {
            curgamestate = GameState.Options;
          /*  if (Keyboard.GetState().IsKeyDown(Keys.Space))// na indrukken spatiebalk wordt het spel gestart
            {
                curgamestate = GameState.Playing;
            }
            */
        }
        if(curgamestate == GameState.Playing)
        {

        }
        if(curgamestate == GameState.Options)
        {

        }
        if(curgamestate == GameState.GameOver)
        {

        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(curgamestate == GameState.Menu)
        {
            DrawText("Menu", lijnmenu, spriteBatch);

        }
        if (curgamestate == GameState.Options) {
            DrawText("Options", lijnmenu, spriteBatch);
            DrawText("Press A to turn left", lijn1 , spriteBatch);
            DrawText("Press W to rotate 180 degrees", lijn2, spriteBatch);
            DrawText("Press D to turn right", lijn3, spriteBatch);
            DrawText("Press A to place blocks", lijn4, spriteBatch);
            DrawText("Press left to move the blocks to the left", lijn5, spriteBatch);
            DrawText("Press right to move the blocks to the right", lijn6, spriteBatch);
        }
        if (curgamestate == GameState.Playing)
        {

        }
        if (curgamestate == GameState.GameOver)
        {
            DrawText("You lose!", lijnmenu, spriteBatch);

        }

        grid.Draw(gameTime, spriteBatch, block);
         }
     
      // utility method for drawing text on the screen
      
    public void DrawText(string text, Vector2 positie, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, positie, Color.Blue);
    }
    public int RandomNumber(int a, int b)
    {
        return random.Next(a, b);
    }
    public int LineIsFull()
    {
        int x = 0;
        // code voorals de rij vol is
        return x;
    }
}
