using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace SAIYASASTRA
{
    public class MainMenu : Screen
    {
        GameSystem game;
        Texture2D[] ButtonGame = new Texture2D[6];
        Texture2D BG,
                  FrameBG;
        SpriteFont font;


        string Textg1 = "0";

        float TimeperFrame,
              totalElapsed;

        int frame,
            framePersSec = 15;

        Texture2D Logogame,
                  Textmainmenu2,
                  Textmainmenu;

        Vector2 LogogameScale,
                center,
                MousePos,
                LogoPos;

        public static bool Pagemainmenu = true,
                           Newgame_but = false,
                           IsbutSFX = true,
                           IsPlayMusicMainMenu = true,
                           IsbutMusic = true,
                           IsExit = true;

        bool[] IsMouseClick = new bool[8],
               IsMouseOnButton = new bool[6];

        public const float Rotation_M = 0,
                            Scale_M = 8.0f,
                            Depth_M = 0.5f;

        //LoadContent
        public MainMenu(GameSystem game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Setting
            TimeperFrame = (float)1 / framePersSec;
            for(int i = 0; i < 5; i++)
            {
                IsMouseClick[i] = true;
            }

            //LoadContent
            Logogame = game.Content.Load<Texture2D>("SATYASAATLOGOANIM");
            BG = game.Content.Load<Texture2D>("BG_Level2");
            font = game.Content.Load<SpriteFont>("November");
            FrameBG = game.Content.Load<Texture2D>("Frame");
            ButtonGame[0] = game.Content.Load<Texture2D>("But NewG Anima");
            ButtonGame[1] = game.Content.Load<Texture2D>("But Set Anima");
            ButtonGame[2] = game.Content.Load<Texture2D>("But Exit Anima");
            ButtonGame[3] = game.Content.Load<Texture2D>("But SFX Anima");
            ButtonGame[4] = game.Content.Load<Texture2D>("But Music Anima");
            ButtonGame[5] = game.Content.Load<Texture2D>("But Back Anima");
            Textmainmenu = game.Content.Load<Texture2D>("Main_Menu");
            Textmainmenu2 = game.Content.Load<Texture2D>("Setting_Menu");
            this.game = game;

            //SetGame
            center.X = game.GraphicsDevice.Viewport.Width;
            center.Y = game.GraphicsDevice.Viewport.Height;
            center.X = center.X / 2;
            center.Y = center.Y / 2;
            LogogameScale.X = 500 / 2;
            LogogameScale.Y = 645 / 2;
            LogoPos.X = center.X - LogogameScale.X;
            LogoPos.Y = center.Y - LogogameScale.Y;
        }

        //Update
        public override void UpdateGame(GameTime theTime)
        {
            //อนืเมชั่น Logo game
            UpdateframeLogo((float)theTime.ElapsedGameTime.TotalSeconds);

            //System Mouse
            MousePos.X = game.MousePosition.X;
            MousePos.Y = game.MousePosition.Y;

            if (game.FrameTransitionIn > 6 && Newgame_but == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = false;
                EventScreen.Invoke(game.mLevel1, new EventArgs());
                Newgame_but = false;
            }

                //String
                Textg1 = Convert.ToString(MousePos);
            base.UpdateGame(theTime);
        }

        //Draw
        public override void Draw(SpriteBatch theBatch)
        {
            //BG and Logo
            theBatch.Draw(BG, new Vector2(0, 0), null, Color.White);
            theBatch.Draw(Logogame, LogoPos, new Rectangle(frame * 500, 0, 500, 345), Color.White);

            //MenuScreen
            MainMenuScene(theBatch);

            //Frame
            theBatch.Draw(FrameBG, new Vector2(0, 0), null, Color.White);

            //String
            //theBatch.DrawString(font, Textg1, new Vector2(20, 650), Color.White);

            base.Draw(theBatch);
        }

        public void MainMenuScene(SpriteBatch theBatch)
        {
            if (IsPlayMusicMainMenu == true)
            {
                MediaPlayer.Play(game.MusicMainMenu);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.3f;
                IsPlayMusicMainMenu = false;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
            
            // Page1 MainMenu
            if (Pagemainmenu == true)
            {
                theBatch.Draw(Textmainmenu, new Vector2(576, 438), null, Color.White);

                //Buttom New Game
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 481 && MousePos.Y <= 527)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[0] == true)
                    {
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[0] = false;
                    }
                    //กดปุ่ม
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && Newgame_but == false)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true )
                        {
                            game.ButtonSFX[2].Play();
                        }
                        MediaPlayer.IsRepeating = false;
                        MediaPlayer.Stop();
                        game.PlayTransitionIn = true;
                        Newgame_but = true;
                    }
                    theBatch.Draw(ButtonGame[0], new Vector2(495, 481), new Rectangle(240, 0, 240, 46), Color.White);
                }
                else
                {
                    IsMouseOnButton[0] = true;
                    theBatch.Draw(ButtonGame[0], new Vector2(495, 481), new Rectangle(0, 0, 240, 46), Color.White);
                }

                //Buttom Setting
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 538 && MousePos.Y <= 584)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[1] == true)
                    {
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[1] = false;
                    }
                    theBatch.Draw(ButtonGame[1], new Vector2(495, 538), new Rectangle(240, 0, 240, 46), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseClick[0] == true)
                        {
                            game.ButtonSFX[1].Play();
                            IsMouseClick[0] = false;
                            IsMouseClick[3] = false;
                            
                        }
                        Pagemainmenu = false;
                        
                    } else
                    {
                        IsMouseClick[0] = true;
                    }
                }
                else
                {
                    IsMouseOnButton[1] = true;
                    theBatch.Draw(ButtonGame[1], new Vector2(495, 538), new Rectangle(0, 0, 240, 46), Color.White);
                }

                //Buttom Exit
                if (MousePos.X >= 495 && MousePos.X <= 735 && MousePos.Y >= 595 && MousePos.Y <= 641)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton[2] == true)
                    {
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton[2] = false;
                    }
                    theBatch.Draw(ButtonGame[2], new Vector2(495, 595), new Rectangle(240, 0, 240, 46), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseClick[1] == true)
                        {
                            game.ButtonSFX[1].Play();
                            IsMouseClick[1] = false;
                        }
                        if (IsExit == true)
                        {
                            Environment.Exit(0);
                        }

                    } else
                    {
                        IsMouseClick[1] = true;
                    }


                }
                else
                {
                    IsMouseOnButton[2] = true;
                    theBatch.Draw(ButtonGame[2], new Vector2(495, 595), new Rectangle(0, 0, 240, 46), Color.White);
                    IsExit = true;
                }
            }

            // Page2 Setting
            else
            {
                theBatch.Draw(Textmainmenu2, new Vector2(592.5f, 438), null, Color.White);
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
                        theBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(243, 0, 243, 46), Color.White);
                    }
                    else
                    {
                        IsMouseClick[0] = true;
                        IsMouseOnButton[3] = true;
                        theBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(0, 0, 243, 46), Color.White);
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
                        theBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(243, 46, 243, 46), Color.White);
                    }
                    else
                    {
                        IsMouseClick[1] = true;
                        IsMouseOnButton[3] = true;
                        theBatch.Draw(ButtonGame[3], new Vector2(492, 481), new Rectangle(0, 46, 243, 46), Color.White);
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
                        theBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(243, 0, 243, 46), Color.White);
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
                        theBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(0, 0, 243, 46), Color.White);
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
                        theBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(243, 46, 243, 46), Color.White);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            MainMenu.IsbutMusic = true;
                            game.IsPlayMusic = true;
                            if (IsMouseClick[3] == true)
                            {
                                game.ButtonSFX[1].Play();
                                IsMouseClick[3] = false;
                            }
                            MediaPlayer.Play(game.MusicMainMenu);
                            MediaPlayer.IsRepeating = true;
                        }
                    }
                    else
                    {
                        IsMouseClick[3] = true;
                        IsMouseOnButton[4] = true;
                        theBatch.Draw(ButtonGame[4], new Vector2(492, 538), new Rectangle(0, 46, 243, 46), Color.White);
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
                    theBatch.Draw(ButtonGame[5], new Vector2(495, 595), new Rectangle(240, 0, 240, 46), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && Pagemainmenu == false)
                        {
                            game.ButtonSFX[1].Play();
                        }
                        Pagemainmenu = true;
                        IsExit = false;
                    }
                }
                else
                {
                    IsMouseOnButton[5] = true;
                    theBatch.Draw(ButtonGame[5], new Vector2(495, 595), new Rectangle(0, 0, 240, 46), Color.White);
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
