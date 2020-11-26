using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


class HUD
{
    // declare an offset variable for the main hud
    int offset;
    // declare a variable for the font
    SpriteFont font;
    // declare additional game assets (mainly for background and hud elements)
    Texture2D block, bombblock, hudbackground, gameover, menu, helpmenu, optionmenu;
    public HUD(ContentManager Content, int hudoffset = 360)
    {   //load content
        offset = hudoffset;
        font = Content.Load<SpriteFont>("SpelFont");
        block = Content.Load<Texture2D>("block");
        bombblock = Content.Load<Texture2D>("bomb_block");
        hudbackground = Content.Load<Texture2D>("hudbackground");
        gameover = Content.Load<Texture2D>("gameover");
        menu = Content.Load<Texture2D>("Mainmenu");
        helpmenu = Content.Load<Texture2D>("Helpmenu");
        optionmenu = Content.Load<Texture2D>("Optionsmenu");
    }
    // Draws the Menu screen background
    public void MenuScreen(GameTime gameTime, SpriteBatch s)
    {
        s.Draw(menu, Vector2.Zero, Color.White);
    }
    // Draws the Game Over screen and it's background
    public void GameOverScreen (GameTime gameTime, SpriteBatch s)
    {
        s.Draw(gameover, Vector2.Zero, Color.White);

        // Make strings to display the score gotten in the game the player just played, and also a string for the high score
        string Scorestring = "Score: " + Score.currentscore.ToString();
        string HighScorestring = "High Score: " + Score.highscore.ToString();
        // Draw these scores
        s.DrawString(font, Scorestring, new Vector2(160, 380), Color.Black);
        s.DrawString(font, HighScorestring, new Vector2(160, 420), Color.Black);
    }
    // Draws the options menu background
    public void Options(GameTime gameTime, SpriteBatch s)
    {
        s.Draw(optionmenu, Vector2.Zero, Color.White);
    }
    // Draws the Help Menu background
    public void Help(GameTime gameTime, SpriteBatch s)
    {
        s.Draw(helpmenu, Vector2.Zero, Color.White);
    }
    // Draws the HUD when the player is in-game
    public void MainDraw(GameTime gameTime, SpriteBatch s, Block nextblock)
    {
        // Draw the hud's background
        s.Draw(hudbackground, new Vector2(offset, 0), Color.White);
        // Declare two strings for the current score and level
        string Scorestring = "Score: " + Score.currentscore.ToString();
        string Levelstring = "Level: " + GameWorld.Level.ToString();
        // Draw these strings on the HUD with the hud offset
        s.DrawString(font, Scorestring, new Vector2(25 + offset , 0), Color.Black);
        s.DrawString(font, Levelstring, new Vector2(25 + offset, 25), Color.Black);
        // Draw the next block next to the playing field
        if(nextblock is BlockBOOM)
        {
            nextblock.DrawNextBlock(gameTime, s, bombblock, new Point(27 + offset, 120));
        }
        else
        {
            nextblock.DrawNextBlock(gameTime, s, block, new Point(27 + offset, 120));
        }
        s.DrawString(font, "Next Piece: ", new Vector2(25 + offset, 80), Color.Black);
        // Draw utility text (the name of the game)
        s.DrawString(font, ("SUPER" + Environment.NewLine + "TETRIS!"), new Vector2(offset+ 30, 550), Color.Black);
    }
}