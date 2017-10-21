﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static System.Console;



class HUD
{
    int offset;
    SpriteFont font;
    Texture2D block, hudbackground, gameover, menu;
    public HUD(ContentManager Content, int hudoffset = 360)
    {
        offset = hudoffset;
        font = Content.Load<SpriteFont>("SpelFont");
        block = Content.Load<Texture2D>("block");
        hudbackground = Content.Load<Texture2D>("hudbackground");
        gameover = Content.Load<Texture2D>("gameover");
        menu = Content.Load<Texture2D>("Mainmenu");
    }

    public void MenuScreen(GameTime gameTime, SpriteBatch s)
    {
        s.Draw(menu, Vector2.Zero, Color.White);
    }
    public void GameOverScreen (GameTime gameTime, SpriteBatch s)
    {
        s.Draw(gameover, Vector2.Zero, Color.White);
        string Scorestring = "Score: " + TetrisGame.Score.score.ToString();
        string HighScorestring = "HighScore: " + TetrisGame.Score.highscore.ToString();
        s.DrawString(font, Scorestring, new Vector2(70, 380), Color.Black);
        s.DrawString(font, HighScorestring, new Vector2(70, 420), Color.Black);
    }

    public void Options(GameTime gameTime, SpriteBatch s)
    {

    }

    public void MainDraw(GameTime gameTime, SpriteBatch s, Block nextblock)
    {
        // Draw the hud's background
        s.Draw(hudbackground, new Vector2(offset, 0), Color.White);
        // Draw the score next to the playing field
        string Scorestring = "Score: " + TetrisGame.Score.score.ToString();
        s.DrawString(font, Scorestring, new Vector2(25 + offset , 0), Color.Black);
        s.DrawString(font, "Next Piece: ", new Vector2(25 + offset, 80), Color.Black);
        s.DrawString(font, ("HYPER" + Environment.NewLine + "TETRIS!"), new Vector2(offset+ 30, 550), Color.Black);
        // Draw the block next to the playingfield
        nextblock.DrawNextBlock(gameTime, s, block, new Point(27 + offset, 120));
    }
}