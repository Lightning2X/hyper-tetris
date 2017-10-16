using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using static System.Console;


class TetrisGame : Game
{
    // initialize the graphics device
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    InputHelper inputHelper;
    GameWorld gameWorld;
    HUD hud;
    public static int score = 0;

    [STAThread]
    static void Main(string[] args)
    {
        TetrisGame game = new TetrisGame();
        game.Run();
    }

    public class Variables
    {
        public static int score = 0;
    }

    public TetrisGame()
    {
        graphics = new GraphicsDeviceManager(this);
        // set the directory where game assets are located
        Content.RootDirectory = "Content";
        // set the desired window size
        graphics.PreferredBackBufferWidth = 500;
        graphics.PreferredBackBufferHeight = 600;
        // create the input helper object
        inputHelper = new InputHelper();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // create and reset the game world
        gameWorld = new GameWorld(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, Content);
        gameWorld.Reset();
        hud = new HUD(gameWorld);
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

        gameWorld.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
 
}
