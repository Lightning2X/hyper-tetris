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
    double timer, timerlimit;
    int blocksplaced;
    public static int level;
    //random number generator
    static Random random;

    //main game font
    SpriteFont font;


    // sprite for representing a single tetris block element
    Texture2D block, playbutton, optionsbutton, helpbutton, backbutton, musicbuttonON, musicbuttonOFF, videobuttonON, videobuttonOFF, alternativebackground;
    Song menu, playing, gameover;
    SoundEffect placeblocksound, levelup;
    // Make booleans for the option menu (for turning music on/off or the video background on/off and make a boolean for the W key to check if a block was placed
    bool music = true;
    bool videoison = true;
    bool placed = false;

    // the current game state
    GameState gameState;

    // the main playing grd
    TetrisGrid grid;
    Block newblock, nextblock;
    HUD hud;
    Button Play, Help, Options, Back, Musicbutton, Videobutton;
    VideoPlayer videoplayer;
    Video backgroundvideo;

    public GameWorld(ContentManager Content, Point screencoordinates, Point screendimensions)
    {
        backgroundvideo = Content.Load<Video>("space");
        menu = Content.Load<Song>("menu_music");
        playing = Content.Load<Song>("main_highlevel");
        placeblocksound = Content.Load<SoundEffect>("placeblock");
        levelup = Content.Load<SoundEffect>("levelchime");
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        playbutton = Content.Load<Texture2D>("playbutton");
        optionsbutton = Content.Load<Texture2D>("optionsbutton");
        helpbutton = Content.Load<Texture2D>("helpbutton");
        backbutton = Content.Load<Texture2D>("gobackbutton");
        musicbuttonON = Content.Load<Texture2D>("musicbutton_ON");
        videobuttonON = Content.Load<Texture2D>("videobutton_ON");
        musicbuttonOFF = Content.Load<Texture2D>("musicbutton_OFF");
        videobuttonOFF = Content.Load<Texture2D>("videobutton_OFF");
        alternativebackground = Content.Load<Texture2D>("spacebackground");

        random = new Random();
        videoplayer = new VideoPlayer();
        hud = new HUD(Content);
        grid = new TetrisGrid();
        Play = new Button(playbutton, new Vector2(10, 130));
        Help = new Button(helpbutton, new Vector2(10, 250));
        Options = new Button(optionsbutton, new Vector2(10, 400));
        Back = new Button(backbutton, new Vector2(160, 500));
        Musicbutton = new Button(musicbuttonON, new Vector2(155, 120));
        Videobutton = new Button(videobuttonON, new Vector2(155, 250));

        newblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        nextblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());

        display = new Rectangle(screencoordinates.X, screencoordinates.Y, screendimensions.X, screendimensions.Y);
        gameState = GameState.Menu;
        timer = 0;
        level = 0;
        timerlimit = 1.5;
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.5f;
        MediaPlayer.Play(menu);
    }

    public void Reset()
    {
        grid.Clear();
        blocksplaced = 0;
        TetrisGame.Score.score = 0;
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (gameState == GameState.Menu)
        {
            if (music)
            {
                MediaPlayer.Play(menu);
            }

            if (inputHelper.MouseLeftButtonPressed())
            {
                if (Play.IsClicked(inputHelper))
                {
                    MediaPlayer.Stop();
                    TetrisGame.showmouse = false;
                    Reset();

                    if (music)
                    {
                        MediaPlayer.Play(playing);
                        Console.WriteLine(MediaPlayer.State);
                    }

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
            // options to turn music and the video background off
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (Back.IsClicked(inputHelper))
                {
                    gameState = GameState.Menu;
                }

                if (Musicbutton.IsClicked(inputHelper))
                {
                    music = !music;
                }

                if (Videobutton.IsClicked(inputHelper))
                {
                    videoison = !videoison;
                }
            }
        }
        if(gameState == GameState.Help || gameState == GameState.GameOver)
        {
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (Back.IsClicked(inputHelper))
                {
                    gameState = GameState.Menu;
                }
            }
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

            if (videoison)
            {
                videoplayer.Play(backgroundvideo);
            }
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

            placeblocksound.Play();
            blocksplaced++;
            nextblock = Block.CreateBlock(RandomNumber(0, 10), 0, Block.RandomiseBlockType());

            if (blocksplaced % 10 == 0)
            {
                levelup.Play();
                level = blocksplaced / 10;
            }

            if (instant)
            {
                placed = true;
            }
        }
    }
    public void Clock(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds;
        if (timer >= (timerlimit - level / 5))
        {
            timer = 0;
            TryMoveDown();
        }

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (gameState == GameState.Playing)
        { 
            if (videoison)
            {
                Texture2D videoframe = videoplayer.GetTexture();
                spriteBatch.Draw(videoframe, display, Color.White);
            }
            else
            {
                spriteBatch.Draw(alternativebackground, Vector2.Zero, Color.White);
            }
            
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            hud.MainDraw(gameTime, spriteBatch, nextblock);
        }
        if (gameState == GameState.GameOver)
        {
            hud.GameOverScreen(gameTime, spriteBatch);
            Back.Draw(gameTime, spriteBatch);
        }
        if (gameState == GameState.Help)
        {
            hud.Help(gameTime, spriteBatch);
            Back.Draw(gameTime, spriteBatch);
        }
        if (gameState == GameState.Options)
        {
            hud.Options(gameTime, spriteBatch);
            if (videoison)
            {
                Videobutton = new Button(videobuttonON, new Vector2(155, 250));
                Videobutton.Draw(gameTime, spriteBatch);
            }
            else
            {     
                Videobutton = new Button(videobuttonOFF, new Vector2(155, 250));
                Videobutton.Draw(gameTime, spriteBatch);
            }

            if (music)
            {
                Musicbutton = new Button(musicbuttonON, new Vector2(155, 130));
                Musicbutton.Draw(gameTime, spriteBatch);
            }
            else
            {
                Musicbutton = new Button(musicbuttonOFF, new Vector2(155, 130));
                Musicbutton.Draw(gameTime, spriteBatch);
            }

            Back.Draw(gameTime, spriteBatch);
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