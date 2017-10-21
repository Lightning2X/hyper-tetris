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

    // enum for different game states (playing or game over)

    enum GameState { Menu, Options, Help, Playing, GameOver }

    // makes a rectangle of the screen so that a video can be played as background during gameplay
    Rectangle display, HelpButton, StartButton, OptionsButton, FromGameOverToMain, FromOptionsToMain, FromHelpToMain, VidoeOff, MusicOff;
    int Highscore;
    double timer, timerlimit;

    //random number generator
    static Random random;

    //main game font

    SpriteFont font;


    // sprite for representing a single tetris block element

    Texture2D block, helpmenu, MainMenu, OptionsMenu, OffSprite, blauwsprite, GameOverSprite;
    Vector2 Gameoverscore = new Vector2(70, 380);
    Vector2 Highscorepositie = new Vector2(70, 420);
    Vector2 Videooff = new Vector2(255, 124);
    Vector2 Musicoff = new Vector2(255, 289);

    // make a boolean for if the music is playing and if a block is placed 
    bool music = false, placed = false, video = false;

    // the current game state

    GameState gameState;
    // the main playing grd
    TetrisGrid grid;
    Block newblock, nextblock;
    HUD hud;
    VideoPlayer videoplayer;
    Video backgroundvideo;
    Song BackGroundMusic;

    public GameWorld(ContentManager Content, Point screencoordinates, Point screendimensions)
    {
        random = new Random();
        display = new Rectangle(screencoordinates.X, screencoordinates.Y, screendimensions.X, screendimensions.Y);
        gameState = GameState.Playing;
        videoplayer = new VideoPlayer();
        newblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        nextblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        hud = new HUD(Content);
        StartButton = new Rectangle(15, 90, 380, 80);
        HelpButton = new Rectangle(15, 90, 380, 80);
        OptionsButton = new Rectangle(15, 90, 380, 80);
        FromOptionsToMain = new Rectangle(13, 439, 300, 50);
        FromHelpToMain = new Rectangle(9, 467, 300, 50);
        FromGameOverToMain = new Rectangle(7, 450, 280, 70);
        VidoeOff = new Rectangle(255, 124, 150, 150);
        MusicOff = new Rectangle(255, 289, 150, 150);

        backgroundvideo = Content.Load<Video>("space");
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        OffSprite = Content.Load<Texture2D>("offoptions");
        OptionsMenu = Content.Load<Texture2D>("OptionsMenu");
        blauwsprite = Content.Load<Texture2D>("blauw");
        MainMenu = Content.Load<Texture2D>("Mainmenu");
        GameOverSprite = Content.Load<Texture2D>("gameover");
        grid = new TetrisGrid();
        timer = 0;
        timerlimit = 1;
        Highscore = 0;
    //    BackGroundMusic = Content.Load<Song>("Background_music");
    }

    public void Reset()
    {
        grid.Clear();
        TetrisGame.score = 0;
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
            if (inputHelper.KeyPressed(Keys.W, false))
            {
                // Beweegt het blok meteen naar beneden, gebruikt de override in TrymoveDown om te kijken of het blokje al geplaatst is met behulp van de bool placed;
                for(int i = 0; i < TetrisGrid.GridHeight; i++)
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
    public void Update(GameTime gameTime, InputHelper inputhelper)
    {
        if (inputhelper.MouseLeftButtonPressed() && StartButton.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// startknop
        {
            Reset();
            gameState = GameState.Playing;
        }
        if (inputhelper.MouseLeftButtonPressed() && HelpButton.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// helpknop
        {
            gameState = GameState.Help;
        }
        if (inputhelper.MouseLeftButtonPressed() && OptionsButton.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// optionsknop
        {
            gameState = GameState.Options;
        }
    
        if (gameState == GameState.Options)
        {
            if(inputhelper.MouseLeftButtonPressed() && MusicOff.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// musicknop
            {
                music = false;// music is off
            }
            if (inputhelper.MouseLeftButtonPressed() && MusicOff.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// musicknop
            {
                music = true; // music is on
            }
            if (inputhelper.MouseLeftButtonPressed() && VidoeOff.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// videoknop
            {
                video = false;// video is off
            }
            if (inputhelper.MouseLeftButtonPressed() && VidoeOff.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// videoknop
            {
                video = true; // video is on
            }
            if (inputhelper.MouseLeftButtonPressed() && FromOptionsToMain.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// Main menuknop
            {
                gameState = GameState.Menu;
            }
        }
        if (gameState == GameState.Help)
        {
            if (inputhelper.MouseLeftButtonPressed() && FromHelpToMain.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// Main menuknop
            {
                gameState = GameState.Menu;
            }
        }
        if (gameState == GameState.GameOver)
        {
            if (inputhelper.MouseLeftButtonPressed() && FromGameOverToMain.Contains(inputhelper.MousePosition.X, inputhelper.MousePosition.Y))// startknop
            {
                gameState = GameState.Menu;
            }
        }

        if (gameState == GameState.Playing)
        {
            if (video)
            {
                videoplayer.Play(backgroundvideo);
            }
            grid.Update(gameTime);
            Clock(gameTime);
            if(music)
            {
             //BackGroundMusic.Play();
            }
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
            if (video)
            {
                Texture2D videoframe = videoplayer.GetTexture();
                spriteBatch.Draw(videoframe, display, Color.White);
            }
            else
            {
                spriteBatch.Draw(blauwsprite, Vector2.Zero, Color.White);
            }
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            hud.MainDraw(gameTime, spriteBatch, nextblock);
        }
        if (gameState == GameState.GameOver)
        {
            spriteBatch.Draw(GameOverSprite, Vector2.Zero, Color.White);
            string Scorestring = "Score: " + TetrisGame.score.ToString();
            string HighScorestring = "HighScore: " + Highscore.ToString();
            spriteBatch.DrawString(font, Scorestring, Gameoverscore, Color.Black);
            if( Highscore < TetrisGame.score )
                {
                Highscore = TetrisGame.score;
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
            //    string Musicoffstring = "Press Z to turn the music off";
            //    spriteBatch.DrawString(font, Musicoffstring, new Vector2(40,160), Color.Black);
            }
            
            if(!video)// video is off
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