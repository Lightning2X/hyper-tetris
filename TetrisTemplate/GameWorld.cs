using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

// A class for representing the game world.

class GameWorld
{

    // enum for different game states
    enum GameState { Menu, Loading, Options, Help, Playing, GameOver }

    // makes a rectangle of the screen so that a video can be played as background during gameplay
    Rectangle display;
    // Variables for the timerlimit, the amount of blocks placed (used to increase level) and a static int for the level.
    double timerlimit;
    int blocksplaced;
    private static int level;

    //Random number generator
    static Random random;

    //main game font
    SpriteFont font;


    // Declarations for game assets
    Texture2D block, playbutton, optionsbutton, helpbutton, backbutton, musicbuttonON, musicbuttonOFF, videobuttonON, videobuttonOFF, alternativebackground;
    Song menu, playing, gameover;
    SoundEffect placeblocksound, levelup, gameoversound;
    Video backgroundvideo;
    // Make booleans for the option menu (for turning music on/off or the video background on/off and make a boolean for the W key to check
    // if a block was placed
    bool music = true;
    bool videoison = true;
    bool placed = false;

    // Declare a variable with type enum gamestate for changing between these game states later on
    GameState gameState;

    // the main playing grid
    TetrisGrid grid;
    // Declare the block newblock (the current block being manipulated by the player in the field) and nextblock (the block represented
    // on the HUD and which the newblock will change to when placed)
    Block newblock, nextblock;
    // Declare the HUD (heads-up-display)
    HUD hud;
    // Declare Buttons for the various screens throughout the gamestates
    Button Play, Help, Options, Back, Musicbutton, Videobutton;
    // Declare a videoplayer for the background video
    VideoPlayer videoplayer;

    public GameWorld(ContentManager Content, Point screencoordinates, Point screendimensions)
    {
        // Load game assets
        backgroundvideo = Content.Load<Video>("space");
        menu = Content.Load<Song>("menu_music");
        playing = Content.Load<Song>("main_highlevel");
        placeblocksound = Content.Load<SoundEffect>("placeblock");
        levelup = Content.Load<SoundEffect>("levelchime");
        gameoversound = Content.Load<SoundEffect>("game_over_snd");
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

        // Declare member variables of new instances of classes
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
        // put initial values in both blocks
        newblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        nextblock = Block.CreateBlock(0, 0, Block.RandomiseBlockType());
        // Declare the display as a rectangle representing the current screen
        display = new Rectangle(screencoordinates.X, screencoordinates.Y, screendimensions.X, screendimensions.Y);
        // Set the initial gamestate 
        gameState = GameState.Loading;
        // Set initial values for a few member variables and put the Mediaplayer on repeat (so the background music doesn't stop after 1 loop)
        level = 0;
        timerlimit = 1.5;
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.5f;
    }

    // Reset method which is called after a new game is started
    public void Reset()
    {
        // clear the grid and put the blocksplaced and score to 0
        grid.Clear();
        blocksplaced = 0;
        Score.currentscore = 0;
    }

    // InputHandler for the various game states
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (gameState == GameState.Menu)
        {
            // Check if the mousebutton is pressed, and if so if it was inside one of the boundingboxes of the buttons
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (Play.IsClicked(inputHelper))
                {
                    // stop the Mediaplayer (so that the game song can be played) and make the mouse invisible
                    MediaPlayer.Stop();
                    TetrisGame.showmouse = false;
                    // Reset the game
                    Reset();

                    // Only play music in the main game if the user has it set to true in the options menu
                    if (music)
                    {
                        MediaPlayer.Play(playing);
                        Console.WriteLine(MediaPlayer.State);
                    }
                    gameState = GameState.Playing;
                }
                // Switch to other menus depending on which button was clicked
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
                if (Back.IsClicked(inputHelper))
                {
                    // set a boolean in the main game clas to true so that it exits the game
                    TetrisGame.exitgame = true;
                }
            }
        }
        if (gameState == GameState.Playing)
        {
            // Check if the current block even exists (this will never be the case but it is a failsafe)
            if (newblock != null)
            {
                // Rotate the block left, check if its new position is valid, and if not rotate it back to it's previous position
                if (inputHelper.KeyPressed(Keys.A, false))
                {
                    newblock.RotateLeft();
                    if (!grid.IsValid(newblock))
                    {
                        newblock.RotateRight();
                    }
                }
                // Rotate the block right, check if its new position is valid, and if not rotate it back to it's previous position
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
                    // Moves the current block down instantly, uses an optional boolean inside the TryMoveDown method to check if the block was placed
                    // (otherwise the next block would go down a bit because the loop didn't exit yet)
                    for (int i = 0; i < TetrisGrid.GridHeight; i++)
                    {
                        TryMoveDown(true);
                        if (placed)
                        {
                            // break the loop if Trymovedown communicates that it placed a block
                            placed = false;
                            break;
                        }
                    }
                }

                // Try to move the block down (same kind of strategy as the rotate method, but a seperate method handles all 3 commands plus
                // a few additional extras)
                if (inputHelper.KeyPressed(Keys.S, true))
                {
                    TryMoveDown();
                }

                // Same as with the rotate method, try to move left and if it is not valid do the reverse operation
                if (inputHelper.KeyPressed(Keys.Left, true))
                {
                    newblock.MoveLeft();
                    if (!grid.IsValid(newblock))
                    {
                        newblock.MoveRight();
                    }
                }
                // Now the same but with moving right
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
            // options menu to turn music and the video background off
            if (inputHelper.MouseLeftButtonPressed())
            {
                // Go back to the main menu if the back button is clicked
                if (Back.IsClicked(inputHelper))
                {
                    gameState = GameState.Loading;
                }
                // if the music button is clicked, change to boolean to its reverse (true to false or false to true) and stop the music
                // currently playing if it is set to false
                if (Musicbutton.IsClicked(inputHelper))
                {
                    music = !music;
                    if (!music)
                    {
                        MediaPlayer.Stop();
                    }
                }
                // Change the boolean for the videobackground if the button is clicked
                if (Videobutton.IsClicked(inputHelper))
                {
                    videoison = !videoison;
                }
            }
        }
        // Check if the player has pressed the back button in the help or gameover screen (it is the same button in the same position)
        if(gameState == GameState.Help || gameState == GameState.GameOver)
        {
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (Back.IsClicked(inputHelper))
                {
                    gameState = GameState.Loading;
                }
            }
        }
    }
    public void Update(GameTime gameTime, InputHelper inputhelper)
    {
        // loading state that initializes and plays the main menu music if the music is set to true
        if (gameState == GameState.Loading)
        {
            if (music)
            {
                MediaPlayer.Play(menu);
            }
            gameState = GameState.Menu;
        }
        // if the game is over display the score and highscore, if the score is higher than the current highscore, it is saved in the
        // highscore variable
        if (gameState == GameState.GameOver)
        {
            if (Score.highscore < Score.currentscore)
            {
                Score.highscore = Score.currentscore;
            }
        }

        if (gameState == GameState.Playing)
        {
            // if the video boolean (controlled in the options menu) is true then play the background video
            if (videoison)
            {
                videoplayer.Play(backgroundvideo);
            }
            //update the grid and run the game clock (for moving blocks down 1 space after a certain amount of time)
            grid.Update(gameTime);
            Clock(gameTime);
        }
    }
    // TryMoveDown is a method that first tries to move the current block down, and if it is not valid that means that there is an obstacle
    // (which means because it is the down direction that the block needs to be placed) after this it places the block and turns the
    // nextblock which is displayed in the HUD to the new block that will fall down, it then checks if this block is still not valid,
    // and if this is the case that means that the previous block is blocking it' s path and thus the player is gmae over. if this is not
    // the case it generates  the next block. it also has a boolean variable for the W key press 
    private void TryMoveDown(bool instant = false)
    {
        newblock.MoveDown();
        if (!grid.IsValid(newblock))
        {
            // move the block back up and place it
            newblock.MoveUp();
            grid.PlaceBlock(newblock);
            // make the HUD nextblock the current block that will fall down
            newblock = nextblock;

            if (!grid.IsValid(newblock))
            {
                // if a block is blocked as it spawns, that means that the player is game over, this is handled down here/
                MediaPlayer.Stop();
                gameoversound.Play();
                gameState = GameState.GameOver;
            }
            // play a sound for placing a block and increase the blocksplaced variable
            placeblocksound.Play();
            blocksplaced++;
            // generate a next block
            nextblock = Block.CreateBlock(RandomNumber(0, 10), 0, Block.RandomiseBlockType());
            // increase the level is blocksplaced is divisible by 10, (so the level will go up per 10 blocks)
            if (blocksplaced % 10 == 0)
            {
                levelup.Play();
                level = blocksplaced / 10;
            }

            if (instant)
            {
                // if the optional boolean was specified as true, return true once the block is placed
                placed = true;
            }
        }
    }
    // Timer method that moves blocks down at the speed of the timerlimit - 1/5th the level variable (so as the level increases, so does
    // the speed at which the block moves down)
    public void Clock(GameTime gameTime)
    {
        double timer = 0;
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
            // if the video is supposed to be on (the user hasn't set it to false in the options menu) play the video, otherwise use
            // an alternative background
            if (videoison)
            {
                Texture2D videoframe = videoplayer.GetTexture();
                spriteBatch.Draw(videoframe, display, Color.White);
            }
            else
            {
                spriteBatch.Draw(alternativebackground, Vector2.Zero, Color.White);
            }
            // Draw the main playing grid, the HUD and the current block
            grid.Draw(gameTime, spriteBatch, block);
            newblock.Draw(gameTime, spriteBatch, block);
            hud.MainDraw(gameTime, spriteBatch, nextblock);
        }
        if (gameState == GameState.GameOver)
        {
            // if the game is over draw the HUD's gameover screen and the back button
            hud.GameOverScreen(gameTime, spriteBatch);
            Back.Draw(gameTime, spriteBatch);
        }
        if (gameState == GameState.Help)
        {
            // draw the help method and the back button
            hud.Help(gameTime, spriteBatch);
            Back.Draw(gameTime, spriteBatch);
        }
        if (gameState == GameState.Options)
        {
            // draw the background for the options menu
            hud.Options(gameTime, spriteBatch);
            // draw a button for the videoison button, which will be green if the video is on and red if it isn' t
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
            // same for music, green if on, red if off.
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
            // Draw the back button
            Back.Draw(gameTime, spriteBatch);
        }
        
        if(gameState == GameState.Menu)
        {
            // Draw the menu and the menu buttons
            hud.MenuScreen(gameTime, spriteBatch);
            Play.Draw(gameTime, spriteBatch);
            Options.Draw(gameTime, spriteBatch);
            Help.Draw(gameTime, spriteBatch);
            Back.Draw(gameTime, spriteBatch);
        }
    }
    // Random number generator for the game
    static public int RandomNumber(int a, int b)
    {
        return random.Next(a, b);
    }
    // Read-only property that return the current level
    public static int Level
    {
        get { return level; }
    }
}
