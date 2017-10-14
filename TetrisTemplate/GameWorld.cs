using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Console;


// A class for representing the game world.

class GameWorld
{

    // enum for different game states (playing or game over)

    enum GameState { Menu, Options, Help, Playing, GameOver }

    // screen width and height

    int screenWidth, screenHeight;

    //random number generator
    static Random random;

    //main game font

    SpriteFont font;


    // sprite for representing a single tetris block element

    Texture2D block, helpmenu, OptionsMenu, OnSprite, OffSprite;
    Vector2 ScorePosition = new Vector2(360, 0);
    Vector2 FSon = new Vector2(250, 70);
    Vector2 Musicoff = new Vector2(250, 260);
    bool fullscreen1 = true;
    bool music = false;
    // the current game state

    GameState gameState;
    // the main playing grid

    TetrisGrid grid;
    Block blockfunction, newblock;


    public GameWorld(int width, int height, ContentManager Content)
    {
        screenWidth = width;
        screenHeight = height;
        random = new Random();
        gameState = GameState.Options;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        OnSprite = Content.Load<Texture2D>("onoptions");
        OffSprite = Content.Load<Texture2D>("offoptions");
        OptionsMenu = Content.Load<Texture2D>("OptionsMenu");
        grid = new TetrisGrid(block);
        blockfunction = new Block();

    }

    public void Reset()
    {

    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (newblock != null)
        {
            newblock.HandleInput(inputHelper);
        }
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
            // TODO add check for if there is already a block
            if (!(grid.IsThereABlock))
            {
                newblock = blockfunction.RandomBlock();
                grid.IsThereABlock = true;
            }
            newblock.Update(gameTime);
            blockfunction.NextBlock();
        }
    }

    public int NextWorldBlock
    {
        get { return blockfunction.blocktype; }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (gameState == GameState.Playing)
        {
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            string Scorestring = TetrisGame.Variables.score.ToString();
            spriteBatch.DrawString(font,Scorestring, ScorePosition, Color.Black);
        }

        if (gameState == GameState.GameOver)
        {

        }
        if (gameState == GameState.Help)
        {
            spriteBatch.Draw(helpmenu, Vector2.Zero, Color.White);
        }
        if (gameState == GameState.Options)
        {
            spriteBatch.Draw(OptionsMenu, Vector2.Zero, Color.White);
            if(fullscreen1)
            {
                spriteBatch.Draw(OnSprite, FSon, Color.White);
            }
            if (!music)
            {
                spriteBatch.Draw(OffSprite, Musicoff, Color.White);
            }
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