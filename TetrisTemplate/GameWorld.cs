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

    Texture2D block, helpmenu, MainMenu, OptionsMenu, OnSprite, OffSprite, blauwsprite, GameOverSprite;
    Vector2 ScorePosition = new Vector2(420, 0);
    Vector2 FSon = new Vector2(250, 70);
    Vector2 Musicoff = new Vector2(250, 260);
    Vector2 blauw1 = new Vector2(380, 0);
    MouseState currentMouseState;
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
        gameState = GameState.Menu;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        OnSprite = Content.Load<Texture2D>("onoptions");
        OffSprite = Content.Load<Texture2D>("offoptions");
        OptionsMenu = Content.Load<Texture2D>("OptionsMenu");
        blauwsprite = Content.Load<Texture2D>("blauw");
        MainMenu = Content.Load<Texture2D>("Mainmenu");
        GameOverSprite = Content.Load<Texture2D>("gameover");
        grid = new TetrisGrid(block);
        blockfunction = new Block();
        currentMouseState = Mouse.GetState();

    }

    public void Reset()
    {

    }
    protected void NextPiece()
    {

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
    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
    }
    public void Update(GameTime gameTime, InputHelper inputhelper)
    {
        if (gameState == GameState.Menu)
        {
           if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >=15 && MousePosition.X <= 365 && MousePosition.Y >= 100 && MousePosition.Y <= 220) // startknop
            {
                gameState = GameState.Playing;
            }
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 15 && MousePosition.X <= 335 && MousePosition.Y >= 375 && MousePosition.Y <= 495) // helpknop
            {
                gameState = GameState.Help;
            }
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 15 && MousePosition.X <= 335 && MousePosition.Y >= 225 && MousePosition.Y <= 345) // optiesnop
            {
                gameState = GameState.Options;
            }
        }
        if (gameState == GameState.Options)
        {
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 250 && MousePosition.X <= 400 && MousePosition.Y >= 70 && MousePosition.Y <= 220) // fullscreenknop
            {   //in veld wordt gedrukt dan uitvoeren
                if (fullscreen1)
                {
                    fullscreen1 = false;
                }
                if (!fullscreen1)
                {
                    fullscreen1 = true;
                }
           }
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 250 && MousePosition.X <= 400 && MousePosition.Y >= 260 && MousePosition.Y <= 410) // songknop
            {   //in veld wordt gedrukt dan uitvoeren
                if (!music)
                {
                    music = true;
                }
                if (music)
                {
                    music = false;
                }
            }
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 20 && MousePosition.X <= 370 && MousePosition.Y >= 430 && MousePosition.Y <= 530) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
        }
        if (gameState == GameState.Help)
        {
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 15 && MousePosition.X <= 325 && MousePosition.Y >= 460 && MousePosition.Y <= 550) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
        }
        Rectangle buttonmainmenufromgameover = new Rectangle(15, 465, 310, 70);

        if (gameState == GameState.GameOver)
        {
            if (inputhelper.MouseLeftButtonPressed() && MousePosition.X >= 15 && MousePosition.X <= 465 && MousePosition.Y >= 310 && MousePosition.Y <= 380) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
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
            string Scorestring = "Score" + TetrisGame.Variables.score.ToString();
            spriteBatch.Draw(blauwsprite, blauw1, Color.White);
            spriteBatch.DrawString(font,Scorestring, ScorePosition, Color.Black);

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
            if(fullscreen1)
            {
                spriteBatch.Draw(OnSprite, FSon, Color.White);
            }
            if (!music)
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