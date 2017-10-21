using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
//using static System.Console;


// A class for representing the game world.

class GameWorld
{

    // enum for different game states (playing or game over
    enum GameState { Menu, Options, Help, Playing, GameOver }

    // makes a rectangle of the screen so that a video can be played as background during gameplay
    Rectangle display;
    int Highscore;
    double timer, timerlimit;

    //random number generator
    static Random random;

    //main game font
    SpriteFont font;


    // sprite for representing a single tetris block element
    Texture2D block, helpmenu, playbutton, optionsbutton, helpbutton;
    Vector2 Musicoff = new Vector2(250, 260);

    // make a boolean for if the music is playing and if a block is placed 
    bool music = false, placed = false;

    // the current game state
    GameState gameState;

    // the main playing grd
    TetrisGrid grid;
    Block newblock, nextblock;
    HUD hud;
    Button Play, Help, Options;
    VideoPlayer videoplayer;
    Video backgroundvideo;

    public GameWorld(ContentManager Content, Point screencoordinates, Point screendimensions)
    {
        backgroundvideo = Content.Load<Video>("space");
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        playbutton = Content.Load<Texture2D>("playbutton");
        optionsbutton = Content.Load<Texture2D>("optionsbutton");
        helpbutton = Content.Load<Texture2D>("helpbutton"); ;

        random = new Random();
        videoplayer = new VideoPlayer();
        hud = new HUD(Content);
        grid = new TetrisGrid();
        Play = new Button(playbutton, new Vector2(10, 130));
        Help = new Button(helpbutton, new Vector2(10, 250));
        Options = new Button(optionsbutton, new Vector2(10, 400));
        newblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        nextblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());

        display = new Rectangle(screencoordinates.X, screencoordinates.Y, screendimensions.X, screendimensions.Y);
        gameState = GameState.Menu;
        timer = 0;
        timerlimit = 1;
        Highscore = 0;
    }

    public void Reset()
    {
        grid.Clear();
        TetrisGame.Score.score = 0;
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (gameState == GameState.Menu)
        {
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (Play.IsClicked(inputHelper))
                {
                    TetrisGame.showmouse = false;
                    gameState = GameState.Playing;
                }
                if (Help.IsClicked(inputHelper))
                {
                    TetrisGame.showmouse = true;
                    gameState = GameState.Help;
                }
                if (Options.IsClicked(inputHelper))
                {
                    TetrisGame.showmouse = true;
                    gameState = GameState.Options;
                }
            }
        }
        if (gameState == GameState.Playing)
        {
            if (newblock != null)
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
                if (inputHelper.KeyPressed(Keys.W, false))
                {
                    // Beweegt het blok meteen naar beneden, gebruikt de override in TrymoveDown om te kijken of het blokje al geplaatst is met behulp van de bool placed;
                    for (int i = 0; i < TetrisGrid.GridHeight; i++)
                    {
                        TryMoveDown(true);
                        if (placed)
                        {
                            placed = false;
                            break;
                        }
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
            }
        }
        if(gameState == GameState.Options)
        {

        }
    }
    public void Update(GameTime gameTime, InputHelper inputhelper)
    {

        if (gameState == GameState.Menu)
        {
            if (inputhelper.KeyPressed(Keys.P, true))// startknop
            {
                Reset();
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
                music = false;//music is off
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
            if (TetrisGame.Score.highscore < TetrisGame.Score.score)
            {
                TetrisGame.Score.highscore = TetrisGame.Score.score;
            }

            if (inputhelper.KeyPressed(Keys.M, true)) // mainmenu knop
            {
                TetrisGame.showmouse = true;
                gameState = GameState.Menu;
            }
        }

        if (gameState == GameState.Playing)
        {
            videoplayer.Play(backgroundvideo);
            grid.Update(gameTime);
            Clock(gameTime);
        }
    }
    private void TryMoveDown(bool instant = false)
    {
        newblock.MoveDown();
        if (!grid.IsValid(newblock))
        {
            newblock.MoveUp();
            grid.PlaceBlock(newblock);
            newblock = nextblock;
            if (!grid.IsValid(newblock))
            {
                gameState = GameState.GameOver;
            }
            nextblock = Block.CreateBlock(RandomNumber(0, 10), 0, Block.RandomiseBlockType());
            if (instant)
            {
                placed = true;
            }
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
            Texture2D videoframe = videoplayer.GetTexture();
            spriteBatch.Draw(videoframe, display, Color.White);
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            hud.MainDraw(gameTime, spriteBatch, nextblock);
        }
        if (gameState == GameState.GameOver)
        {
            hud.GameOverScreen(gameTime, spriteBatch);
        }
        if (gameState == GameState.Help)
        {
            spriteBatch.Draw(helpmenu, Vector2.Zero, Color.White);
        }
        if (gameState == GameState.Options)
        {
            /*
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
            */
        }
        
        if(gameState == GameState.Menu)
        {
            hud.MenuScreen(gameTime, spriteBatch);
            Play.Draw(gameTime, spriteBatch);
            Options.Draw(gameTime, spriteBatch);
            Help.Draw(gameTime, spriteBatch);
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