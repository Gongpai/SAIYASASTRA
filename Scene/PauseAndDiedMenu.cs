using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class PauseAndDiedMenu : Screen
    {
        GameSystem game;

        int framePersSec;
        float TimeperFrame,
              totalElapsed;

        int frame;

        Texture2D Logogame,
                  Diedgame,
                  BG,
                  FrameBG,
                  Textsetting,
                  TextDYWTPA;

        Texture2D[] ButtonGame;

        Vector2 center,
                LogoPos,
                DiedPos,
                MousePos;

        Vector2[] TextPos;

        Vector2 LogogameScale;
        SpriteFont font;
        string Holy_Text = Convert.ToString(0);

        bool Pagemainmenu = true,
             IsBackToGame = false,
             IsBackTOMainMenu = false,
             IsExit = true;

        bool[] IsMouseClick = new bool[8],
               IsMouseOnButton = new bool[6];

        public static bool IsPlayMusicPauseMenu = false;
        public PauseAndDiedMenu(GameSystem game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Setiing
            for (int i = 0; i < 6; i++)
            {
                IsMouseOnButton[i] = true;
            }
            ButtonGame = new Texture2D[12];
            center.X = game.GraphicsDevice.Viewport.Width;
            center.Y = game.GraphicsDevice.Viewport.Height;
            center.X = center.X / 2;
            center.Y = center.Y / 2;
            LogogameScale.X = 500 / 2;
            LogogameScale.Y = 645 / 2;
            LogoPos.X = center.X - LogogameScale.X;
            LogoPos.Y = center.Y - LogogameScale.Y;
            DiedPos.X = center.X - 270;
            DiedPos.Y = center.Y - 325;
            framePersSec = 15;
            TimeperFrame = (float)1 / framePersSec;
            frame = 0;
            totalElapsed = 0;
            TextPos = new Vector2[16];
            TextPos[0].X = center.X - 47.5f;
            TextPos[0].Y = 438;

            //LoadContent
            ButtonGame[0] = game.Content.Load<Texture2D>("But NewG Anima");
            ButtonGame[1] = game.Content.Load<Texture2D>("But Set Anima");
            ButtonGame[2] = game.Content.Load<Texture2D>("But Exit Anima");
            ButtonGame[3] = game.Content.Load<Texture2D>("But SFX Anima");
            ButtonGame[4] = game.Content.Load<Texture2D>("But Music Anima");
            ButtonGame[5] = game.Content.Load<Texture2D>("But Back Anima");
            ButtonGame[9] = game.Content.Load<Texture2D>("But Back to Menu Anima");
            ButtonGame[10] = game.Content.Load<Texture2D>("But Continue Anima");
            Logogame = game.Content.Load<Texture2D>("SATYASAATLOGOANIM");
            Diedgame = game.Content.Load<Texture2D>("DIEDLOGOANIM");
            font = game.Content.Load<SpriteFont>("November");
            BG = game.Content.Load<Texture2D>("BG_Level2");
            FrameBG = game.Content.Load<Texture2D>("Frame");
            TextDYWTPA = game.Content.Load<Texture2D>("DUWTPA_Died");
            Textsetting = game.Content.Load<Texture2D>("Setting_Menu");

            this.game = game;
        }

        public override void UpdateGame(GameTime theTime)
        {
            if(IsBackToGame == true && game.FrameTransitionIn > 6)
            {
                game.PlayTransitionIn = false;
                if(game.Level == 1)
                {
                    EventScreen.Invoke(game.mLevel1, new EventArgs());
                }
                if (game.Level == 2)
                {
                    EventScreen.Invoke(game.mLevel2, new EventArgs());
                }
                if (game.Level == 3)
                {
                    EventScreen.Invoke(game.mLevel3, new EventArgs());
                }
                if (game.Level == 4)
                {
                    MediaPlayer.Play(game.MusicLevel);
                    EventScreen.Invoke(game.mLevel4, new EventArgs());
                }
                game.PlayTransitionOut = true;
                game.IsPauseMenu = false;
                game.IsDiedMenu = false;
                game.IsGamePause = false;
                IsBackToGame = false;
            }
            if(game.FrameTransitionIn > 6 && IsBackTOMainMenu == true)
            {
                game.PlayTransitionIn = false;
                EventScreen.Invoke(game.mMainMenu, new EventArgs());
                game.PlayTransitionOut = true;
                game.IsPauseMenu = false;
                game.IsDiedMenu = false;
                game.IsGamePause = false;
                game.CharHart = 100;
                IsBackTOMainMenu = false;
            }
            if (IsPlayMusicPauseMenu == true && game.IsPauseMenu == false)
            {
                MediaPlayer.Play(game.MusicMainMenu);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.3f;
                IsPlayMusicPauseMenu = false;
            }
            
            MousePos = game.MousePosition;
            UpdateframeLogo((float)theTime.ElapsedGameTime.TotalSeconds); ;
            base.UpdateGame(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            Died_PauseMenu(theBatch);

            //Frame
            theBatch.Draw(FrameBG, new Vector2(0, 0), null, Color.White);
            base.Draw(theBatch);
        }

        public void Died_PauseMenu(SpriteBatch spriteBatch)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                IsBackToGame = true;
                if (game.IsPauseMenu == false)
                {
                    game.IsplayMusicGAttack = true;
                    game.IsShowChar = true;
                    game.GhostPos = -192;
                    game.CharHart = 100;
                    game.SpeedGhostLevel1 = 0;
                    game.IsGhost_See = false;
                    if (game.GhostPos_Level2 < 640)
                    {
                        game.SpeedGhostLevel2 = 0;
                        game.GhostPos_Level2 = -192;
                        game.GhostWalkBackward = 0;
                        game.ISGhostBackward = false;
                        game.PlayTransitionIn = true;
                    }
                    else
                    {
                        game.SpeedGhostLevel2 = 3280;
                        game.GhostPos_Level2 = 1479;
                        game.GhostWalkBackward = 1;
                        game.ISGhostBackward = true;
                        game.PlayTransitionIn = true;
                    }
                    if (game.GhostPos_Level3 < 640)
                    {
                        game.SpeedGhostLevel3 = 0;
                        game.GhostPos_Level3 = -192;
                        game.GhostWalkBackward = 0;
                        game.ISGhostBackward = false;
                        game.PlayTransitionIn = true;
                    }
                    else
                    {
                        game.SpeedGhostLevel3 = 3280;
                        game.GhostPos_Level3 = 1479;
                        game.GhostWalkBackward = 1;
                        game.ISGhostBackward = true;
                        game.PlayTransitionIn = true;
                    }
                }
                game.PlayTransitionIn = true;
            }
            //BG and Logo
            spriteBatch.Draw(BG, new Vector2(0, 0), null, Color.White);
            if (game.IsPauseMenu == true)
            {
                spriteBatch.Draw(Logogame, LogoPos, new Rectangle(frame * 500, 0, 500, 345), Color.White);
            }
            else
            {
                spriteBatch.Draw(Diedgame, DiedPos, new Rectangle(frame * 540, 0, 540, 301), Color.White);
            }


            // Page1 MainMenu
            if (Pagemainmenu == true)
            {
                spriteBatch.Draw(TextDYWTPA, new Vector2(center.X - (TextDYWTPA.Width / 2), 438), null, Color.White);


                //Buttom Continue
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 481 && MousePos.Y <= 527)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[0] == true)
                    {
                        SoundEffect.MasterVolume = 1.0f;
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[0] = false;
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsBackToGame == false)
                        {
                            game.ButtonSFX[1].Play();
                        }
                        IsBackToGame = true;
                        if (game.IsPauseMenu == false)
                        {
                            game.IsPlaySFXGhostSee = true;
                            game.IsShowChar = true;
                            game.GhostPos = -192;
                            game.CharHart = 100;
                            game.SpeedGhostLevel1 = 0;
                            game.IsplayMusicGAttack = true;
                            game.IsGhost_See = false;
                            if (game.GhostPos_Level2 < 640)
                            {
                                game.SpeedGhostLevel2 = 0;
                                game.GhostPos_Level2 = -192;
                                game.GhostWalkBackward = 0;
                                game.ISGhostBackward = false;
                                game.PlayTransitionIn = true;
                            }
                            else
                            {
                                game.SpeedGhostLevel2 = 3280;
                                game.GhostPos_Level2 = 1479;
                                game.GhostWalkBackward = 1;
                                game.ISGhostBackward = true;
                                game.PlayTransitionIn = true;
                            }
                            if (game.GhostPos_Level3 < 640)
                            {
                                game.SpeedGhostLevel3 = 0;
                                game.GhostPos_Level3 = -192;
                                game.GhostWalkBackward = 0;
                                game.ISGhostBackward = false;
                                game.PlayTransitionIn = true;
                            }
                            else
                            {
                                game.SpeedGhostLevel3 = 3280;
                                game.GhostPos_Level3 = 1479;
                                game.GhostWalkBackward = 1;
                                game.ISGhostBackward = true;
                                game.PlayTransitionIn = true;
                            }
                        }
                        game.PlayTransitionIn = true;
                    }
                    spriteBatch.Draw(ButtonGame[10], new Vector2(495, 481), new Rectangle(240, 0, 240, 46), Color.White);
                }
                else
                {
                    IsMouseOnButton[0] = true;
                    spriteBatch.Draw(ButtonGame[10], new Vector2(495, 481), new Rectangle(0, 0, 240, 46), Color.White);
                }

                //Buttom Setting
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 538 && MousePos.Y <= 584)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[1] == true)
                    {
                        SoundEffect.MasterVolume = 1.0f;
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[1] = false;
                    }
                    spriteBatch.Draw(ButtonGame[1], new Vector2(495, 538), new Rectangle(240, 0, 240, 46), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && Pagemainmenu == true)
                        {
                            game.ButtonSFX[1].Play();
                        }
                        Pagemainmenu = false;
                    }
                }
                else
                {
                    IsMouseOnButton[1] = true;
                    spriteBatch.Draw(ButtonGame[1], new Vector2(495, 538), new Rectangle(0, 0, 240, 46), Color.White);
                }

                //Buttom Backtomenu
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 595 && MousePos.Y <= 641)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[2] == true)
                    {
                        SoundEffect.MasterVolume = 1.0f;
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[2] = false;
                    }
                    spriteBatch.Draw(ButtonGame[9], new Vector2(495, 595), new Rectangle(240, 0, 240, 46), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsBackTOMainMenu == false)
                        {
                            game.ButtonSFX[1].Play();
                        }
                        if (IsExit == true)
                        {
                            game.ResetGame();
                            game.PlayJumpScareEffact = false;
                            game.PlayEffectLevel2 = false;
                            IsBackTOMainMenu = true;
                            game.PlayTransitionIn = true;
                            game.IsReset = true;
                        }

                    }


                }
                else
                {
                    IsMouseOnButton[2] = true;
                    spriteBatch.Draw(ButtonGame[9], new Vector2(495, 595), new Rectangle(0, 0, 240, 46), Color.White);
                    IsExit = true;
                }
            }

            // Page2 Setting
            else
            {
                spriteBatch.Draw(Textsetting, TextPos[0], null, Color.White);
                //Buttom SFX
                if (MainMenu.IsbutSFX == true)
                {
                    if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 481 && MousePos.Y <= 527)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton[3] == true)
                        {
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton[3] = false;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            //เสียงปุ่ม
                            if (game.IsPlaySFX == true && IsMouseClick[0] == true)
                            {
                                game.ButtonSFX[1].Play();
                                IsMouseClick[0] = false;
                            }
                            SoundEffect.MasterVolume = 0.0f;
                            game.IsPlaySFX = false;
                            MainMenu.IsbutSFX = false;
                        }
                        spriteBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(243, 0, 243, 46), Color.White);
                    }
                    else
                    {
                        IsMouseClick[0] = true;
                        IsMouseOnButton[3] = true;
                        spriteBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(0, 0, 243, 46), Color.White);
                    }
                }
                else
                {
                    if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 481 && MousePos.Y <= 527)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton[3] == true)
                        {
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton[3] = false;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            //เสียงปุ่ม
                            if (game.IsPlaySFX == true && IsMouseClick[1] == true)
                            {
                                game.ButtonSFX[1].Play();
                                IsMouseClick[1] = false;
                            }
                            SoundEffect.MasterVolume = 1.0f;
                            game.IsPlaySFX = true;
                            MainMenu.IsbutSFX = true;
                        }
                        spriteBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(243, 46, 243, 46), Color.White);
                    }
                    else
                    {
                        IsMouseClick[1] = true;
                        IsMouseOnButton[3] = true;
                        spriteBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(0, 46, 243, 46), Color.White);
                    }
                }


                //Buttom Music
                if (MainMenu.IsbutMusic == true)
                {
                    if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 538 && MousePos.Y <= 584)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton[4] == true)
                        {
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton[4] = false;
                        }
                        spriteBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(243, 0, 243, 46), Color.White);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            MediaPlayer.Stop();
                            //เสียงปุ่ม
                            if (game.IsPlayMusic == true && IsMouseClick[2] == true)
                            {
                                game.ButtonSFX[1].Play();
                                IsMouseClick[2] = false;
                            }
                            if (IsMouseClick[2] == false)
                            {
                                game.IsPlayMusic = false;
                                
                            }
                            MainMenu.IsbutMusic = false;
                        }
                    }
                    else
                    {
                        IsMouseClick[2] = true;
                        IsMouseOnButton[4] = true;
                        spriteBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(0, 0, 243, 46), Color.White);
                    }
                }
                else
                {
                    if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 538 && MousePos.Y <= 584)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton[4] == true)
                        {
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton[4] = false;
                        }
                        spriteBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(243, 46, 243, 46), Color.White);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            MainMenu.IsbutMusic = true;
                            game.IsPlayMusic = true;
                            if (IsMouseClick[3] == true)
                            {
                                game.ButtonSFX[1].Play();
                                IsMouseClick[3] = false;
                            }
                            if (Level1.Show_Ghost == true || Level2.Show_Ghost[1] == true || Level2.Show_Ghost[3] == true || Level3.Show_Ghost[1] == true || Level3.Show_Ghost[3] == true)
                            {
                                MediaPlayer.Play(game.MusicGhostAttack);
                                MediaPlayer.IsRepeating = true;
                            }
                            else
                            {
                                MediaPlayer.Play(game.MusicLevel);
                                MediaPlayer.IsRepeating = true;
                            }
                        }
                    }
                    else
                    {
                        IsMouseClick[3] = true;
                        IsMouseOnButton[4] = true;
                        spriteBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(0, 46, 243, 46), Color.White);
                    }
                }


                //Buttom Back
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 595 && MousePos.Y <= 641)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[5] == true)
                    {
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[5] = false;
                    }
                    spriteBatch.Draw(ButtonGame[5], new Vector2(495, 595), new Rectangle(240, 0, 240, 46), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        Pagemainmenu = true;
                        IsExit = false;
                    }
                }
                else
                {
                    IsMouseOnButton[5] = true;
                    spriteBatch.Draw(ButtonGame[5], new Vector2(495, 595), new Rectangle(0, 0, 240, 46), Color.White);
                }
            }

        }

        public void UpdateframeLogo(float elapsedlogo)
        {
            totalElapsed += elapsedlogo;
            if (totalElapsed > TimeperFrame)
            {
                frame = frame + 1;
                if (frame == 3)
                {
                    frame = 0;
                }
                totalElapsed -= TimeperFrame;
            }
        }
    }
}
