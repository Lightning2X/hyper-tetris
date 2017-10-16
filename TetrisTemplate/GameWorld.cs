using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
//using static System.Console;


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

    Texture2D block, helpmenu, MainMenu, OptionsMenu, OffSprite, blauwsprite, blauwsprite2, GameOverSprite;
    Vector2 ScorePosition = new Vector2(370, 0);
    Vector2 FSon = new Vector2(250, 70);
    Vector2 Musicoff = new Vector2(250, 260);
    Vector2 blauw1 = new Vector2(360, 0);
    Vector2 NexpiecePosition = new Vector2(410, 120);
    //MouseState currentMouseState;
    //bool fullscreen1 = true;
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
        gameState = GameState.Menu;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        OffSprite = Content.Load<Texture2D>("offoptions");
        OptionsMenu = Content.Load<Texture2D>("OptionsMenu");
        blauwsprite = Content.Load<Texture2D>("blauw");
        blauwsprite2 = Content.Load<Texture2D>("blauw2");
        MainMenu = Content.Load<Texture2D>("Mainmenu");
        GameOverSprite = Content.Load<Texture2D>("gameover");
        grid = new TetrisGrid(block);
        blockfunction = new Block();
     }

    public void Reset()
    {

    }
    protected void NextPiece()
    {
        TetrisGame.Variables.score += 10;
    }
    protected void Additionaleffects()
    {

    }
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (newblock != null)
        {
            newblock.HandleInput(inputHelper);
        }
    }
    public void Update(GameTime gameTime, InputHelper inputhelper)
    {
        if (gameState == GameState.Menu)
        {
            if (inputhelper.KeyPressed(Keys.P, true))// startknop
            { 
                gameState = GameState.Playing;
            }
            if (inputhelper.KeyPressed(Keys.H, true)) // helpknop
            {
                gameState = GameState.Help;
            }
            if (inputhelper.KeyPressed(Keys.O, true)) // optiesnop
            {
                gameState = GameState.Options;
            }
        }
        if (gameState == GameState.Options)
        {
            if (inputhelper.KeyPressed(Keys.S, true) && music == true) // songknop
            {
                music = false;// music is off
                inputhelper.KeyPressed(Keys.S, false);
            }
            if (inputhelper.KeyPressed(Keys.S, true) && music == false) // songknop
            {
                music = true; // music is on
                inputhelper.KeyPressed(Keys.S, false);
            }
            if (inputhelper.KeyPressed(Keys.M, true)) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
        }
        if (gameState == GameState.Help)
        {
            if (inputhelper.KeyPressed(Keys.M, true)) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
        }
        //MouseLeftButtonPressed() && MousePosition.X >= 15 && MousePosition.X <= 325 && MousePosition.Y >= 460 && MousePosition.Y <= 550) // mainmenu knop

        if (gameState == GameState.GameOver)
        {

            if (inputhelper.KeyPressed(Keys.M, true)) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
        }
        //MouseLeftButtonPressed() && MousePosition.X >= 15 && MousePosition.X <= 465 && MousePosition.Y >= 310 && MousePosition.Y <= 380) // mainmenu knop
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
            spriteBatch.Draw(blauwsprite, Vector2.Zero, Color.White);// achtergrond weghalen
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            string Scorestring = "Score: " + TetrisGame.Variables.score.ToString();
            spriteBatch.Draw(blauwsprite2, blauw1, Color.White);
            spriteBatch.DrawString(font,Scorestring, ScorePosition, Color.Black);
            spriteBatch.Draw(block, NexpiecePosition, Color.White);
        }
        if (gameState == GameState.GameOver)
        {
            spriteBatch.Draw(GameOverSprite, Vector2.Zero, Color.White);
        }
        if (gameState == GameState.Help)
        {
            spriteBatch.Draw(helpmenu, Vector2.Zero, Color.White);
        }
        if (gameState == GameState.Options)
        {
            spriteBatch.Draw(OptionsMenu, Vector2.Zero, Color.White);
            if (!music)// music is off
            {
                spriteBatch.Draw(OffSprite, Musicoff, Color.White);
            }
        }
        if(gameState == GameState.Menu)
        {
            spriteBatch.Draw(MainMenu, Vector2.Zero, Color.White);
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