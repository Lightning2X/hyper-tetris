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

    int screenWidth, screenHeight, Highscore;
    double timer, timerlimit;

    //random number generator
    static Random random;

    //main game font

    SpriteFont font;


    // sprite for representing a single tetris block element

    Texture2D block, helpmenu, MainMenu, OptionsMenu, OffSprite, blauwsprite, blauwsprite2, GameOverSprite;
    Vector2 ScorePosition = new Vector2(370, 0);
    Vector2 Gameoverscore = new Vector2(70, 380);
    Vector2 Highscorepositie = new Vector2(70, 420);
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
    Block newblock;


    public GameWorld(int width, int height, ContentManager Content)
    {
        screenWidth = width;
        screenHeight = height;
        random = new Random();
        gameState = GameState.GameOver;
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
        timer = 0;
        timerlimit = 1;
        newblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        Highscore = 0;
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
        if(newblock != null)
        {
            if (inputHelper.KeyPressed(Keys.A, false))
            {
                newblock.RotateLeft();
                if (!grid.IsValid(newblock))
                {
                    newblock.RotateRight();
                }
            }
            if (inputHelper.KeyPressed(Keys.D, false))
            {
                newblock.RotateRight();
                if (!grid.IsValid(newblock))
                {
                    newblock.RotateLeft();
                }
            }
            if (inputHelper.KeyPressed(Keys.S, true))
            {
                TryMoveDown();
            }
            if (inputHelper.KeyPressed(Keys.Left, true))
            {
                newblock.MoveLeft();
                if (!grid.IsValid(newblock))
                {
                    newblock.MoveRight();
                }
            }
            if (inputHelper.KeyPressed(Keys.Right, true))
            {
                newblock.MoveRight();
                if (!grid.IsValid(newblock))
                {
                    newblock.MoveLeft();
                }
            }
            if (inputHelper.KeyPressed(Keys.Space, true))
            {
                //pauze;
            }
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
            }
            if (inputhelper.KeyPressed(Keys.Z, true) && music == false) // songknop
            {
                music = true; // music is on
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
        if (gameState == GameState.GameOver)
        {

            if (inputhelper.KeyPressed(Keys.M, true)) // mainmenu knop
            {
                gameState = GameState.Menu;
            }
        }
        if (gameState == GameState.Playing)
        {
            Clock(gameTime);
        }
    }
    private void TryMoveDown()
    {
        newblock.MoveDown();
        if (!grid.IsValid(newblock))
        {
            newblock.MoveUp();
            grid.PlaceBlock(newblock);
            newblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        }
    }
    public void Clock(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds;
        if (timer >= timerlimit)
        {
            timer = 0;
            TryMoveDown();
        }

    }
//    public int NextWorldBlock
 //   {
 //       get { return .blocktype; }
  //  }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (gameState == GameState.Playing)
        {
            spriteBatch.Draw(blauwsprite, Vector2.Zero, Color.White);// achtergrond weghalen
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            spriteBatch.Draw(blauwsprite2, blauw1, Color.White);
            string Scorestring = "Score: " + TetrisGame.Variables.score.ToString();
            spriteBatch.DrawString(font,Scorestring, ScorePosition, Color.Black);
            spriteBatch.Draw(block, NexpiecePosition, Color.White);
        }
        if (gameState == GameState.GameOver)
        {
            spriteBatch.Draw(GameOverSprite, Vector2.Zero, Color.White);
            string Scorestring = "Score: " + TetrisGame.Variables.score.ToString();
            string HighScorestring = "HighScore: " + Highscore.ToString();
            spriteBatch.DrawString(font, Scorestring, Gameoverscore, Color.Black);
            if( Highscore < TetrisGame.Variables.score )
                {
                Highscore = TetrisGame.Variables.score;
            }
            spriteBatch.DrawString(font, HighScorestring, Highscorepositie, Color.Black);
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
                string Musicoffstring = "Press Z to turn the music off";
                spriteBatch.DrawString(font, Musicoffstring, new Vector2(40,160), Color.Black);
            }
            else // music is on
            {
                string Musiconstring = "Press S to turn the music on";
                spriteBatch.DrawString(font, Musiconstring, new Vector2(40, 160), Color.Black);
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