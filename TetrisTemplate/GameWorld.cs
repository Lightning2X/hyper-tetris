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
      
    enum GameState     {         Menu, Options, Help, Playing, GameOver
    }
     
      // screen width and height
      
    int screenWidth, screenHeight;

    //random number generator
    static Random random;

    //main game font

    SpriteFont font;

     
      // sprite for representing a single tetris block element
      
    Texture2D block, helpmenu;

     
      // the current game state
      
    GameState gameState;
      // the main playing grid
      
    TetrisGrid grid;
    Block blockfunction;
   

    public GameWorld(int width, int height, ContentManager Content)
    {
        screenWidth = width;
        screenHeight = height;
        random = new Random();
        gameState = GameState.Help;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        grid = new TetrisGrid(block);
        blockfunction = new Block(grid);
    }

    public void Reset()
    {
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {

    }

    public void Gameclock()
    {

    }

    public void Update(GameTime gameTime)
    {
        if (gameState == GameState.Options)
        {

        }
        if (gameState == GameState.Help)
        {

        }
        if (gameState == GameState.GameOver)
        {

        }
        if (gameState == GameState.Playing)
        {

        }
        Block newblock = blockfunction.RandomBlock();
        blockfunction.NextBlock();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        grid.Draw(gameTime, spriteBatch, block);
        if(gameState == GameState.GameOver)
        {

        }
        if (gameState == GameState.Help)
        {
            spriteBatch.Draw(helpmenu, Vector2.Zero, Color.White);
        }
    }
     
      // utility method for drawing text on the screen
      
    public void DrawText(string text, Vector2 positie, SpriteBatch spriteBatch)
    {

        spriteBatch.DrawString(font, text, positie, Color.Blue);
    }
    static public int RandomNumber(int a, int b)
    {
        return random.Next(a, b);
    }
}
