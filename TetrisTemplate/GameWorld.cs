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
      
    enum GameState     {         Menu, Options, Help, Playing, GameOver    }
    Vector2 lijnmenu = new Vector2(100, 50);
    Vector2 lijn1 = new Vector2(100, 80);
    Vector2 lijn2 = new Vector2(100, 110);
    Vector2 lijn3 = new Vector2(100, 140);
    Vector2 lijn4 = new Vector2(100, 170);
    Vector2 lijn5 = new Vector2(100, 200);
    Vector2 lijn6 = new Vector2(100, 230);
    Vector2 lijn7 = new Vector2(100, 300);

    // screen width and height

    int screenWidth, screenHeight;

    //random number generator
    static Random random;

    //main game font

    SpriteFont font;

     
      // sprite for representing a single tetris block element
      
    Texture2D block;

     
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
        gameState = GameState.Menu;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid(block);
        blockfunction = new Block();
    }

    public void Reset()
    {
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {

    }

    public void Update(GameTime gameTime)
    {
        if(gameState == GameState.Menu)
        {
            //zet menu! 
        }
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
        blockfunction.NextBlock();
        Block newblock = blockfunction.RandomBlock();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        grid.Draw(gameTime, spriteBatch, block);
        if (gameState == GameState.Options)
        {

        }
            if (gameState == GameState.Help)
        {
   
            DrawText("Help!", Vector2.Zero, spriteBatch);
            DrawText("Press A to turn left", lijn1,spriteBatch);
           // DrawText("press W to rotate 180 degrees", lijn3, spriteBatch);
            DrawText("Press D to turn right", lijn2, spriteBatch);
            DrawText("Press S to move to bottom", lijn4, spriteBatch);
            DrawText("Press left to move left", lijn5, spriteBatch);
            DrawText("Press right to move left", lijn6, spriteBatch);
            DrawText("Press space to return to menu", lijn7, spriteBatch);
        }
        if (gameState == GameState.Menu)
        {
            DrawText("Menu", Vector2.Zero, spriteBatch);
            DrawText("Start", lijn1, spriteBatch);
            DrawText("Opties", lijn3, spriteBatch);
            DrawText("Help en Credits", lijn5, spriteBatch);
        }
        if (gameState == GameState.Playing)
        {
       //     DrawText(Score, Vector2.Zero, spriteBatch);

        }
        if (gameState == GameState.GameOver)
        {
            DrawText("You lose!", lijn1, spriteBatch);

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
