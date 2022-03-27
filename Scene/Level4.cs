using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class Level4 : Screen
    {
        GameSystem game;

        int framePersSec,
            NumSFX = 0,
            CharMainPos;

        int[] MoveFloor = new int[12];
        float TimeperFrame,
              Rotation,
              MouseAngle;
        float[] Sec = new float[7],
                TimeSec = new float[20],
                totalEla = new float[5];

        Random R_SFXGhost = new Random();

        int frameIn,
            frameOut,
            N_Text = 1,
            Next_Attack = 0,
            frame_ghost = 0,
            frameWarp,
            GhostHart = 200,
            TotalframeWarp,
            MovementFloor = 0,
            SpeedJump = -100,
            NFloor = 0,
            PFloor = 0;

        Rectangle charRectangle,
                  ItemHolyRec,
                  WarpRectangle;
        Rectangle[] HolyItemRectangle = new Rectangle[20],
                    TexttemRectangle = new Rectangle[1],
                    GhostRectangle2 = new Rectangle[20],
                    GhostRectangle = new Rectangle[20];

        Texture2D NoTexture2D,
                  Sign,
                  TextUI,
                  TextInGame,
                  ButtonTextUI,
                  ItemText,
                  BG,
                  Ghost,
                  FrameBG,
                  Floor_top,
                  Bar_Item_LevelOne,
                  text_begin_sence,
                  line_begin_sence,
                  cr1_begin_sence,
                  cr2_begin_sence,
                  cr3_begin_sence,
                  Warp,
                  arrow,
                  MouseArrow,
                  Holly;

        AnimatedTexture Character_Main;

        Texture2D[] ButtonGame,
                    HPCharBar;

        Vector2 center,
                MousePos,
                floorSpeedPos,
                MoveChar,
                Proj_Holly,
                BananaBulletPos,
                MoveHolly = new Vector2(0, 0),
                origin = new Vector2(0, 0),
                Velocity;

        Vector2[] TextPos,
                  Ghost_Pos2 = new Vector2[20],
                  Ghost_Pos = new Vector2[20];
        SpriteFont font,
                   fontLarge;
        string Textg1 = "0",
               Holy_Text = Convert.ToString(0),
               Text_String = "0",
               Talisman_Text = "0";

        bool[] IsGhostStun = new bool[5],
               TextItem_Visi = new bool[1];

        bool Show_Level4 = false,
             End_Ghost = false,
             Show_WarpMessage = false,
             
             directionChar = false,
             GhostStartAttack = false,
             StartTime = true,
             StartTime_Ghost = false,
             IsJump = false,
             IsPlaySFXRendom = false,
             StopJump = false,
             Jump = true,
             IsRunProj = false,
             IsKeySelectOne = false,
             To_Ending = false,
             IsShowTextUI = false,
             Is_MouseClick = false,
             IsMouseOnButton4 = true,
             IsMouseOnButton5 = true,
             IsWalk = true;

        float elapsed;

        private const float Rotation_charmain = 0,
                            Scale_charmain = 1.0f,
                            Depth_charmain = 0.5f;

        int FramesChar = 5,
            FramesRowChar = 1,
            FramesPerSecChar = 10;

        //ตัวแปรของขวดน้ำมนต์
        float v_Gravity = 8,
              FilpRotation,
              Move_Holly = 0,
              angle_holly = 0;
        float v_Speed = 8;
        float g = 10;
        bool[] Holy_Visi = new bool[5],
               IsBack2 = new bool[20],
               IsPlayGhostFootSPX = new bool[20],
               IsPlayGhostHandSPX = new bool[20],
               GhostHartAttack = new bool[20],
               GhostHartAttack2 = new bool[20],
               IsBack = new bool[20];

        public bool IsMouseOnButton = true;
        public bool IsMouseClick = true;
        public bool IsMouseOnButton3 = true;

        public static bool IsMouseClick3 = true,
                           Ghost_Attack = false;

        KeyboardState keyboardState;
        KeyboardState old_keyboardState;

        public bool IsPlayMusicCutSence = true; 

        public Level4(GameSystem game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Setting
            Sec[6] = 249;
            TextPos = new Vector2[16];
            CharMainPos = 576;
            for (int i = 0; i < 4; i++)
            {
                Holy_Visi[i] = false;
            }
            for(int i = 0; i < 20; i++)
            {
                IsPlayGhostFootSPX[i] = true;
                IsPlayGhostHandSPX[i] = true;
                Ghost_Pos[i].Y = -552;
                Ghost_Pos2[i].Y = -552;
            }
            IsGhostStun[0] = false;
            MoveChar.Y = ((720 - 64) - (64 * 5.2f));
            MoveChar.X = -576;
            ButtonGame = new Texture2D[6];
            HPCharBar = new Texture2D[6];
            Character_Main = new AnimatedTexture(Vector2.Zero, Rotation_charmain, Scale_charmain, Depth_charmain);
            center.X = game.GraphicsDevice.Viewport.Width;
            center.Y = game.GraphicsDevice.Viewport.Height;
            center.X = center.X / 2;
            center.Y = center.Y / 2;
            Proj_Holly.X = center.X;
            Proj_Holly.Y = center.Y;
            TextPos[0].X = center.X - 47.5f;
            TextPos[0].Y = 438;
            MoveFloor[0] = 0;
            framePersSec = 15;
            TimeperFrame = (float)1 / framePersSec;
            frameIn = 0;
            frameOut = 0;
            TextItem_Visi[0] = false;

            //LoadContent
            fontLarge = game.Content.Load<SpriteFont>("LargeFont");
            TextUI = game.Content.Load<Texture2D>("TextUI");
            TextInGame = game.Content.Load<Texture2D>("TextInGame");
            ButtonTextUI = game.Content.Load<Texture2D>("ButtonTextUI");
            ItemText = game.Content.Load<Texture2D>("ItemText");
            BG = game.Content.Load<Texture2D>("BG_Level4");
            NoTexture2D = game.Content.Load<Texture2D>("No_Texture");
            Sign = game.Content.Load<Texture2D>("Sign");
            HPCharBar[0] = game.Content.Load<Texture2D>("HP01");
            HPCharBar[1] = game.Content.Load<Texture2D>("HP02");
            HPCharBar[3] = game.Content.Load<Texture2D>("GhostHPbar");
            font = game.Content.Load<SpriteFont>("November");
            FrameBG = game.Content.Load<Texture2D>("Frame");
            ButtonGame[0] = game.Content.Load<Texture2D>("Next_button");
            ButtonGame[1] = game.Content.Load<Texture2D>("Warp_Message");
            ButtonGame[2] = game.Content.Load<Texture2D>("Hide_message");
            Ghost = game.Content.Load<Texture2D>("Ghost_Level4");
            text_begin_sence = game.Content.Load<Texture2D>("beta1l4");
            line_begin_sence = game.Content.Load<Texture2D>("beta05");
            cr1_begin_sence = game.Content.Load<Texture2D>("S1L2");
            Floor_top = game.Content.Load<Texture2D>("Floor");
            Holly = game.Content.Load<Texture2D>("Holy Water");
            arrow = game.Content.Load<Texture2D>("arrow");
            Warp = game.Content.Load<Texture2D>("Warp");
            Bar_Item_LevelOne = game.Content.Load<Texture2D>("Bar_Item");
            MouseArrow = game.Content.Load<Texture2D>("MouseArrow");
            Character_Main.Load(game.Content, "Shaman", FramesChar, FramesRowChar, FramesPerSecChar);
            Character_Main.Pause();
            this.game = game;
        }
        public override void UpdateGame(GameTime theTime)
        {

            FilpRotation = -(Rotation);
            frameIn = game.FrameTransitionIn;
            frameOut = game.FrameTransitionOut;
            MousePos = game.MousePosition;
            elapsed = (float)theTime.ElapsedGameTime.TotalSeconds;
            charRectangle = new Rectangle((int)CharMainPos + Convert.ToInt32(MoveChar.X), (int)Convert.ToInt32(MoveChar.Y), 128, 200);
            
            Ghost_Time(elapsed);
            Character_Main.UpdateFrame(elapsed);
            game.GhostPos = -192 + (game.SpeedGhostLevel1 / 2);
            if (StopJump == false)
            {
                MoveChar.Y += Velocity.Y;
            }
            MusicLevel();
            Key_Control();
            setFloorPos();
            WarpAnima();
            if (IsRunProj == true)
            {
                ItemHolyRec = new Rectangle(Convert.ToInt32((Proj_Holly.X - 16) + MoveHolly.X), Convert.ToInt32((Proj_Holly.Y + 100) + MoveHolly.Y), 32, 32);
            }
            TimeS1((int)theTime.ElapsedGameTime.TotalSeconds);
            Holy_Text = Convert.ToString(game.Holy_Num);
            Textg1 = Convert.ToString(Next_Attack + " <> " + (IsBack[0] == true && Ghost_Pos[0].Y > -552) + " <> " + Next_Attack + "<>" + (WarpRectangle.Intersects(charRectangle) == true));

            if (game.FrameTransitionIn > 6 && game.IsPauseMenu == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                EventScreen.Invoke(game.mPauseDiedMenu, new EventArgs());

            }
            if (game.FrameTransitionIn > 6 && game.IsDiedMenu == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                EventScreen.Invoke(game.mPauseDiedMenu, new EventArgs());

            }
            if (game.FrameTransitionIn > 6 && To_Ending == true)
            {
                game.PlayTransitionIn = false;
                EventScreen.Invoke(game.mEndingScene, new EventArgs());

            }
            Text_String = Convert.ToString(game.Text_Num);
            base.UpdateGame(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            if (game.FrameTransitionIn > 6 && Show_Level4 == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                Show_Level4 = false;
                StartTime = false;
            }
            if (StartTime == true)
            {
                if (IsPlayMusicCutSence == true && game.IsPlayMusic == true)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(game.MusicCutScene[0]);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.5f;
                    IsPlayMusicCutSence = false;
                }
                CutSenceFour(theBatch);
            }
            else
            {
                Level_4(theBatch);
            }


            //Frame
            theBatch.Draw(FrameBG, new Vector2(0, 0), null, Color.White);

            //Test
            theBatch.DrawString(font, Textg1, new Vector2(20, 600), Color.White);
            base.Draw(theBatch);
        }
        public void CutSenceFour(SpriteBatch spriteBatch)
        {
            //Sence1
            if (Sec[0] > 0)
            {
                game.PlayJumpScareEffact = false;
                if (game.IsPlaySFX == true && game.IsPlaySFXCutSence == true)
                {
                    game.CutSenceSFX[0].Play();
                    SoundEffect.MasterVolume = 0.4f;
                    game.IsPlaySFXCutSence = false;
                }
                game.Level = 4;
                
                spriteBatch.Draw(line_begin_sence, new Vector2(275, 0), new Rectangle(0, 0, 166, 811), Color.White);
                spriteBatch.Draw(text_begin_sence, new Vector2(22, 54), new Rectangle(1281, 0, 427, 56), Color.White);
            }

            if (Sec[0] > 10)
            {
                //Sence3
                spriteBatch.Draw(cr1_begin_sence, new Vector2(918, 215), new Rectangle(0, 0, 376, 410), Color.White);
            }

            if (Sec[0] > 5)
            {
                //Sence2
                spriteBatch.Draw(cr1_begin_sence, new Vector2(435, 150), new Rectangle(376, 0, 376, 410), Color.White);
                spriteBatch.Draw(line_begin_sence, new Vector2(825, 0), new Rectangle(0, 0, 166, 811), Color.White);
                spriteBatch.Draw(text_begin_sence, new Vector2(455, 547), new Rectangle(1281, 56, 427, 56), Color.White);
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
                            game.IsplayMusicGAttack = true;
                            SoundEffect.MasterVolume = 1.0f;
                            game.ButtonSFX[1].Play();
                            IsMouseClick = false;
                        }
                        MediaPlayer.Stop();
                        game.IsplayMusicLevel = true;
                        game.PlayTransitionIn = true;
                        Show_Level4 = true;
                    }
                    spriteBatch.Draw(ButtonGame[0], new Vector2(1027, 640), new Rectangle(189, 0, 189, 46), Color.White);
                }
                else
                {
                    spriteBatch.Draw(ButtonGame[0], new Vector2(1027, 640), new Rectangle(0, 0, 189, 46), Color.White);
                }
            }


        }
        public void Level_4(SpriteBatch spriteBatch)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.IsPauseMenu = true;
                game.IsDiedMenu = false;
                game.PlayTransitionIn = true;
            }
            //Draw
            spriteBatch.Draw(BG, new Vector2((-1 * (floorSpeedPos.X)), 0), null, Color.White);

            //ป้ายบอกจุดวาร์ป
            spriteBatch.Draw(Sign, new Vector2(1000 - floorSpeedPos.X, (720 - 64) - (64 * 3.5f)), new Rectangle(0, 0, 117, 89), Color.White);

            WarpRectangle = new Rectangle(Convert.ToInt32(1400 - floorSpeedPos.X), 176, 282, 348);
            if (End_Ghost == false)
            {
                spriteBatch.Draw(Warp, new Vector2(1400 - floorSpeedPos.X, 176), new Rectangle(0, 0, 282, 348), Color.White);
            } else
            {
                spriteBatch.Draw(Warp, new Vector2(1400 - floorSpeedPos.X, 176), new Rectangle(282 * frameWarp, 0, 282, 348), Color.White);
            }

            if (WarpRectangle.Intersects(charRectangle) == true)
            {
                Show_WarpMessage = true;
                GhostStartAttack = true;
            }
            else
            {
                Show_WarpMessage = false;
            }
            if (Show_WarpMessage == true && End_Ghost == true)
            {
                spriteBatch.Draw(ButtonGame[1], new Vector2(545.5f + MoveChar.X, MoveChar.Y-32), Color.White);
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (game.IsPlaySFX == true && To_Ending == false)
                    {
                        game.SoundSFX[13].Play();
                        SoundEffect.MasterVolume = 0.4f;
                    }
                    game.IsplayMusicGAttack = true;
                    game.IsPlaySFXCutSence = true;
                    game.PlayTransitionIn = true;
                    To_Ending = true;
                }
            }

            if (game.IsShowChar == true)
            {
                Character_Main.DrawFrame(spriteBatch, new Vector2(CharMainPos + MoveChar.X, MoveChar.Y), directionChar);
            }
            else
            {
                IsKeySelectOne = false;
            }


            //ไอเทมตามแมพ
            ItemDrop(300, 0);
            ItemDrop(1150, 1);
            ItemTextDrop(1350, 0);


            if (IsPlaySFXRendom == true)
            {
                Sec[6] += 1;
                if(Sec[6] >= 250)
                {
                    NumSFX = R_SFXGhost.Next(2);
                    if (NumSFX == 0)
                    {
                        if (game.IsPlaySFX == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[16].Play();
                        }
                    }
                    Sec[6] = 0;
                }
                
            }
            

            //รอบ 1
            if (Next_Attack >= 0 && GhostStartAttack == true)
            {
                game.PlayJumpScareEffact = true;
                IsPlaySFXRendom = true;
                if (game.IsPlayMusic == true && game.IsplayMusicGAttack == true)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(game.MusicGhostAttack);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 1f;
                    game.IsplayMusicGAttack = false;
                }
                GhostAttack_Foot(1200, 0, false);
            }
            //รอบ 2
            if (Next_Attack >= 1)
            {
                GhostAttack_Hand(300, 0);
            }
            //รอบ 3
            if (Next_Attack >= 2)
            {
                GhostAttack_Foot(0, 1, true);
                GhostAttack_Foot(500, 2, false);
            }
            //รอบ 4
            if (Next_Attack >= 4)
            {
                GhostAttack_Hand(1000, 1);
            }
            //รอบ 5
            if (Next_Attack >= 5)
            {
                GhostAttack_Foot(700, 3, false);
            }
            if (Next_Attack >= 5)
            {
                GhostAttack_Hand(300, 2);
            }
            //รอบ 6
            if (Next_Attack >= 7)
            {
                GhostAttack_Foot(350, 4, true);
            }
            if (Next_Attack >= 8)
            {
                GhostAttack_Hand(0, 3);
            }
            if (Next_Attack >= 8)
            {
                GhostAttack_Foot(1300, 5, false);
            }
            //รอบ7
            if (Next_Attack >= 10)
            {
                GhostAttack_Foot(750, 6, false);
            }
            if (Next_Attack >= 10)
            {
                GhostAttack_Hand(100, 5);
            }
            //รอบ8
            if (Next_Attack >= 12)
            {
                GhostAttack_Foot(500, 7, true);
            }
            //รอบ9
            if (Next_Attack >= 13)
            {
                GhostAttack_Foot(1300, 8, false);
            }
            if (Next_Attack >= 13)
            {
                GhostAttack_Hand(900, 6);
            }
            //รอบ 10
            if (Next_Attack >= 15)
            {
                GhostAttack_Foot(0, 9, false);
            }
            if (Next_Attack >= 15)
            {
                GhostAttack_Hand(450, 7);
            }
            if (Next_Attack >= 14)
            {
                GhostAttack_Foot(900, 10, true);
            }
       
            //รอบ 11
            if (Next_Attack >= 18)
            {
                GhostAttack_Foot(1300, 11, false);
            }
            if (Next_Attack >= 18)
            {
                GhostAttack_Hand(250, 8);
            }
            if (Next_Attack >= 18)
            {
                GhostAttack_Hand(550, 9);
            }

            SystemFloorMove();
            floor();

            if (Next_Attack <= 17)
            {
                //รอบ 1
                if (Next_Attack >= 0 && GhostStartAttack == true)
                {
                    Ghost_Warn(true, false, 0, 1200);
                }
                //รอบ 2
                if (Next_Attack >= 1)
                {
                    Ghost_Warn(false, true, 0, 300);
                }
                //รอบ 3
                if (Next_Attack >= 2)
                {
                    Ghost_Warn(true, false, 1, 0);
                    Ghost_Warn(true, false, 2, 500);
                }
                //รอบ 4
                if (Next_Attack >= 4)
                {
                    Ghost_Warn(false, true, 1, 1000);
                }
                //รอบ 5
                if (Next_Attack >= 5)
                {
                    Ghost_Warn(true, false, 3, 700);
                }
                if (Next_Attack >= 5)
                {
                    Ghost_Warn(false, true, 2, 300);
                }
                //รอบ 6
                if (Next_Attack >= 7)
                {
                    Ghost_Warn(true, false, 4, 350);
                }
                if (Next_Attack >= 8)
                {
                    Ghost_Warn(false, true, 3, 0);
                }
                if (Next_Attack >= 8)
                {
                    Ghost_Warn(true, false, 5, 1300);
                }
                //รอบ 7
                if (Next_Attack >= 10)
                {
                    Ghost_Warn(true, false, 6, 750);
                }
                if (Next_Attack >= 10)
                {
                    Ghost_Warn(false, true, 5, 100);
                }
                //รอบ 8
                if (Next_Attack >= 12)
                {
                    Ghost_Warn(true, false, 7, 500);
                }
                //รอบ 9
                if (Next_Attack >= 13)
                {
                    Ghost_Warn(true, false, 8, 1300);
                }
                if (Next_Attack >= 13)
                {
                    Ghost_Warn(false, true, 6, 900);
                }
                //รอบ 10
                if (Next_Attack >= 15)
                {
                    Ghost_Warn(true, false, 9, 0);
                }
                if (Next_Attack >= 15)
                {
                    Ghost_Warn(false, true, 7, 450);
                }
                if (Next_Attack >= 14)
                {
                    Ghost_Warn(true, false, 10, 900);
                }
            }
            //รอบ 11
            if (Next_Attack >= 18)
            {
                Ghost_Warn(true, false, 11, 1300);
            }
            if (Next_Attack >= 18)
            {
                Ghost_Warn(false, true, 8, 250);
            }
            if (Next_Attack >= 18)
            {
                Ghost_Warn(false, true, 9, 550);
                game.CharHart = 0;
            }

            //Draw ขวดน้ำมนต์
            if(IsRunProj == false)
            {
                Proj_Holly = new Vector2(-10000, -10000);
            }
            if (game.Holy_Num == 0)
            {
                IsRunProj = false;
                IsKeySelectOne = false;

            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && IsRunProj == false && IsKeySelectOne == true)
            {
                Sec[3] = 0;
                IsRunProj = true;
                Proj_Holly.Y = MoveChar.Y;
                Proj_Holly.X = MoveChar.X + CharMainPos + 64;
                if (game.IsPlaySFX == true)
                {
                    SoundEffect.MasterVolume = 1.0F;
                    game.SoundSFX[6].Play();
                }
                game.IsPlaySFXGhostSee = true;
                if (MousePos.X > (MoveChar.X + CharMainPos + 64) && MousePos.X <= 1280)
                {
                    angle_holly = (Rotation) * 5;
                    v_Speed = v_Gravity - (Rotation * 2.8f);
                }
                else { }
                if (MousePos.X >= 0 && MousePos.X <= (MoveChar.X + CharMainPos + 64))
                {
                    angle_holly = (FilpRotation) * -5;
                    v_Speed = v_Gravity - (FilpRotation * 2.8f);
                }
                else { }
                g = 12;

            }
            else
            {

            }
            if (IsRunProj == true && game.Holy_Num > 0)
            {
                Sec[3] += 1;
                HolyWater(spriteBatch);
            }
            if (IsKeySelectOne == true && game.Holy_Num > 0)
            {
                Arrow_HolyWater(spriteBatch);
            }
            else { }

            //Item
            ItemInGame(spriteBatch);

            //หัวใจ
            HartChar(spriteBatch);
            HartGhost(spriteBatch);
            TextUIShow();

            //เช็ดฉาก
            void floor()
            {
                int height = 234,
                    Wideth = 135;
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[0], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[1], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[2], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[3], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[4], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[5], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[6], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[7], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[8], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[9], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);
                spriteBatch.Draw(Floor_top, new Vector2(MoveFloor[10], (720 - height)), new Rectangle(0, 0, Wideth, height), Color.White);

            }
            void ItemDrop(int Position, int i)
            {
                if (charRectangle.Intersects(HolyItemRectangle[i]) == true && Holy_Visi[i] == false)
                {
                    game.Holy_Num += 15;
                    Holy_Visi[i] = true;
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[4].Play();
                    }
                }
                else
                {
                    if (Holy_Visi[i] == false)
                    {
                        spriteBatch.Draw(Holly, new Vector2(Position - floorSpeedPos.X, (720 - 64) - (64 * 3.4f)), null, Color.White);
                        HolyItemRectangle[i] = new Rectangle(Convert.ToInt32(Position - floorSpeedPos.X), Convert.ToInt32((720 - 64) - (64 * 3.4f)), 34, 34);
                    }

                }

            }
            void GhostAttack_Foot(int Position, int i, bool isAttack)
            {
                if(IsBack[i] == true && Ghost_Pos[i].Y > -552)
                {
                    Ghost_Pos[i].Y -= 4;
                } else
                {
                    if(Ghost_Pos[i].Y > 0)
                    {
                        if(game.IsPlaySFX == true && IsPlayGhostFootSPX[i] == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[17].Play();
                            IsPlayGhostFootSPX[i] = false;
                        }
                        if (isAttack == false)
                        {
                            Sec[3]++;
                            if (Sec[3] > 60)
                            {
                                Sec[3] = 0;
                                IsBack[i] = true;
                                if(End_Ghost == false)
                                {
                                    Next_Attack++;
                                }
                            }
                        }
                        else
                        {
                            Sec[5]++;
                            if (Sec[5] > 120)
                            {
                                Sec[5] = 0;
                                IsBack[i] = true;
                                Next_Attack++;
                            }
                        }
                    } else
                    {
                        Ghost_Pos[i].Y += 4;
                    }
                }
                GhostRectangle[i] = new Rectangle(Convert.ToInt32((Position + 26) - floorSpeedPos.X), Convert.ToInt32(Ghost_Pos[i].Y - 50), 500, 552);
                spriteBatch.Draw(Ghost, new Vector2(Position - floorSpeedPos.X, Ghost_Pos[i].Y), new Rectangle(0, 0, 552, 552), Color.White);
                if(GhostRectangle[i].Intersects(charRectangle) == true)
                {
                    TimeSec[i]++;
                    if(TimeSec[i] > 15)
                    {
                        TimeSec[i] = 0;
                        game.CharHart -= 10;
                    }
                }
                if(GhostRectangle[i].Intersects(ItemHolyRec) == true)
                {
                    if (GhostHartAttack[i] == false)
                    {
                        if (game.IsPlaySFX == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[5].Play();
                        }
                        game.Holy_Num -= 1;
                        if (isAttack == true)
                        {
                            
                            GhostHart -= 5;
                        } 
                        GhostHart -= 10;
                        GhostHartAttack[i] = true;
                    }
                    IsRunProj = false;
                    
                } else
                {
                    GhostHartAttack[i] = false;
                }
            }
            void GhostAttack_Hand(int Position, int i)
            {
                if (IsBack2[i] == true && Ghost_Pos2[i].Y > -552)
                {
                    Ghost_Pos2[i].Y -= 4;
                }
                else
                {
                    if (Ghost_Pos2[i].Y > 0)
                    {
                        if (game.IsPlaySFX == true && IsPlayGhostHandSPX[i] == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[18].Play();
                            IsPlayGhostHandSPX[i] = false;
                        }
                        Sec[4]++;
                        if (Sec[4] > 60)
                        {
                            Sec[4] = 0;
                            IsBack2[i] = true;
                            if (End_Ghost == false)
                            {
                                Next_Attack++;
                            }
                        }
                    }
                    else
                    {
                        Ghost_Pos2[i].Y += 4;
                    }
                }
                GhostRectangle2[i] = new Rectangle(Convert.ToInt32((Position + 15) - floorSpeedPos.X), Convert.ToInt32(Ghost_Pos2[i].Y - 50), 260, 552);
                spriteBatch.Draw(Ghost, new Vector2(Position - floorSpeedPos.X, Ghost_Pos2[i].Y), new Rectangle(552, 0, 290, 552), Color.White);
                if (GhostRectangle2[i].Intersects(charRectangle) == true)
                {
                    TimeSec[i]++;
                    if (TimeSec[i] > 15)
                    {
                        TimeSec[i] = 0;
                        game.CharHart -= 5;
                    }
                }
                if (GhostRectangle2[i].Intersects(ItemHolyRec) == true)
                {
                    if(GhostHartAttack2[i] == false)
                    {
                        if (game.IsPlaySFX == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[5].Play();
                        }
                        game.Holy_Num -= 1;
                        GhostHartAttack2[i] = true;
                        GhostHart -= 20;
                    }
                    IsRunProj = false;
                    
                }
                else
                {
                    GhostHartAttack2[i] = false;
                }
            }
            void Ghost_Warn(bool foot, bool hand, int i, int Position)
            {
                if (IsBack[i] == false)
                {
                    if (foot == true)
                    {
                        spriteBatch.Draw(Ghost, new Vector2(Position - floorSpeedPos.X, 475), new Rectangle(552 * frame_ghost, 552, 552, 56), Color.White);
                    }
                }
                if(IsBack2[i] == false) 
                { 
                    if (hand == true)
                    {
                        spriteBatch.Draw(Ghost, new Vector2(Position - floorSpeedPos.X, 475), new Rectangle(290 * frame_ghost, 608, 290, 56), Color.White);
                    }
                }
            }
            void ItemTextDrop(int Position, int i)
            {
                if (charRectangle.Intersects(TexttemRectangle[i]) == true && TextItem_Visi[i] == false)
                {
                    game.Text_Num += 1;
                    TextItem_Visi[i] = true;
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[7].Play();
                    }
                }
                else
                {
                    if (TextItem_Visi[i] == false)
                    {
                        spriteBatch.Draw(ItemText, new Vector2(Position - floorSpeedPos.X, (720 - 64) - (64 * 3.4f)), null, Color.White);
                        TexttemRectangle[i] = new Rectangle(Convert.ToInt32(Position - floorSpeedPos.X), Convert.ToInt32((720 - 64) - (64 * 3.4f)), 34, 34);
                    }

                }

            }
            void TextUIShow()
            {
                if (IsShowTextUI == true)
                {
                    IsKeySelectOne = false;
                    game.IsGamePause = true;
                    spriteBatch.Draw(TextUI, new Vector2(center.X - 450, center.Y - 250), new Rectangle(0, 0, 900, 500), Color.White);
                    spriteBatch.Draw(TextUI, new Vector2(1058, 110), new Rectangle(868, 0, 32, 32), Color.White);
                    spriteBatch.DrawString(fontLarge, Convert.ToString(N_Text), new Vector2(406, 412), Color.White);
                    spriteBatch.Draw(TextInGame, new Vector2(530, 130), new Rectangle(462 * (N_Text - 1), 0, 462, 462), Color.White);


                    //ปุ่มปิด
                    if (MousePos.X >= 1058 && MousePos.X <= 1090 && MousePos.Y >= 110 && MousePos.Y <= 142)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton3 == true)
                        {
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton3 = false;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            //เสียงปุ่ม
                            if (game.IsPlaySFX == true && IsMouseClick3 == true)
                            {
                                SoundEffect.MasterVolume = 1.0f;
                                game.ButtonSFX[1].Play();
                                IsMouseClick3 = false;
                            }
                            game.IsGamePause = false;
                            IsShowTextUI = false;
                        }
                        else
                        {
                            IsMouseClick3 = true;
                        }
                        spriteBatch.Draw(TextUI, new Vector2(1058, 110), new Rectangle(900, 0, 32, 32), Color.White);
                    }
                    else
                    {
                        IsMouseOnButton3 = true;
                    }

                    //ปุ่มก่อนหน้า
                    if (MousePos.X >= 277 && MousePos.X <= 373 && MousePos.Y >= 466 && MousePos.Y <= 494)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton4 == true)
                        {
                            SoundEffect.MasterVolume = 1.0f;
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton4 = false;
                        }
                        spriteBatch.Draw(ButtonTextUI, new Vector2(277f, 466), new Rectangle(0, 28, 96, 28), Color.White);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Is_MouseClick == true)
                        {
                            Is_MouseClick = false;
                            N_Text -= 1;

                            if (N_Text < 1)
                            {
                                N_Text = game.Text_Num;
                            }
                            //เสียงปุ่ม
                            if (game.IsPlaySFX == true)
                            {
                                game.ButtonSFX[1].Play();
                            }
                        }
                        else
                        {
                            if (Mouse.GetState().LeftButton == ButtonState.Released)
                            {
                                Is_MouseClick = true;
                            }
                        }
                    }
                    else
                    {
                        spriteBatch.Draw(ButtonTextUI, new Vector2(277f, 466), new Rectangle(0, 0, 96, 28), Color.White);
                        IsMouseOnButton4 = true;
                    }
                    //ปุ่มถัดไป
                    if (MousePos.X >= 373 && MousePos.X <= 469 && MousePos.Y >= 466 && MousePos.Y <= 494)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton5 == true)
                        {
                            SoundEffect.MasterVolume = 1.0f;
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton5 = false;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Is_MouseClick == true)
                        {
                            Is_MouseClick = false;
                            N_Text += 1;

                            if (N_Text >= game.Text_Num + 1)
                            {
                                N_Text = 1;
                            }
                            //เสียงปุ่ม
                            if (game.IsPlaySFX == true)
                            {
                                game.ButtonSFX[1].Play();
                            }
                        }
                        else
                        {
                            if (Mouse.GetState().LeftButton == ButtonState.Released)
                            {
                                Is_MouseClick = true;
                            }
                        }
                        spriteBatch.Draw(ButtonTextUI, new Vector2(373f, 466), new Rectangle(96, 28, 96, 28), Color.White);
                    }
                    else
                    {
                        IsMouseOnButton5 = true;
                        spriteBatch.Draw(ButtonTextUI, new Vector2(373f, 466), new Rectangle(96, 0, 96, 28), Color.White);
                    }

                }

            }
        }
        public void WarpAnima()
        {
            TotalframeWarp += 1;
            if (TotalframeWarp > 5)
            {
                frameWarp += 1;
                TotalframeWarp = 0;
            }
            if (frameWarp >= 9)
            {
                frameWarp = 0;
            }
        }
        //ระบบเสียง
        void MusicLevel()
        {
            if (game.IsplayMusicLevel == true && game.IsPlayMusic == true)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(game.MusicLevel);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 1f;
                game.IsplayMusicLevel = false;
            }
        }
        //ระบบหัวใจ
        public void HartChar(SpriteBatch spriteBatch)
        {
            if (game.IsGamePause == false)
            {
                if (ItemHolyRec.Intersects(charRectangle) == true)
                {
                    if (Sec[3] > 60)
                    {
                        game.CharHart = 100;
                        Sec[3] = 0;
                        IsRunProj = false;
                        MoveHolly.X = 0;
                        game.Holy_Num -= 1;
                        if (game.IsPlaySFX == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[5].Play();
                        }
                    }
                }
                if (game.CharHart <= 0)
                {
                    MediaPlayer.Stop();
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[12].Play();
                    }
                    PauseAndDiedMenu.IsPlayMusicPauseMenu = true;
                    BananaBulletPos.X = 0;
                    BananaBulletPos.Y = 0;
                    Sec[2] = 0;
                    game.Holy_Num = 30;
                    game.IsGamePause = true;
                    game.IsGhost_See = false;
                    game.IsDiedMenu = true;
                    game.PlayTransitionIn = true;
                    Reset_Level(576, 0, 0, -576);
                }
                spriteBatch.Draw(HPCharBar[1], new Vector2(1280 - 250, 50), null, Color.White);
                spriteBatch.Draw(HPCharBar[0], new Vector2(1280 - 250, 50), new Rectangle(0, 0, game.CharHart * 2, 32), Color.White);
            }
        }
        void HartGhost(SpriteBatch spriteBatch)
        {
            if(GhostHart <= 0)
            {
                game.PlayJumpScareEffact = false;
                IsPlaySFXRendom = false;
                End_Ghost = true;
                game.IsplayMusicLevel = true;
            }
            if (GhostStartAttack == true && End_Ghost == false)
            {
                spriteBatch.Draw(HPCharBar[3], new Vector2(485, 50), new Rectangle(0, 32, 400, 32), Color.White);
                spriteBatch.Draw(HPCharBar[3], new Vector2(485, 50), new Rectangle(0, 0, GhostHart * 2, 32), Color.White);

            }
            
        }

        //ปุ่มควบคุม
        public void Key_Control()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D2) && old_keyboardState.IsKeyUp(Keys.D2))
            {
                IsShowTextUI = true;
            }
            //Draw ลูกศร
            if (keyboardState.IsKeyDown(Keys.D1) && game.Holy_Num > 0 && MoveChar.X == 0 && game.IsShowChar == true)
            {
                if (IsKeySelectOne == false)
                {
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[3].Play();
                    }
                    IsKeySelectOne = true;
                    MoveHolly.X = 0;
                }
                else
                {
                    IsKeySelectOne = false;
                }

            }

            //เดินไปข้างหน้า
            if (keyboardState.IsKeyDown(Keys.D))
            {
                if (MoveChar.X > 575)
                {
                    Character_Main.Pause(0, 0);
                }
                else
                {
                    if (Sec[4] == 0.1f && game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 0.5f;
                        game.SoundSFX[0].CreateInstance().Play();
                    }
                    if (Sec[4] >= 3.3f)
                    {
                        Sec[4] = 0.0f;
                    }
                    Sec[4] += 0.1f;
                    Character_Main.Play();
                }

                directionChar = false;

                if (floorSpeedPos.X < 500 && MoveChar.X == 0)
                {
                    game.IsShowChar = true;
                    if (IsRunProj == true)
                    {
                        MoveHolly.X -= 4;
                    }

                    if (Proj_Holly.Y >= 380)
                    {
                        MoveHolly.X = 0;
                    }
                    MovementFloor = -4;
                    FloorMoveMent();
                    floorSpeedPos.X = floorSpeedPos.X + 4;
                }
                else
                {

                    if (MoveChar.X != 576)
                    {
                        MoveChar.X = MoveChar.X + 4;
                        if (IsRunProj != true)
                        {
                            Move_Holly += 4;
                        }

                    }

                }


            }
            else
            {
                if (old_keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.D))
                {
                    MovementFloor = 0;
                    Character_Main.Pause(0, 0);
                    
                    Sec[4] = 0.0f;
                }
                if (IsWalk == false)
                {
                    Character_Main.Pause(0, 0);
                }
            }

            //เดินกลับหลัง
            if (keyboardState.IsKeyDown(Keys.A))
            {
                if (MoveChar.X < -575)
                {
                    Character_Main.Pause(0, 0);
                }
                else
                {
                    if (Sec[4] == 0.1f && game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 0.5f;
                        game.SoundSFX[0].CreateInstance().Play();
                    }
                    if (Sec[4] >= 3.3f)
                    {
                        Sec[4] = 0.0f;
                    }
                    Sec[4] += 0.1f;
                    Character_Main.Play();
                }
                directionChar = true;
                if (floorSpeedPos.X > 0 && MoveChar.X == 0)
                {
                    game.IsShowChar = true;
                    MovementFloor = +4;
                    FloorMoveMent();
                    if (Proj_Holly.Y >= 380)
                    {
                        MoveHolly.X = 0;
                    }
                    if (IsRunProj == true)
                    {
                        MoveHolly.X += 4;
                    }
                    floorSpeedPos.X = floorSpeedPos.X - 4;
                }
                else
                {
                    if (MoveChar.X != -576)
                    {
                        MoveChar.X = MoveChar.X - 4;
                        if (IsRunProj != true)
                        {
                            Move_Holly -= 4;
                        }
                    }
                }

            }
            else
            {
                if (old_keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A))
                {
                    MovementFloor = 0;
                    
                    Sec[4] = 0.0f;
                    Character_Main.Pause(0, 0);
                }
                if (IsWalk == false)
                {
                    Character_Main.Pause(0, 0);
                }

            }

            //กระโดด
            if (MoveChar.Y > 300f && StopJump == true)
            {
                IsJump = false;
            }
            else
            {
                if (game.IsPlaySFX == true && StopJump == true)
                {
                    SoundEffect.MasterVolume = 1.0f;
                    game.SoundSFX[2].Play();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Jump == true)
            {
                if (game.IsPlaySFX == true)
                {
                    SoundEffect.MasterVolume = 1.0f;
                    game.SoundSFX[1].Play();
                }
                if (StopJump == false)
                {
                    v_Gravity = 6;
                }
                game.IsShowChar = true;
                Jump = false;
                MoveChar.Y += SpeedJump;
                Velocity.Y = -3.5f;
                IsJump = true;
            }

            if (IsJump == true)
            {
                if (StopJump == false)
                {
                    Velocity.Y += 0.5f;
                }
                else
                {
                    v_Gravity = 7;
                    Jump = true;
                    SpeedJump = -50;
                }
            }
            if (IsJump == false)
            {
                Velocity.Y = 0f;
                MoveChar.Y = ((720 - 64) - (64 * 5.2f));
            }
            if (MoveChar.Y + 200 >= 525)
            {
                if (game.IsPlaySFX == true)
                {
                    SoundEffect.MasterVolume = 1.0f;
                    game.SoundSFX[2].Play();
                }
                IsJump = false;
                Jump = true;
                SpeedJump = -100;
                v_Gravity = 8;
            }



            old_keyboardState = keyboardState;
        }

        //ระบบพื้น
        void FloorMoveMent()
        {
            MoveFloor[0] = MoveFloor[0] + MovementFloor;
            MoveFloor[1] = MoveFloor[1] + MovementFloor;
            MoveFloor[2] = MoveFloor[2] + MovementFloor;
            MoveFloor[3] = MoveFloor[3] + MovementFloor;
            MoveFloor[4] = MoveFloor[4] + MovementFloor;
            MoveFloor[5] = MoveFloor[5] + MovementFloor;
            MoveFloor[6] = MoveFloor[6] + MovementFloor;
            MoveFloor[7] = MoveFloor[7] + MovementFloor;
            MoveFloor[8] = MoveFloor[8] + MovementFloor;
            MoveFloor[9] = MoveFloor[9] + MovementFloor;
            MoveFloor[10] = MoveFloor[10] + MovementFloor;
        }
        void setFloorPos()
        {
            if (NFloor != 11)
            {
                NFloor += 1;
                PFloor += 128;
                MoveFloor[NFloor] = PFloor;

            }
            else
            {

            }

        }
        void SystemFloorMove()
        {
            int v_ = -128,
                vx = 1276;
            //เดินหน้า
            for (int i = 0; i < 11; i++)
            {
                if (MoveFloor[i] < v_)
                {
                    MoveFloor[i] = vx;
                }
            }
            //กลับหลัง
            for (int i = 0; i < 11; i++)
            {
                if (MoveFloor[i] > vx)
                {
                    MoveFloor[i] = v_;
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

        //แปลง Double เป็น Int
        public static float ToSingle(double value)
        {
            return (float)value;
        }

        //ระบบปาน้ำมนต์
        void Arrow_HolyWater(SpriteBatch spriteBatch)
        {

            origin = new Vector2(arrow.Width / 2, 50 + (arrow.Height / 2));

            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            MouseAngle = Convert.ToSingle(Math.Atan2(MousePos.Y - (MoveChar.Y + 100), MousePos.X - (MoveChar.X + 640)) + MathHelper.ToRadians(90));
            if (MouseAngle > -1.25f && MouseAngle < 1.25f)
            {
                Rotation = MouseAngle;
            }
            else
            {
                if (MousePos.X < 640)
                {
                    Rotation = -1.25f;
                }
                else
                {
                    Rotation = 1.25f;
                }
            }
            spriteBatch.Draw(arrow, new Vector2(MoveChar.X + 640, MoveChar.Y + 100), sourceRectangle, Color.White, Rotation, origin, 1, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(MouseArrow, new Vector2(MousePos.X - 16, MousePos.Y - 16), null, Color.White);

        }
        void HolyWater(SpriteBatch spriteBatch)
        {

            if (Proj_Holly.Y > 0)
            {

                v_Speed -= 0.1f;
                Proj_Holly.Y -= (v_Speed);
                Proj_Holly.X += angle_holly;
            }
            else
            {
                g -= 1;
                Proj_Holly.Y += g;


            }
            if (Proj_Holly.Y >= 380)
            {
                IsRunProj = false;
                MoveHolly.X = 0;
                game.Holy_Num -= 1;
                Sec[3] = 0;
                if (game.IsPlaySFX == true)
                {
                    SoundEffect.MasterVolume = 1.0F;
                    game.SoundSFX[5].Play();
                }
            }
            if (Proj_Holly.Y <= -1)
            {
                IsRunProj = false;
                MoveHolly.X = 0;
                game.Holy_Num -= 1;
                Sec[3] = 0;
            }
            spriteBatch.Draw(Holly, new Vector2((Proj_Holly.X - 16) + MoveHolly.X, (Proj_Holly.Y + 100) + MoveHolly.Y), null, Color.White);

        }

        //ระบบไอเทม
        void ItemInGame(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Bar_Item_LevelOne, new Vector2(32, 32), null, Color.White);
            spriteBatch.DrawString(font, Holy_Text, new Vector2(76, 112), Color.White);
            spriteBatch.DrawString(font, Talisman_Text, new Vector2(136, 112), Color.White);
            spriteBatch.DrawString(font, Text_String, new Vector2(286, 112), Color.White);
        }

        //ระบบผี
        void Ghost_Time(float elapsed)
        {
            if(Ghost_Attack == true && StartTime_Ghost == true)
            {
                totalEla[1] += 1;
                if (totalEla[1] > 15)
                {
                    Sec[1]++;
                    totalEla[1] = 0;
                }
            } else { }
            totalEla[2] += elapsed;
            if(totalEla[2] > 0.1f)
            {
                frame_ghost += 1;
                totalEla[2] = 0;
            }
            if(frame_ghost > 1)
            {
                frame_ghost = 0;
            }
        }
        

        //รีเซ็ตตำแหน่งผู้เล่นเมื่อเริ่มด่านใหม่
        void Reset_Level(int CharPos, int FloorPos, int SpeedPos, int Movechar)
        {
            MovementFloor = FloorPos;
            floorSpeedPos.X = SpeedPos;
            CharMainPos = CharPos;
            MoveChar.X = Movechar;
            if (NFloor != 11)
            {
                NFloor += 1;
                PFloor += 64;
                MoveFloor[NFloor] = PFloor;

            }
            else
            {

            }
            End_Ghost = false;
            Show_WarpMessage = false;
            Ghost_Attack = false;
            directionChar = false;
            GhostStartAttack = false;
            StartTime_Ghost = false;
            IsJump = false;
            IsPlaySFXRendom = false;
            StopJump = false;
            Jump = true;
            IsRunProj = false;
            IsKeySelectOne = false;
            To_Ending = false;
            IsShowTextUI = false;
            Is_MouseClick = false;
             IsWalk = true;
            N_Text = 1;
            Next_Attack = 0;
            frame_ghost = 0;
            frameWarp = 0;
            GhostHart = 200;
            TotalframeWarp = 0;
            MovementFloor = 0;
            SpeedJump = -100;
            NumSFX = 0;
            game.PlayJumpScareEffact = false;
            for (int i = 0; i < 20; i++)
            {
                IsPlayGhostFootSPX[i] = true;
                IsPlayGhostHandSPX[i] = true;
                Ghost_Pos[i].Y = -552;
                Ghost_Pos2[i].Y = -552;
                IsBack[i] = false;
                IsBack2[i] = false;
            }
            for (int i = 0; i < 7; i++)
            {
                Sec[i] = 0;
            }
            Sec[6] = 249;
            TextItem_Visi[0] = false;

        }
        public void Reset_To_Level()
        {
            floorSpeedPos.X = 0;
            framePersSec = 0;
            CharMainPos = 576;
            TextItem_Visi[0] = false;
            IsGhostStun[0] = false;
            MoveChar.Y = ((720 - 64) - (64 * 5.2f));
            MoveChar.X = 0;
            MoveFloor[0] = 0;
            frame_ghost = 0;
            frameWarp = 0;
            TotalframeWarp = 0;
            MovementFloor = 0;
            SpeedJump = -100;
            NFloor = 0;
            PFloor = 0;
            Textg1 = "0";
            Holy_Text = "0";
            Text_String = "0";
            Talisman_Text = "0";
            Show_Level4 = false;
            IsPlayMusicCutSence = true;
            IsMouseOnButton = true;
            IsMouseClick = true;
            Show_WarpMessage = false;
            directionChar = false;
            StartTime = true;
            IsJump = false;
            StopJump = false;
            IsShowTextUI = false;
            Jump = true;
            IsMouseOnButton3 = true;
            IsMouseClick3 = true;
            IsRunProj = false;
            IsKeySelectOne = false;
            To_Ending = false;
            IsWalk = true;
            frame_ghost = 0;
            N_Text = 1;
            frameWarp = 0;
            TotalframeWarp = 0;
            MovementFloor = 0;
            SpeedJump = -100;
            NFloor = 0;
            PFloor = 0;

            IsPlayMusicCutSence = true;

            IsMouseOnButton = true;
            IsMouseClick = true;

            IsMouseOnButton3 = true;

            IsMouseClick3 = true;
            IsMouseOnButton3 = true;

            IsMouseClick3 = true;
            IsShowTextUI = false;
            Show_WarpMessage = false;
            directionChar = false;
            StartTime = true;
            Is_MouseClick = false;
            IsJump = false;
            StopJump = false;
            Jump = true;
            IsRunProj = false;
            IsKeySelectOne = false;
            IsWalk = true;
            frame_ghost = 0;
            frameWarp = 0;
            N_Text = 1;
            N_Text = 1;
            Next_Attack = 0;
            frame_ghost = 0;
            frameWarp =0;
            GhostHart = 200;
            TotalframeWarp= 0;
            MovementFloor = 0;
            SpeedJump = -100;
            NumSFX = 0;
            for (int i = 0; i < 20; i++)
            {
                IsPlayGhostFootSPX[i] = true;
                IsPlayGhostHandSPX[i] = true;
                Ghost_Pos[i].Y = -552;
                Ghost_Pos2[i].Y = -552;
            }
            Sec[6] = 249;
            for (int i = 0; i < 3; i++)
            {
                Holy_Visi[i] = false;
            }
            for (int i = 0; i < 7; i++)
            {
                Sec[i] = 0;
            }
            for (int i = 0; i < 2; i++)
            {
                totalEla[i] = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                Holy_Visi[i] = false;
            }
            for (int i = 0; i < 1; i++)
            {
                TextItem_Visi[i] = false;
            }
            IsGhostStun[0] = false;
            MoveChar.Y = ((720 - 64) - (64 * 5.2f));
        }
    }
}
