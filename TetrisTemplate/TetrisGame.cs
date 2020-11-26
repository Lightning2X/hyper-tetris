using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using static System.Console;


class TetrisGame : Game
{
    // initialize the graphics device, declare the spritebatch, inputhelper and gameworld
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    InputHelper inputHelper;
    GameWorld gameWorld;
    // Declare booleans for showing the mousee and exiting the game
    public static bool showmouse = true;
    public static bool exitgame = false;
    // Make a new point that contains the current screendimensions
    private Point screendimensions;

    [STAThread]
    static void Main(string[] args)
    {
        TetrisGame game = new TetrisGame();
        game.Run();
    }

    public TetrisGame()
    {
        graphics = new GraphicsDeviceManager(this);
        // set the directory where game assets are located
        Content.RootDirectory = "Content";
        // set the desired window size
        graphics.PreferredBackBufferWidth = 500;
        graphics.PreferredBackBufferHeight = 600;
        // store the desired window size in the screendimensions variable
        screendimensions = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        // create the input helper object
        inputHelper = new InputHelper();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // create the gameworld with the current X, Y of the screen and it' s dimensions
        gameWorld = new GameWorld(Content, new Point (GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y), screendimensions);
        // Reset the gameworld
        gameWorld.Reset();
    }
    protected override void Update(GameTime gameTime)
    {
        // Update the inputhelp and gameworld objects
        inputHelper.Update(gameTime);
        gameWorld.HandleInput(gameTime, inputHelper);
        gameWorld.Update(gameTime, inputHelper);
        // exit the game if the exitgame boolean is true
        if (exitgame)
        {
            Exit();
        }
    }
    protected override void Draw(GameTime gameTime)
    {
       // Begin drawing the game and depending on the showmouse boolean draw the mouse, then draw the game world.
        spriteBatch.Begin();
        if (showmouse)
        {
            this.IsMouseVisible = true;
        }
        else
        {
            this.IsMouseVisible = false;
        }
        this.IsMouseVisible = true;
        gameWorld.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}

// Global class for keeping the score it is static because there is only one instance of the score
static class Score
{
    public static int currentscore = 0, highscore = 0;
}