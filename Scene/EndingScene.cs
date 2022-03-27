using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class EndingScene : Screen
    {
        GameSystem game;
        SpriteFont font,
                   font_bold;

        Random r_SFX = new Random();

        Texture2D BG,
                  Logogame,
                  FrameBG,
                  Char,
                  ButtonGame,
                  text_begin_sence,
                  line_begin_sence,
                  cr1_begin_sence,
                  cr2_begin_sence,
                  cr3_begin_sence;

        int frame,
            i_r_SFX = 0,
            Speed = 0,
            TotalFrame = 0,
            FrameChar = 0,
            framePersSec;
            
        int[] Sec = new int[4],
              TextScroll = new int[31];

        float totalElapsed,
              TimeperFrame;
        float[] totalEla = new float[10];

        bool StartTime = true,
             Show_Ending = false,
             IsWalk = false,
             IsPlayAnim = true,
             IsShowChar = true,
             IsUp = false,
             IsDown = false,
             IsPlayCredit = false,
             IsCharMove = true,
             IsfloorMove = false,
             To_MainMenu = false,
             IsPlayMusicCredit = false,
             IsPlayMusicCutSence = true,
             IsMouseOnButton = true,
             IsMouseClick = true,
             StartTime_Ending = true;

        string Textg1 = "";

        Vector2 MousePos,
                BgPos = new Vector2(0, -728),
                CharPos = new Vector2(0, 392),
                center;
        Vector2[] TextPos = new Vector2[31];

        public EndingScene(GameSystem game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Setting
            TextScroll[0] = 720;
            TextScroll[1] = 720 + 0;
            TextScroll[2] = 720 + 100;
            TextScroll[3] = 720 + 150;
            TextScroll[4] = 720 + 250;
            TextScroll[5] = 720 + 300;
            TextScroll[6] = 720 + 400;
            TextScroll[7] = 720 + 450;
            TextScroll[8] = 720 + 550;
            TextScroll[9] = 720 + 600;
            TextScroll[10] = 720 + 700;
            TextScroll[11] = 720 + 750;
            TextScroll[12] = 720 + 850;
            TextScroll[13] = 720 + 900;
            TextScroll[14] = 720 + 950;
            TextScroll[15] = 720 + 1050;
            TextScroll[16] = 720 + 1100;
            TextScroll[17] = 720 + 1150;
            TextScroll[18] = 720 + 1200;
            TextScroll[19] = 720 + 1250;
            TextScroll[20] = 720 + 1300;
            TextScroll[21] = 720 + 1350;
            TextScroll[22] = 720 + 1400;
            TextScroll[23] = 720 + 1450;
            TextScroll[24] = 720 + 1550;
            TextScroll[25] = 720 + 1600;
            TextScroll[26] = 720 + 1650;
            TextScroll[27] = 720 + 1700;
            TextScroll[28] = 720 + 1750;
            TextScroll[29] = 720 + 1800;
            TextScroll[30] = 720 + 1850;
            center.X = game.GraphicsDevice.Viewport.Width;
            center.Y = game.GraphicsDevice.Viewport.Height;
            center.X = center.X / 2;
            center.Y = center.Y / 2;
            TextPos[0].X = center.X - 47.5f;
            TextPos[0].Y = 438;
            framePersSec = 15;
            TimeperFrame = (float)1 / framePersSec;
            frame = 0;
            totalElapsed = 0;

            //LoadContent
            font = game.Content.Load<SpriteFont>("November");
            font_bold = game.Content.Load<SpriteFont>("Font_Bold");
            BG = game.Content.Load<Texture2D>("BGEnding");
            Char = game.Content.Load<Texture2D>("CharEnding");
            FrameBG = game.Content.Load<Texture2D>("Frame");
            text_begin_sence = game.Content.Load<Texture2D>("beta1l5");
            line_begin_sence = game.Content.Load<Texture2D>("beta05");
            cr1_begin_sence = game.Content.Load<Texture2D>("S2L4");
            cr2_begin_sence = game.Content.Load<Texture2D>("S1L4");
            ButtonGame = game.Content.Load<Texture2D>("Next_button");
            Logogame = game.Content.Load<Texture2D>("SATYASAATLOGOANIM");

            //เสียง
            
            this.game = game;
        }
        public override void UpdateGame(GameTime theTime)
        {
            MousePos = game.MousePosition;
            TimeS1((int)theTime.ElapsedGameTime.TotalSeconds);
            UpdateframeLogo((float)theTime.ElapsedGameTime.TotalSeconds);
            if (game.FrameTransitionIn > 6 && To_MainMenu == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                EventScreen.Invoke(game.mMainMenu, new EventArgs());
                game.IsReset = true;
            }
            if(IsPlayCredit == true)
            {
                TimeSEnding((int)theTime.ElapsedGameTime.TotalSeconds);
            }
            CharAnim();
            
            Textg1 = Convert.ToString(CharPos.X + " <> " + BgPos.X + " <> " + Sec[3] + " <> " + BgPos.Y);
            base.UpdateGame(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            if (game.FrameTransitionIn > 6 && Show_Ending == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                Show_Ending = false;
                StartTime = false;
                MediaPlayer.IsRepeating = false;
                MediaPlayer.Stop();
                MediaPlayer.Play(game.MusicEnding2);
            }
            if (StartTime == true)
            {
                if (IsPlayMusicCutSence == true)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(game.MusicCutScene[4]);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.5f;
                }
                IsPlayMusicCutSence = false;
                CutSenceEnding(theBatch);
            }
            else
            {
                Ending_Secene(theBatch);
                if (IsPlayCredit == true)
                {
                    Credit(theBatch);
                }
            }

            //Frame
            theBatch.Draw(FrameBG, new Vector2(0, 0), null, Color.White);
            //theBatch.DrawString(font, Textg1, new Vector2(20, 600), Color.White);
            base.Draw(theBatch);
        }
        void CutSenceEnding(SpriteBatch spriteBatch)
        {
            //Sence1
            if (Sec[0] > 0)
            {
                if (game.IsPlaySFX == true && game.IsPlaySFXCutSence == true)
                {
                    game.CutSenceSFX[0].Play();
                    SoundEffect.MasterVolume = 0.4f;
                    game.IsPlaySFXCutSence = false;
                }
                
                spriteBatch.Draw(line_begin_sence, new Vector2(275, 0), new Rectangle(0, 0, 166, 811), Color.White);
                spriteBatch.Draw(text_begin_sence, new Vector2(22, 154), new Rectangle(1281, 0, 427, 56), Color.White);
            }

            if (Sec[0] > 10)
            {
                //Sence3
                spriteBatch.Draw(cr2_begin_sence, new Vector2(830, 259), null, Color.White);
            }

            if (Sec[0] > 5)
            {
                //Sence2
                spriteBatch.Draw(cr1_begin_sence, new Vector2(307, 239), null, Color.White);
                spriteBatch.Draw(line_begin_sence, new Vector2(825, 0), new Rectangle(0, 0, 166, 811), Color.White);
                spriteBatch.Draw(text_begin_sence, new Vector2(433, 247), new Rectangle(1281, 56, 427, 56), Color.White);
            }

            if (Sec[0] > 10)
            {
                //Sence3
                spriteBatch.Draw(text_begin_sence, new Vector2(792, 165), new Rectangle(1281, 56 * 2, 427, 56), Color.White);
            }

            if (Sec[0] > 15)
            {
                if (MousePos.X >= 1027 && MousePos.X <= 1216 && MousePos.Y >= 640 && MousePos.Y <= 686)
                {
                    //เสียงปุ่ม
                    if (game.IsPlaySFX == true && IsMouseOnButton == true)
                    {
                        game.ButtonSFX[0].Play();
                        IsMouseOnButton = false;
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseClick == true)
                        {
                            game.ButtonSFX[1].Play();
                            IsMouseClick = false;
                        }
                        IsWalk = true;
                        game.PlayTransitionIn = true;
                        Show_Ending = true;
                    }
                    spriteBatch.Draw(ButtonGame, new Vector2(1027, 640), new Rectangle(189, 0, 189, 46), Color.White);
                }
                else
                {
                    spriteBatch.Draw(ButtonGame, new Vector2(1027, 640), new Rectangle(0, 0, 189, 46), Color.White);
                }
            }


        }
        void Ending_Secene(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BG, BgPos, null, Color.White);
            if(IsShowChar == true)
            {
                spriteBatch.Draw(Char, CharPos, new Rectangle(95 * FrameChar, 0, 95, 173), Color.White);
            }
        }
        void CharAnim()
        {
            //อนิเมชั่นเดิน
            if (IsPlayAnim == true)
            {
                TotalFrame += 1;
                if (TotalFrame >= 12)
                {
                    FrameChar += 1;
                    TotalFrame = 0;
                    
                }
                if (FrameChar >= 3)
                {
                    FrameChar = 0;
                    if (StartTime == false)
                    {
                        SoundEffect.MasterVolume = 1f;
                        i_r_SFX = r_SFX.Next(2);
                        game.FootStepSFX[i_r_SFX].CreateInstance().Play();
                    }
                }
            }

            //เดิน
            if (IsWalk == true)
            {
                Speed = 1;
            } else
            {
                Speed = 0;
            }
            if (IsCharMove == true)
            {
                CharPos.X += Speed;
                if (CharPos.X > 592.5f)
                {
                    IsfloorMove = true;
                    IsCharMove = false;
                }
            }
            if (IsfloorMove == true)
            {
                if(BgPos.X > -400)
                {
                    BgPos.X -= Speed;
                } else
                {
                    if(CharPos.X < 1000)
                    {
                        CharPos.X += Speed;
                    } else
                    {
                        IsPlayAnim = false;
                        Sec[3] += 1;
                        if(Sec[3] >= 30)
                        {
                            IsShowChar = false;
                            IsfloorMove = false;
                            IsUp = true;
                            Sec[3] = 0;
                        }
                    }
                    
                }
            }
            if(IsUp == true)
            {
                if(BgPos.Y < 0)
                {
                    BgPos.Y += Speed;
                } else
                {
                    Sec[3] += 1;
                    if(Sec[3] >= 200)
                    {
                        IsUp = false;
                        IsDown = true;
                        Sec[3] = 0;
                    }
                    
                }
            }
            if(IsDown == true)
            {
                if (BgPos.Y > -1858)
                {
                    BgPos.Y -= 4;
                }
                else
                {
                    Speed = 0;
                    if(IsPlayMusicCredit == false)
                    {
                        MediaPlayer.Play(game.MusicEnding);
                        IsPlayMusicCredit = true;
                    }
                }
                if (BgPos.Y < -1346)
                {
                    if(TextScroll[0] > 208)
                    {
                        TextScroll[0] -= 4;
                    }
                    IsPlayCredit = true;
                }
                
            }
        }
        void Credit(SpriteBatch spriteBatch)
        {
            
            TextPos[1] = font_bold.MeasureString("Game DD and Grade DD TEAM");
            TextPos[2] = font_bold.MeasureString("Game Programmer / Creative Director");
            TextPos[3] = font.MeasureString("Kongphai Wutthichaiya");
            TextPos[4] = font_bold.MeasureString("Game Artist / Animator");
            TextPos[5] = font.MeasureString("Kavintida Tafan");
            TextPos[6] = font_bold.MeasureString("Game Artist / Project Manager");
            TextPos[7] = font.MeasureString("Naris Booninkiew");
            TextPos[8] = font_bold.MeasureString("Level Editor / Audio & Sound");
            TextPos[9] = font.MeasureString("Nuttanicha Kunlava");
            TextPos[10] = font_bold.MeasureString("Project supervisor");
            TextPos[11] = font.MeasureString("Lecturer Noppon Wongta");
            TextPos[12] = font_bold.MeasureString("Advisor");
            TextPos[13] = font.MeasureString("Dr.Patison Palee");
            TextPos[14] = font.MeasureString("Dr.Thepparit Sinthamrongruk");
            TextPos[15] = font_bold.MeasureString("Background Music");
            TextPos[16] = font.MeasureString("[Scary Soundtrack] EP.1 - The DiangDang Studio");
            TextPos[17] = font.MeasureString("TORMENTED - Epic Intense Hybrid Horror Music Mix - Horror Music World");
            TextPos[18] = font.MeasureString("Bridge of Death - Chernobyl OST");
            TextPos[19] = font.MeasureString("Khon Uat Phee Sound - Khon Uat Phee TV Show");
            TextPos[20] = font.MeasureString("HORROR THEME - VIVEK ABHISHEK");
            TextPos[21] = font.MeasureString("Scary sounds from a night in the wild - Ambient Mixer");
            TextPos[22] = font.MeasureString("Calming Nature Night Sounds - TheSilentWatcher");
            TextPos[23] = font.MeasureString("Home Sweet Home OST - Home Sweet Home");
            TextPos[24] = font.MeasureString("SoundEffect");
            TextPos[25] = font.MeasureString("www.mixkit.com");
            TextPos[26] = font.MeasureString("SFX-GhostLevel 1-4 - Youtube");
            TextPos[27] = font.MeasureString("www.pond5.com");
            TextPos[28] = font.MeasureString("Dimensional Door Sound Effect HD - [NAMI - Relaxing Sounds]");
            TextPos[29] = font.MeasureString("www.soundsnap.com");
            TextPos[30] = font.MeasureString("www.storyblocks.com");

            
            if (Sec[1] > 20 && TextScroll[30] >= -50 && StartTime == false)
            {
                TextScroll[0] -= 1;
                TextScroll[1] -= 1;
                TextScroll[2] -= 1;
                TextScroll[3] -= 1;
                TextScroll[4] -= 1;
                TextScroll[5] -= 1;
                TextScroll[6] -= 1;
                TextScroll[7] -= 1;
                TextScroll[8] -= 1;
                TextScroll[9] -= 1;
                TextScroll[10] -= 1;
                TextScroll[11] -= 1;
                TextScroll[12] -= 1;
                TextScroll[13] -= 1;
                TextScroll[14] -= 1;
                TextScroll[15] -= 1;
                TextScroll[16] -= 1;
                TextScroll[17] -= 1;
                TextScroll[18] -= 1;
                TextScroll[19] -= 1;
                TextScroll[20] -= 1;
                TextScroll[21] -= 1;
                TextScroll[22] -= 1;
                TextScroll[23] -= 1;
                TextScroll[24] -= 1;
                TextScroll[25] -= 1;
                TextScroll[26] -= 1;
                TextScroll[27] -= 1;
                TextScroll[28] -= 1;
                TextScroll[29] -= 1;
                TextScroll[30] -= 1;
            }
            if(Sec[1] == 175)
            {
                if(MediaPlayer.Volume > 0.0f)
                {
                    MediaPlayer.Volume -= 0.02f;
                }
            }
            if (Sec[1] == 182)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(game.MusicMainMenu);
                StartTime_Ending = false;
                game.PlayTransitionIn = true;
                To_MainMenu = true;
            }
            spriteBatch.Draw(Logogame, new Vector2(center.X - 250, TextScroll[0]), new Rectangle(frame * 500, 0, 500, 345), Color.White);

            spriteBatch.DrawString(font_bold, "Game DD and Grade DD TEAM", new Vector2(center.X - (TextPos[1].X / 2), TextScroll[1]), Color.White);

            spriteBatch.DrawString(font_bold, "Game Programmer / Creative Director", new Vector2(center.X - (TextPos[2].X / 2), TextScroll[2]), Color.White);
            spriteBatch.DrawString(font, "Kongphai Wutthichaiya", new Vector2(center.X - (TextPos[3].X / 2), TextScroll[3]), Color.White);

            spriteBatch.DrawString(font_bold, "Game Artist / Animator", new Vector2(center.X - (TextPos[4].X / 2), TextScroll[4]), Color.White);
            spriteBatch.DrawString(font, "Kavintida Tafan", new Vector2(center.X - (TextPos[5].X / 2), TextScroll[5]), Color.White);

            spriteBatch.DrawString(font_bold, "Game Artist / Project Manager", new Vector2(center.X - (TextPos[6].X / 2), TextScroll[6]), Color.White);
            spriteBatch.DrawString(font, "Naris Booninkiew", new Vector2(center.X - (TextPos[7].X / 2), TextScroll[7]), Color.White);

            spriteBatch.DrawString(font_bold, "Level Editor / Audio & Sound", new Vector2(center.X - (TextPos[8].X / 2), TextScroll[8]), Color.White);
            spriteBatch.DrawString(font, "Nuttanicha Kunlava", new Vector2(center.X - (TextPos[9].X / 2), TextScroll[9]), Color.White);

            spriteBatch.DrawString(font_bold, "Project supervisor", new Vector2(center.X - (TextPos[10].X / 2), TextScroll[10]), Color.White);
            spriteBatch.DrawString(font, "Lecturer Noppon Wongta", new Vector2(center.X - (TextPos[11].X / 2), TextScroll[11]), Color.White);

            spriteBatch.DrawString(font_bold, "Advisor", new Vector2(center.X - (TextPos[12].X / 2), TextScroll[12]), Color.White);
            spriteBatch.DrawString(font, "Dr.Patison Palee", new Vector2(center.X - (TextPos[13].X / 2), TextScroll[13]), Color.White);
            spriteBatch.DrawString(font, "Dr.Thepparit Sinthamrongruk", new Vector2(center.X - (TextPos[14].X / 2), TextScroll[14]), Color.White);
           
            spriteBatch.DrawString(font_bold, "Background Music", new Vector2(center.X - (TextPos[15].X / 2), TextScroll[15]), Color.White);
            spriteBatch.DrawString(font, "[Scary Soundtrack] EP.1 - The DiangDang Studio", new Vector2(center.X - (TextPos[16].X / 2), TextScroll[16]), Color.White);
            spriteBatch.DrawString(font, "TORMENTED - Epic Intense Hybrid Horror Music Mix - Horror Music World", new Vector2(center.X - (TextPos[17].X / 2), TextScroll[17]), Color.White);
            spriteBatch.DrawString(font, "Bridge of Death - Chernobyl OST", new Vector2(center.X - (TextPos[18].X / 2), TextScroll[18]), Color.White);
            spriteBatch.DrawString(font, "Khon Uat Phee Sound - Khon Uat Phee TV Show", new Vector2(center.X - (TextPos[19].X / 2), TextScroll[19]), Color.White);
            spriteBatch.DrawString(font, "HORROR THEME - VIVEK ABHISHEK", new Vector2(center.X - (TextPos[20].X / 2), TextScroll[20]), Color.White);
            spriteBatch.DrawString(font, "Scary sounds from a night in the wild - Ambient Mixer", new Vector2(center.X - (TextPos[21].X / 2), TextScroll[21]), Color.White);
            spriteBatch.DrawString(font, "Calming Nature Night Sounds - TheSilentWatcher", new Vector2(center.X - (TextPos[22].X / 2), TextScroll[22]), Color.White);
            spriteBatch.DrawString(font, "Home Sweet Home OST - Home Sweet Home", new Vector2(center.X - (TextPos[23].X / 2), TextScroll[23]), Color.White);

            spriteBatch.DrawString(font_bold, "SoundEffect", new Vector2(center.X - (TextPos[24].X / 2), TextScroll[24]), Color.White);
            spriteBatch.DrawString(font, "www.mixkit.com", new Vector2(center.X - (TextPos[25].X / 2), TextScroll[25]), Color.White);
            spriteBatch.DrawString(font, "SFX-GhostLevel 1-4 - Youtube", new Vector2(center.X - (TextPos[26].X / 2), TextScroll[26]), Color.White);
            spriteBatch.DrawString(font, "www.pond5.com", new Vector2(center.X - (TextPos[27].X / 2), TextScroll[27]), Color.White);
            spriteBatch.DrawString(font, "Dimensional Door Sound Effect HD - [NAMI - Relaxing Sounds]", new Vector2(center.X - (TextPos[28].X / 2), TextScroll[28]), Color.White);
            spriteBatch.DrawString(font, "www.soundsnap.com", new Vector2(center.X - (TextPos[29].X / 2), TextScroll[29]), Color.White);
            spriteBatch.DrawString(font, "www.storyblocks.com", new Vector2(center.X - (TextPos[30].X / 2), TextScroll[30]), Color.White);
        }
        void TimeSEnding(int TimeforS1)
        {
            if (StartTime == false)
            {
                totalEla[1]++;
                if (totalEla[1] > 15)
                {
                    Sec[1]++;
                    totalEla[1] = 0;
                }

            }

        }
        void TimeS1(int TimeforS1)
        {
            if (StartTime == true)
            {
                totalEla[0]++;
                if (totalEla[0] > 15)
                {
                    Sec[0]++;
                    totalEla[0] = 0;
                }

            }

        }
        void UpdateframeLogo(float elapsedlogo)
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
        public void Reset()
        {
            frame = 0;
            i_r_SFX = 0;
            Speed = 0;
            TotalFrame = 0;
            FrameChar = 0;
            framePersSec =0;
            for(int i = 0; i < 4; i++)
            {
                Sec[i] = 0;
            }
            totalElapsed = 0;
            TimeperFrame = 0;

            StartTime = true;
            Show_Ending = false;
            IsWalk = false;
            IsPlayAnim = true;
            IsShowChar = true;
            IsUp = false;
            IsDown = false;
            IsPlayCredit = false;
            IsCharMove = true;
            IsfloorMove = false;
            To_MainMenu = false;
            IsPlayMusicCredit = false;
            IsPlayMusicCutSence = true;
            IsMouseOnButton = true;
            IsMouseClick = true;
                 StartTime_Ending = true;

            Textg1 = "";
            BgPos = new Vector2(0, -728);
            CharPos = new Vector2(0, 392);
            TextScroll[0] = 720;
            TextScroll[1] = 720 + 0;
            TextScroll[2] = 720 + 100;
            TextScroll[3] = 720 + 150;
            TextScroll[4] = 720 + 250;
            TextScroll[5] = 720 + 300;
            TextScroll[6] = 720 + 400;
            TextScroll[7] = 720 + 450;
            TextScroll[8] = 720 + 550;
            TextScroll[9] = 720 + 600;
            TextScroll[10] = 720 + 700;
            TextScroll[11] = 720 + 750;
            TextScroll[12] = 720 + 850;
            TextScroll[13] = 720 + 900;
            TextScroll[14] = 720 + 950;
            TextScroll[15] = 720 + 1050;
            TextScroll[16] = 720 + 1100;
            TextScroll[17] = 720 + 1150;
            TextScroll[18] = 720 + 1200;
            TextScroll[19] = 720 + 1250;
            TextScroll[20] = 720 + 1300;
            TextScroll[21] = 720 + 1350;
            TextScroll[22] = 720 + 1400;
            TextScroll[23] = 720 + 1450;
            TextScroll[24] = 720 + 1550;
            TextScroll[25] = 720 + 1600;
            TextScroll[26] = 720 + 1650;
            TextScroll[27] = 720 + 1700;
            TextScroll[28] = 720 + 1750;
            TextScroll[29] = 720 + 1800;
            TextScroll[30] = 720 + 1850;
            center.X = game.GraphicsDevice.Viewport.Width;
            center.Y = game.GraphicsDevice.Viewport.Height;
            center.X = center.X / 2;
            center.Y = center.Y / 2;
            TextPos[0].X = center.X - 47.5f;
            TextPos[0].Y = 438;
            framePersSec = 15;
            TimeperFrame = (float)1 / framePersSec;
            frame = 0;
            totalElapsed = 0;
        }
    }
}
