using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using static System.Console;


class TetrisGame : Game
{
    // initialize the graphics device
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    InputHelper inputHelper;
    GameWorld gameWorld;
    public static int score = 0;
    public static int highscore = 0;
    public static bool showmouse = true;
    private Point screendimensions;

    [STAThread]
    static void Main(string[] args)
    {
        TetrisGame game = new TetrisGame();
        game.Run();
    }

    public class Score
    {
        public static int score = 0;
        public static int highscore = 0;
    }

    public TetrisGame()
    {
        graphics = new GraphicsDeviceManager(this);
        // set the directory where game assets are located
        Content.RootDirectory = "Content";
        // set the desired window size
        graphics.PreferredBackBufferWidth = 500;
        graphics.PreferredBackBufferHeight = 600;
        screendimensions = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        // create the input helper object
        inputHelper = new InputHelper();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // create and reset the game world
        gameWorld = new GameWorld(Content, new Point (GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y), screendimensions);
        gameWorld.Reset();
    }
    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update(gameTime);
        gameWorld.HandleInput(gameTime, inputHelper);
        gameWorld.Update(gameTime, inputHelper);
    }
    protected override void Draw(GameTime gameTime)
    {
       // GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        this.IsMouseVisible = true;
        gameWorld.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}
