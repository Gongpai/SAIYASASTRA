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
    public class Level2 : Screen
    {
        GameSystem game;
        SpriteFont font,
                   fontLarge;

        int framePersSec,
            CharMainPos;

        int[] MoveFloor = new int[12],
              MoveTree = new int[22];
        float TimeperFrame,
              Rotation,
              MouseAngle,
              Elapsed_Ghost,
              v_X_Ghost = 10,
              v_Y_Ghost = 2;
        float[] Sec = new float[6],
                totalEla = new float[5];

        int frame_ghost,
            N_Text = 1,
            frameWarp,
            Direction_tree = 0,
            GhostWalkBackward = 0,
            Is_HideOut = 0,
            TotalframeWarp,
            MovementFloor = 0,
            MovementTree = 0,
            SpeedJump = -100,
            Talisman_Scrap_Num = 0,
            Talisman_Num = 0,
            NFloor = 0,
            PFloor = 0;

        Rectangle charRectangle,
                  LogBlockRectangle,
                  ItemHolyRec,
                  WarpRectangle,
                  PoleRectangle,
                  BananaBulletRectangle,
                  StumpBlockRectangle;
        Rectangle[] HolyItemRectangle = new Rectangle[6],
                    TalismanRectangle = new Rectangle[6],
                    TexttemRectangle = new Rectangle[2],
                    Talisman_PuzzleRectangle = new Rectangle[10],
                    Talisman_ScrapRectangle = new Rectangle[10],
                    HideOutLevel_1_Rectangle = new Rectangle[3],
                    GhostRectangle = new Rectangle[3];

        Texture2D NoTexture2D,
                  Level2_2BG,
                  Sign,
                  Grass_1,
                  Grass_2,
                  FrameBG,
                  Floor_top,
                  Bar_Item_LevelOne,
                  Banana_Tree,
                  Hide,
                  TextUI,
                  TextInGame,
                  ButtonTextUI,
                  ItemText,
                  text_begin_sence,
                  line_begin_sence,
                  cr1_begin_sence,
                  cr2_begin_sence,
                  cr3_begin_sence,
                  TalismanUI,
                  TalismanPuzzle,
                  Warp,
                  arrow,
                  MouseArrow,
                  Talisman_Sprite,
                  Tree_Talisman_Sprite,
                  Holly,
                  Ghost;

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
        float elapsed;
        Vector2[] TextPos,
                  Talisman_Puzzle_Pos = new Vector2[10];

        string Holy_Text = Convert.ToString(0),
               Talisman_Text = "0";

        string Textg1 = "0",
               Text_String = "0";

        bool[] IsGhostStun = new bool[3],
               IsMouse_Click = new bool[2],
               TextItem_Visi = new bool[2];

        bool Ghost_Shot = false,
             Ghost_Died = false,
             IsShowTextUI = false,
             Show_Level2 = false,
             To_Level3 = false,
             IsTalismanPuzzle_Show = false,
             Show_WarpMessage = false,
             Show_HideOutMessage = false,
             directionChar = false,
             StartTime = true,
             Is_MouseClick = false,
             IsJump = false,
             StopJump = false,
             Jump = true,
             IsRunProj = false,
             IsKeySelectOne = false,
             IsHitLog = true,
             IsHolyInter_Ghost = false,
             IsHitStump = true,
             CanWalk_A = false,
             Canwalk_D = false,
             IsMouseOnButton4 = true,
             IsMouseOnButton5 = true,
             IsWalk = true;

        private const float Rotation_charmain = 0,
                            Scale_charmain = 1.0f,
                            Depth_charmain = 0.5f;

        int FramesChar = 5,
            FramesRowChar = 1,
            FramesPerSecChar = 10;

        KeyboardState keyboardState;
        KeyboardState old_keyboardState;

        //เทส
        float v_Gravity = 8,
              FilpRotation,
              Move_Holly = 0,
              angle_holly = 0;
        float v_Speed = 8;
        float g = 10;
        public static bool[] Show_Ghost = new bool[4];
        bool[] Holy_Visi = new bool[6],
               IsPlaySPFTalismanPuzzle = new bool[10],
               IsPlaySPFTalismanPuzzle2 = new bool[10],
               Talisman_Puzzle_Visi = new bool[10],
               Is_Talisman_Puzzle = new bool[10],
               IS_Talisman_Puzzle_Correct = new bool[10],
               Talisman_Visi = new bool[4];

        bool IsLogSFXJump = true;

        bool IsPlayMusicCutSence = true;

        public bool IsMouseOnButton = true;
        public bool IsMouseClick = true;

        public bool IsMouseOnButton2 = true;

        public bool IsMouseClick2 = true;
        public bool IsMouseOnButton3 = true;

        public bool IsMouseClick3 = true;

        public Level2(GameSystem game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Setting
            Talisman_Puzzle_Pos[0] = new Vector2(690, 180);
            Talisman_Puzzle_Pos[1] = new Vector2(690, 230);
            Talisman_Puzzle_Pos[2] = new Vector2(690, 280);
            Talisman_Puzzle_Pos[3] = new Vector2(789, 180);
            Talisman_Puzzle_Pos[4] = new Vector2(789, 230);
            Talisman_Puzzle_Pos[5] = new Vector2(789, 280);
            Talisman_Puzzle_Pos[6] = new Vector2(892, 180);
            Talisman_Puzzle_Pos[7] = new Vector2(892, 230);
            Talisman_Puzzle_Pos[8] = new Vector2(892, 280);
            TextPos = new Vector2[16];
            CharMainPos = 576;
            setTreePos();
            for (int i = 0; i < 4; i++)
            {
                Holy_Visi[i] = false;
            }
            for (int i = 0; i < 2; i++)
            {
                TextItem_Visi[i] = false;
            }
            IsGhostStun[0] = false;
            MoveChar.Y = ((720 - 64) - (64 * 5.2f));
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

            //LoadContent
            fontLarge = game.Content.Load<SpriteFont>("LargeFont");
            TextUI = game.Content.Load<Texture2D>("TextUI");
            TextInGame = game.Content.Load<Texture2D>("TextInGame");
            ButtonTextUI = game.Content.Load<Texture2D>("ButtonTextUI");
            ItemText = game.Content.Load<Texture2D>("ItemText");
            NoTexture2D = game.Content.Load<Texture2D>("No_Texture");
            TalismanPuzzle = game.Content.Load<Texture2D>("TalismanPuzzle");
            Tree_Talisman_Sprite = game.Content.Load<Texture2D>("TreeTalisman");
            TalismanUI = game.Content.Load<Texture2D>("TalismanUI");
            Grass_1 = game.Content.Load<Texture2D>("GrassLevel2");
            Grass_2 = game.Content.Load<Texture2D>("GrassLevel2_2");
            Ghost = game.Content.Load<Texture2D>("GhostLevel2");
            HPCharBar[0] = game.Content.Load<Texture2D>("HP01");
            HPCharBar[1] = game.Content.Load<Texture2D>("HP02");
            Talisman_Sprite = game.Content.Load<Texture2D>("Talisman");
            font = game.Content.Load<SpriteFont>("November");
            Sign = game.Content.Load<Texture2D>("Sign");
            FrameBG = game.Content.Load<Texture2D>("Frame");
            ButtonGame[0] = game.Content.Load<Texture2D>("Next_button");
            ButtonGame[1] = game.Content.Load<Texture2D>("Warp_Message");
            ButtonGame[2] = game.Content.Load<Texture2D>("Hide_message");
            ButtonGame[3] = game.Content.Load<Texture2D>("UseTalisman");
            ButtonGame[4] = game.Content.Load<Texture2D>("Message_Ghost_Level2_01");
            ButtonGame[5] = game.Content.Load<Texture2D>("Message_Ghost_Level2_02");
            text_begin_sence = game.Content.Load<Texture2D>("beta1l2");
            line_begin_sence = game.Content.Load<Texture2D>("beta05");
            cr1_begin_sence = game.Content.Load<Texture2D>("S1L2");
            Floor_top = game.Content.Load<Texture2D>("Floor");
            Banana_Tree = game.Content.Load<Texture2D>("Banana-Tree");
            Hide = game.Content.Load<Texture2D>("Hide_Level2");
            Holly = game.Content.Load<Texture2D>("Holy Water");
            arrow = game.Content.Load<Texture2D>("arrow");
            Warp = game.Content.Load<Texture2D>("Thai_house");
            Level2_2BG = game.Content.Load<Texture2D>("BG_Level2");
            Bar_Item_LevelOne = game.Content.Load<Texture2D>("Bar_Item");
            MouseArrow = game.Content.Load<Texture2D>("MouseArrow");
            Character_Main.Load(game.Content, "Shaman", FramesChar, FramesRowChar, FramesPerSecChar);
            Character_Main.Pause();
            this.game = game;
        }

        public override void UpdateGame(GameTime theTime)
        {
            FilpRotation = -(Rotation);
            MousePos = game.MousePosition;
            elapsed = (float)theTime.ElapsedGameTime.TotalSeconds;
            if (game.IsGamePause == false)
            {
                charRectangle = new Rectangle((int)CharMainPos + Convert.ToInt32(MoveChar.X), (int)Convert.ToInt32(MoveChar.Y), 128, 200);
                LogBlockRectangle = new Rectangle((int)MoveTree[17] + 15, (int)((720 - 64) - (64 * 2.9f)), 70, 100);
                StumpBlockRectangle = new Rectangle((int)MoveTree[18] + 50, (int)((720 - 64) - (64 * 2.8f)), 25, 100);
                GhostRectangle[1] = new Rectangle(game.GhostPos_Level2 + 100, 220, 200, 200);
                if (Show_Ghost[1] == true || Show_Ghost[3] == true)
                {
                    if (game.ISGhostBackward == true)
                    {
                        BananaBulletRectangle = new Rectangle(Convert.ToInt32((BananaBulletPos.X + (game.GhostPos_Level2))), Convert.ToInt32(BananaBulletPos.Y + 280), 32, 16);
                    }
                    else
                    {
                        BananaBulletRectangle = new Rectangle(Convert.ToInt32((BananaBulletPos.X + (game.GhostPos_Level2 + 180))), Convert.ToInt32(BananaBulletPos.Y + 280), 32, 16);

                    }
                }
                else
                {
                    BananaBulletRectangle = new Rectangle(-100, -100, 32, 16);
                }
                LogBlock(charRectangle, LogBlockRectangle, StumpBlockRectangle);
                Ghost_Anima((float)theTime.ElapsedGameTime.TotalSeconds);
                Character_Main.UpdateFrame(elapsed);
                game.GhostPos_Level2 = -192 + (game.SpeedGhostLevel2 / 2);
                if (StopJump == false)
                {
                    MoveChar.Y += Velocity.Y;
                }
                Key_Control();
                setFloorPos();
                WarpAnima();
                if (IsRunProj == true)
                {
                    ItemHolyRec = new Rectangle(Convert.ToInt32((Proj_Holly.X - 16) + MoveHolly.X), Convert.ToInt32((Proj_Holly.Y + 100) + MoveHolly.Y), 32, 32);
                }
            } else
            {
                Talisman_PuzzleRectangle[0] = new Rectangle(205, 125, 150, 150);
                Talisman_PuzzleRectangle[1] = new Rectangle(365, 125, 150, 150);
                Talisman_PuzzleRectangle[2] = new Rectangle(525, 285, 150, 150);
                Talisman_PuzzleRectangle[3] = new Rectangle(205, 445, 150, 150);
                Talisman_PuzzleRectangle[4] = new Rectangle(365, 445, 150, 150);
                Talisman_PuzzleRectangle[5] = new Rectangle(525, 125, 150, 150);
                Talisman_PuzzleRectangle[6] = new Rectangle(205, 285, 150, 150);
                Talisman_PuzzleRectangle[7] = new Rectangle(365, 285, 150, 150);
                Talisman_PuzzleRectangle[8] = new Rectangle(525, 445, 150, 150);
            }

            MusicLevel();
            TimeS1((int)theTime.ElapsedGameTime.TotalSeconds);
            Talisman_Text = Convert.ToString(Talisman_Scrap_Num);
            Holy_Text = Convert.ToString(game.Holy_Num);
            Text_String = Convert.ToString(game.Text_Num);
            Textg1 = Convert.ToString(N_Text + " <> " + game.Text_Num + " <> " + game.PlayEffectLevel2);

            if (game.FrameTransitionIn > 6 && game.IsPauseMenu == true)
            {
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                EventScreen.Invoke(game.mPauseDiedMenu, new EventArgs());

            }
            if (game.FrameTransitionIn > 6 && game.IsDiedMenu == true)
            {
                if (game.Holy_Num > 0)
                {
                    game.Holy_Num -= 1;
                }
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                EventScreen.Invoke(game.mPauseDiedMenu, new EventArgs());

            }
            if (game.FrameTransitionIn > 6 && To_Level3 == true)
            {
                game.PlayTransitionIn = false;
                EventScreen.Invoke(game.mLevel3, new EventArgs());

            }

            //ตั้งจุดเกิดผี
            if (floorSpeedPos.X > 1100 && Show_Ghost[2] == false && game.IsGhost_DiedLevel2 == false)
            {
                if (game.IsPlayMusic == true && game.IsplayMusicGAttack == true)
                {
                    game.PlayJumpScareEffact = true;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(game.MusicGhostAttack);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 1f;
                    game.IsplayMusicGAttack = false;
                }
                Show_Ghost[1] = true;
            }
            if (floorSpeedPos.X > 7000 && Show_Ghost[2] == true && game.IsGhost_DiedLevel2 == false)
            {
                if (game.IsPlayMusic == true && game.IsplayMusicGAttack == true)
                {
                    game.PlayJumpScareEffact = true;
                    game.IsPlaySFXGhostSee = true;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(game.MusicGhostAttack);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 1f;
                    game.IsplayMusicGAttack = false;
                }
                Show_Ghost[3] = true;
            }
            base.UpdateGame(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            if (game.FrameTransitionIn > 6 && Show_Level2 == true)
            {
                Reset_To_Level(576, 0, 0, -576);
                game.PlayTransitionIn = false;
                game.PlayTransitionOut = true;
                Show_Level2 = false;
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
                CutSenceTwo(theBatch);
            }
            else
            {
                Level_2(theBatch);
            }

            //Frame
            theBatch.Draw(FrameBG, new Vector2(0, 0), null, Color.White);

            //Test
            //theBatch.DrawString(font, Textg1, new Vector2(20, 600), Color.White);
            base.Draw(theBatch);
        }
        void CutSenceTwo(SpriteBatch spriteBatch)
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
                
                game.Level = 2;
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
                        Show_Level2 = true;
                    }
                    spriteBatch.Draw(ButtonGame[0], new Vector2(1027, 640), new Rectangle(189, 0, 189, 46), Color.White);
                }
                else
                {
                    spriteBatch.Draw(ButtonGame[0], new Vector2(1027, 640), new Rectangle(0, 0, 189, 46), Color.White);
                }
            }


        }
        void Level_2(SpriteBatch spriteBatch)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.IsPauseMenu = true;
                game.IsDiedMenu = false;
                game.PlayTransitionIn = true;
            }
            //Draw
            spriteBatch.Draw(Level2_2BG, new Vector2((-1 * (floorSpeedPos.X * 0.3f)), 0), null, Color.White);
            spriteBatch.Draw(Level2_2BG, new Vector2((-1 * (floorSpeedPos.X * 0.3f)) + 1786, 0), null, Color.White);
            spriteBatch.Draw(Level2_2BG, new Vector2((-1 * (floorSpeedPos.X * 0.3f)) + (1786 * 2), 0), null, Color.White);
            Tree();
            GrassBG();

            hideout(2900, 0);
            hideout(7500, 1);

            //ป้ายบอกจุดวาร์ป
            spriteBatch.Draw(Sign, new Vector2(10180 - floorSpeedPos.X, (720 - 64) - (64 * 3.5f)), new Rectangle(0, 0, 117, 89), Color.White);

            //เสาใช้งานยันต์
            Pole();

            //Warp
            WarpRectangle = new Rectangle(Convert.ToInt32(10330 - floorSpeedPos.X), 10, 432, 492);
            spriteBatch.Draw(Warp, new Vector2(10330 - floorSpeedPos.X, 10), Color.White);
            if (WarpRectangle.Intersects(charRectangle) == true)
            {
                Show_WarpMessage = true;
            }
            else
            {
                Show_WarpMessage = false;
            }
            if (Show_WarpMessage == true && game.IsGhost_DiedLevel2 == true)
            {
                spriteBatch.Draw(ButtonGame[1], new Vector2(545.5f + MoveChar.X, MoveChar.Y - 32), Color.White);
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    game.PlayJumpScareEffact = false;
                    if (game.IsPlaySFX == true && To_Level3 == false)
                    {
                        game.SoundSFX[14].Play();
                        SoundEffect.MasterVolume = 0.4f;
                    }
                    game.IsplayMusicGAttack = true;
                    game.IsPlaySFXCutSence = true;
                    game.PlayTransitionIn = true;
                    To_Level3 = true;
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
            if (Show_Ghost[1] == true && game.IsGhost_DiedLevel2 == false)
            {
                Ghots(false);
                if (game.GhostPos_Level2 >= 1280 && IsGhostStun[1] == false)
                {
                    game.ISGhostBackward = true;
                }
                else
                {
                    if (game.GhostPos_Level2 > 1480 && IsGhostStun[1] == true)
                    {
                        game.ISGhostBackward = true;
                    }
                }
                if (game.GhostPos_Level2 <= -200)
                {
                    game.ISGhostBackward = false;
                }
            }
            if (Show_Ghost[3] == true && Show_Ghost[2] == true && game.IsGhost_DiedLevel2 == false)
            {
                Ghots(false);
                if (game.GhostPos_Level2 >= 1280 && IsGhostStun[1] == false)
                {
                    game.ISGhostBackward = true;
                }
                else
                {
                    if (game.GhostPos_Level2 > 1480 && IsGhostStun[1] == true)
                    {
                        game.ISGhostBackward = true;
                    }
                }
                if (game.GhostPos_Level2 <= -200)
                {
                    game.ISGhostBackward = false;
                }
            }

            //ไอเทมตามแมพ
            ItemDrop(1500, 3);
            ItemDrop(6500, 5);
            ItemTextDrop(200, 0);
            ItemTextDrop(7200, 1);
            Talisman(800, 0, 1);
            Talisman(5350, 1, 3);
            Talisman(7900, 2, 5);

            SystemFloorMove();
            Obstacle();
            GrassFG();
            floor();

            //Draw น้ำมนต์
            if (MoveChar.X != 0)
            {
                Sec[3] = 0;
                IsKeySelectOne = false;

            }
            if (game.Holy_Num == 0)
            {
                IsRunProj = false;
                IsKeySelectOne = false;
                Sec[3] = 0;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && IsRunProj == false && IsKeySelectOne == true)
            {
                IsRunProj = true;
                Proj_Holly.Y = MoveChar.Y;
                Proj_Holly.X = 640;
                Sec[3] = 0;
                if (game.IsPlaySFX == true)
                {
                    SoundEffect.MasterVolume = 1.0F;
                    game.SoundSFX[6].Play();
                }
                game.IsPlaySFXGhostSee = true;
                if (MousePos.X > 640 && MousePos.X <= 1280)
                {
                    angle_holly = (Rotation) * 5;
                    v_Speed = v_Gravity - (Rotation * 2.8f);
                }
                else { }
                if (MousePos.X >= 0 && MousePos.X <= 640)
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

            //ผี
            void Ghots(bool IsGhostLoop)
            {
                if (game.IsGamePause == false)
                {
                    if (GhostRectangle[1].Intersects(charRectangle) == true && game.GhostPos_Level2 > 0 && game.IsGhost_See == true)
                    {
                        game.CharHart = 0;
                    }
                    if (IsRunProj == true)
                    {
                        if (ItemHolyRec.Intersects(GhostRectangle[1]) == true && IsHolyInter_Ghost == false)
                        {
                            IsHolyInter_Ghost = true;
                            IsRunProj = false;
                            MoveHolly.X = 1280;
                            IsGhostStun[1] = true;
                            game.Holy_Num -= 1;
                        }
                    }
                    if (ItemHolyRec.Intersects(GhostRectangle[1]) == false && IsHolyInter_Ghost == true)
                    {
                        if (game.IsPlaySFX == true)
                        {
                            SoundEffect.MasterVolume = 1.0F;
                            game.SoundSFX[5].Play();
                            game.SoundSFX[11].Play();
                        }
                        IsHolyInter_Ghost = false;
                        IsRunProj = false;
                        MoveHolly.X = 0;
                        Sec[3] = 0;
                    }

                    //ผีเดินหน้า
                    if (game.ISGhostBackward == false)
                    {
                        if (game.IsGhost_See == true)
                        {
                            if (game.IsPlaySFX == true && game.IsPlaySFXGhostSee == true)
                            {
                                SoundEffect.MasterVolume = 1.0F;
                                game.SoundSFX[10].Play();
                                game.IsPlaySFXGhostSee = false;
                            }
                            if(game.IsShowChar == false)
                            {
                                spriteBatch.Draw(ButtonGame[4], new Vector2(game.GhostPos_Level2 + 40f, 155), null, Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(ButtonGame[5], new Vector2(game.GhostPos_Level2 + 90, 155), null, Color.White);
                            }
                        }

                        if (game.IsShowChar == true && game.GhostPos_Level2 < 460 && IsGhostStun[1] == false)
                        {
                            Ghost_Shot = true;
                        }

                        if (IsGhostStun[1] == true)
                        {
                            Ghost_Shot = false;
                            game.IsGhost_See = false;
                        }
                        if (Sec[2] == 0)
                        {
                            if (game.IsShowChar == false)
                            {
                                Ghost_Shot = false;
                            }
                            if (game.GhostPos_Level2 >= 460)
                            {
                                Ghost_Shot = false;
                            }
                        }

                        if (game.GhostPos_Level2 > 0)
                        {
                            if (Ghost_Shot == true)
                            {
                                bananabullet_level2();
                            }

                        }

                        GhostWalkBackward = 0;
                        if (IsGhostStun[1] == true && 20 + (game.SpeedGhostLevel2 / 2) > -400)
                        {
                            if (game.GhostPos_Level2 > 640)
                            {
                                game.SpeedGhostLevel2 += 3;
                            }
                            else
                            {
                                game.SpeedGhostLevel2 -= 3;
                            }

                        }
                        if (20 + (game.SpeedGhostLevel2 / 2) < -400)
                        {
                            IsGhostStun[1] = false;
                            game.ISGhostBackward = false;
                        }
                        game.SpeedGhostLevel2 += 1;
                    }

                    //ผีเดินหลัง
                    else
                    {
                        if (game.IsGhost_See == true)
                        {
                            if (game.IsPlaySFX == true && game.IsPlaySFXGhostSee == true)
                            {
                                SoundEffect.MasterVolume = 1.0F;
                                game.SoundSFX[10].Play();
                                game.IsPlaySFXGhostSee = false;
                            }
                            if (game.IsShowChar == false)
                            {
                                spriteBatch.Draw(ButtonGame[4], new Vector2(game.GhostPos_Level2 + 40f, 155), null, Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(ButtonGame[5], new Vector2(game.GhostPos_Level2 + 100, 155), null, Color.White);
                            }
                        }

                        if (game.IsShowChar == true && game.GhostPos_Level2 > 460 && IsGhostStun[1] == false)
                        {
                            Ghost_Shot = true;
                        }

                        if (IsGhostStun[1] == true)
                        {
                            Ghost_Shot = false;
                            game.IsGhost_See = false;
                        }
                        if (Sec[2] == 0)
                        {
                            if (game.IsShowChar == false)
                            {
                                Ghost_Shot = false;
                            }
                            if (game.GhostPos_Level2 <= 460)
                            {
                                Ghost_Shot = false;
                            }
                        }

                        if (game.GhostPos_Level2 < 1080 && game.GhostPos_Level2 > 640)
                        {
                            if (Ghost_Shot == true)
                            {
                                bananabullet_level2();
                            }

                        }

                        GhostWalkBackward = 1;
                        if (IsGhostStun[1] == true && game.GhostPos_Level2 < 1480)
                        {
                            if (game.GhostPos_Level2 < 640)
                            {
                                game.SpeedGhostLevel2 -= 3;
                            }
                            else
                            {
                                game.SpeedGhostLevel2 += 3;
                            }

                        }
                        if (game.GhostPos_Level2 > 1479)
                        {
                            game.ISGhostBackward = true;
                            IsGhostStun[1] = false;
                        }
                        if (game.GhostPos_Level2 <= -396 && IsGhostLoop == false)
                        {
                            game.PlayJumpScareEffact = false;
                            MediaPlayer.Stop();
                            game.IsplayMusicGAttack = true;
                            game.IsplayMusicLevel = true;
                            Show_Ghost[1] = false;
                            Show_Ghost[2] = true;
                        }
                        game.SpeedGhostLevel2 -= 1;
                    }
                }
                //ผื
                spriteBatch.Draw(Ghost, new Vector2(game.GhostPos_Level2, 120), new Rectangle(400 * frame_ghost, 400 * GhostWalkBackward, 400, 400), Color.White);


            }
            void bananabullet_level2()
            {
                if (game.ISGhostBackward == false)
                {
                    if (game.GhostPos_Level2 > 0 && Sec[2] <= 100)
                    {
                        Sec[2] += 1;
                    }
                    if (Sec[2] > 90)
                    {
                        if (BananaBulletPos.X <= 1280 && BananaBulletPos.Y <= 720)
                        {
                            BananaBulletPos.X += v_X_Ghost;
                            BananaBulletPos.Y += v_Y_Ghost;
                            if (v_X_Ghost > 0)
                            {
                                v_X_Ghost -= 0.05f;
                            }
                            v_Y_Ghost += 0.1f;


                            spriteBatch.Draw(NoTexture2D, new Vector2(BananaBulletPos.X + (game.GhostPos_Level2 + 180), BananaBulletPos.Y + 280), new Rectangle(0, 0, 32, 16), Color.White);
                        }
                        else
                        {
                            BananaBulletPos.X = 0;
                            BananaBulletPos.Y = 0;
                            v_X_Ghost = 10;
                            v_Y_Ghost = 2;
                            Sec[2] = 0;
                        }
                    }
                }
                else
                {
                    if (game.GhostPos_Level2 < 1080 && Sec[2] <= 100)
                    {
                        Sec[2] += 1;
                    }
                    if (Sec[2] > 90)
                    {
                        if (BananaBulletPos.X >= -500 && BananaBulletPos.Y <= 720)
                        {
                            BananaBulletPos.X -= v_X_Ghost;
                            BananaBulletPos.Y += v_Y_Ghost;
                            if (v_X_Ghost < 0)
                            {
                                v_X_Ghost -= 0.05f;
                            }
                            v_Y_Ghost += 0.1f;
                            spriteBatch.Draw(NoTexture2D, new Vector2(BananaBulletPos.X + (game.GhostPos_Level2), BananaBulletPos.Y + 280), new Rectangle(0, 0, 32, 16), Color.White);
                        }
                        else
                        {
                            v_X_Ghost = 10;
                            v_Y_Ghost = 2;
                            BananaBulletPos.X = 0;
                            BananaBulletPos.Y = 0;
                            Sec[2] = 0;
                        }
                    }
                }
            }
            void hideout(int Location, int i)
            {
                HideOutLevel_1_Rectangle[i] = new Rectangle(Convert.ToInt32(Location - floorSpeedPos.X), Convert.ToInt32(((720 - 64) - (64 * 4.9f))), 168, 192);
                spriteBatch.Draw(Hide, new Vector2(Location - floorSpeedPos.X, ((720 - 64) - (64 * 4.9f))), new Rectangle(168 * Is_HideOut, 0, 168, 192), Color.White);
                if (HideOutLevel_1_Rectangle[i].Intersects(charRectangle) == true)
                {
                    Show_HideOutMessage = true;
                }
                else
                {
                    Show_HideOutMessage = false;
                }
                if (game.GhostPos_Level2 > 0 && game.GhostPos_Level2 < 1080 && game.IsShowChar == true && IsGhostStun[1] == false)
                {
                    game.IsGhost_See = true;
                }
                if (Show_HideOutMessage == true)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        if (game.GhostPos_Level2 > 0 && game.GhostPos_Level2 < 1080 && game.IsShowChar == true && IsGhostStun[1] == false)
                        {
                            game.IsGhost_See = true;
                        }
                        game.IsShowChar = false;
                        Is_HideOut = 1;
                    }

                    if (game.IsShowChar == false)
                    {
                        spriteBatch.Draw(ButtonGame[2], new Vector2(529.5f + MoveChar.X, MoveChar.Y - 32), new Rectangle(0, 42, 221, 42), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(ButtonGame[2], new Vector2(545.5f + MoveChar.X, MoveChar.Y - 32), new Rectangle(0, 0, 189, 42), Color.White);
                    }
                }

            }


            //Draw ลูกศร
            if (IsKeySelectOne == true && game.Holy_Num > 0)
            {
                Arrow_HolyWater(spriteBatch);
            }
            else { }

            //Item
            ItemInGame(spriteBatch);

            //หัวใจ
            HartChar(spriteBatch);

            //PuzzleLevel2
            Talisman_Puzzle();
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
            void Tree()
            {
                //Banana-Tree-Stump
                spriteBatch.Draw(Banana_Tree, new Vector2(MoveTree[18], ((720 - 64) - (64 * 3f))), new Rectangle(192, 100, 100, 107), Color.White);
            }
            void GrassBG()
            {
                spriteBatch.Draw(Grass_2, new Vector2(MoveTree[1], ((720 - 64) - (64 * 3.3f))), Color.White);
                spriteBatch.Draw(Grass_2, new Vector2(MoveTree[5], ((720 - 64) - (64 * 3.3f))), Color.White);
                spriteBatch.Draw(Grass_2, new Vector2(MoveTree[3], ((720 - 64) - (64 * 3.3f))), Color.White);
            }
            void GrassFG()
            {
                spriteBatch.Draw(Grass_2, new Vector2(MoveTree[9], ((720 - 64) - (64 * 3.3f))), Color.White);
                spriteBatch.Draw(Grass_2, new Vector2(MoveTree[14], ((720 - 64) - (64 * 3.3f))), Color.White);
            }
            void Obstacle()
            {
                SystemTreeMove();
                //Banana-Tree-Log
                spriteBatch.Draw(Banana_Tree, new Vector2(MoveTree[17], ((720 - 64) - (64 * 3.5f))), new Rectangle(256, 0, 100, 100), Color.White);
            }
            void ItemDrop(int Position, int i)
            {
                if (charRectangle.Intersects(HolyItemRectangle[i]) == true && Holy_Visi[i] == false)
                {
                    game.Holy_Num += 1;
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
            void Talisman(int Position, int i, int num)
            {
                if (charRectangle.Intersects(TalismanRectangle[i]) == true && Talisman_Visi[i] == false)
                {
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[7].Play();
                    }
                    Talisman_Scrap_Num += num;
                    Talisman_Visi[i] = true;
                }
                else
                {
                    if (Talisman_Visi[i] == false)
                    {
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(Position - floorSpeedPos.X, (720 - 64) - (64 * 3.4f)), null, Color.White);
                        TalismanRectangle[i] = new Rectangle(Convert.ToInt32(Position - floorSpeedPos.X), Convert.ToInt32((720 - 64) - (64 * 3.4f)), 32, 32);
                    }

                }

            }
            void Talisman_Puzzle()
            {
                if (Talisman_Scrap_Num == 9)
                {
                    IsTalismanPuzzle_Show = true;
                }

                if (IsTalismanPuzzle_Show == true)
                {
                    IsKeySelectOne = false;
                    game.IsGamePause = true;
                    spriteBatch.Draw(TalismanUI, new Vector2(center.X - 450, center.Y - 250), new Rectangle(0, 0, 900, 500), Color.White);
                    spriteBatch.Draw(TalismanUI, new Vector2(1058, 110), new Rectangle(868, 0, 32, 32), Color.White);
                    //ปุ่มปิด
                    if (MousePos.X >= 1058 && MousePos.X <= 1090 && MousePos.Y >= 110 && MousePos.Y <= 142)
                    {
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton2 == true)
                        {
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton2 = false;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Talisman_Scrap_Num == 1)
                        {
                            //เสียงปุ่ม
                            if (game.IsPlaySFX == true && IsMouseClick2 == true)
                            {
                                SoundEffect.MasterVolume = 1.0f;
                                game.ButtonSFX[1].Play();
                                IsMouseClick2 = false;
                            }
                            game.IsGamePause = false;
                            IsTalismanPuzzle_Show = false;
                        }
                        else
                        {
                            IsMouseClick2 = true;
                        }
                        spriteBatch.Draw(TalismanUI, new Vector2(1058, 110), new Rectangle(900, 0, 32, 32), Color.White);
                    }
                    else
                    {
                        IsMouseOnButton2 = true;
                    }

                    //เมื่อวางยันต์ถูกให้แสดง
                    if(Talisman_Scrap_Num == 1)
                    {
                        spriteBatch.Draw(TalismanPuzzle, new Vector2(215, 135), new Rectangle(0, 0, 450, 450), Color.White);
                    } else
                    {
                        if (IS_Talisman_Puzzle_Correct[0] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(205, 125), new Rectangle(0, 0, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[6] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(205, 285), new Rectangle(0, 150, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[3] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(205, 445), new Rectangle(0, 300, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[1] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(365, 125), new Rectangle(150, 0, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[7] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(365, 285), new Rectangle(150, 150, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[4] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(365, 445), new Rectangle(150, 300, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[5] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(525, 125), new Rectangle(300, 0, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[2] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(525, 285), new Rectangle(300, 150, 150, 150), Color.White);
                        }
                        if (IS_Talisman_Puzzle_Correct[8] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, new Vector2(525, 445), new Rectangle(300, 300, 150, 150), Color.White);
                        }

                        spriteBatch.Draw(Talisman_Sprite, new Vector2(710, 200), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(848, 200), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(986, 200), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(710, 250), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(848, 250), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(986, 250), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(710, 300), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(848, 300), null, Color.White);
                        spriteBatch.Draw(Talisman_Sprite, new Vector2(986, 300), null, Color.White);

                        SetPosPuzzle(new Vector2(710, 200), new Vector2(742, 232), new Vector2(205, 125), new Vector2(690, 180), 0);
                        SetPosPuzzle(new Vector2(710, 250), new Vector2(742, 282), new Vector2(205, 285), new Vector2(690, 230), 1);
                        SetPosPuzzle(new Vector2(710, 300), new Vector2(742, 332), new Vector2(205, 445), new Vector2(690, 280), 2);
                        SetPosPuzzle(new Vector2(848, 200), new Vector2(880, 232), new Vector2(365, 125), new Vector2(789, 180), 3);
                        SetPosPuzzle(new Vector2(848, 250), new Vector2(880, 282), new Vector2(365, 285), new Vector2(789, 230), 4);
                        SetPosPuzzle(new Vector2(848, 300), new Vector2(880, 332), new Vector2(365, 445), new Vector2(789, 280), 5);
                        SetPosPuzzle(new Vector2(986, 200), new Vector2(1018, 232), new Vector2(525, 125), new Vector2(888, 180), 6);
                        SetPosPuzzle(new Vector2(986, 250), new Vector2(1018, 282), new Vector2(525, 285), new Vector2(888, 230), 7);
                        SetPosPuzzle(new Vector2(986, 300), new Vector2(1018, 332), new Vector2(525, 445), new Vector2(888, 280), 8);

                        void SetPosPuzzle(Vector2 Mouse_Pos_Over, Vector2 Mouse_Pos_Less, Vector2 TalismanPuzzlePos, Vector2 TalismanPuzzleOriginalPos, int i)
                        {
                            if (MousePos.X >= Mouse_Pos_Over.X && MousePos.X <= Mouse_Pos_Less.X && MousePos.Y >= Mouse_Pos_Over.Y && MousePos.Y <= Mouse_Pos_Less.Y && IS_Talisman_Puzzle_Correct[i] == false)
                            {
                                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                                {
                                    if (game.IsPlaySFX == true && IsPlaySPFTalismanPuzzle[i] == true)
                                    {
                                        SoundEffect.MasterVolume = 1.0F;
                                        game.SoundSFX[8].Play();
                                        IsPlaySPFTalismanPuzzle[i] = false;
                                    }
                                    Talisman_Puzzle_Pos[i] = new Vector2(MousePos.X - 75, MousePos.Y - 75);
                                    Talisman_ScrapRectangle[i] = new Rectangle(Convert.ToInt32(MousePos.X - 75), Convert.ToInt32(MousePos.Y - 75), 150, 150);
                                    Is_Talisman_Puzzle[i] = true;
                                }
                                Talisman_Puzzle_Visi[i] = true;
                            }
                            else
                            {
                                if (Is_Talisman_Puzzle[i] == false)
                                {
                                    Talisman_Puzzle_Visi[i] = false;
                                }
                                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Is_Talisman_Puzzle[i] == true && IS_Talisman_Puzzle_Correct[i] == false)
                                {
                                    Talisman_Puzzle_Pos[i] = new Vector2(MousePos.X - 75, MousePos.Y - 75);
                                    Talisman_ScrapRectangle[i] = new Rectangle(Convert.ToInt32(MousePos.X - 75), Convert.ToInt32(MousePos.Y - 75), 150, 150);
                                }
                                else
                                {
                                    if (Talisman_PuzzleRectangle[i].Intersects(Talisman_ScrapRectangle[i]) == true)
                                    {
                                        if (game.IsPlaySFX == true && IsPlaySPFTalismanPuzzle2[i] == true)
                                        {
                                            SoundEffect.MasterVolume = 1.0F;
                                            game.SoundSFX[9].Play();
                                            IsPlaySPFTalismanPuzzle2[i] = false;
                                        }
                                        IS_Talisman_Puzzle_Correct[i] = true;
                                        Talisman_Puzzle_Pos[i] = TalismanPuzzlePos;
                                        Talisman_ScrapRectangle[i] = new Rectangle(Convert.ToInt32(TalismanPuzzlePos.X), Convert.ToInt32(TalismanPuzzlePos.Y), 150, 150);
                                        Talisman_Puzzle_Visi[i] = true;
                                    }
                                    else
                                    {
                                        IsPlaySPFTalismanPuzzle2[i] = true;
                                        IsPlaySPFTalismanPuzzle[i] = true;
                                        Talisman_Puzzle_Pos[i] = TalismanPuzzleOriginalPos;
                                        Is_Talisman_Puzzle[i] = false;
                                    }

                                }

                            }
                        }
                        //เซ็ตใช้แสดง
                        if (Talisman_Puzzle_Visi[0] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[0], new Rectangle(0, 0, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[1] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[1], new Rectangle(150, 0, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[2] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[2], new Rectangle(300, 150, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[3] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[3], new Rectangle(0, 300, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[4] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[4], new Rectangle(150, 300, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[5] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[5], new Rectangle(300, 0, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[6] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[6], new Rectangle(0, 150, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[7] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[7], new Rectangle(150, 150, 150, 150), Color.White);
                        }
                        if (Talisman_Puzzle_Visi[8] == true)
                        {
                            spriteBatch.Draw(TalismanPuzzle, Talisman_Puzzle_Pos[8], new Rectangle(300, 300, 150, 150), Color.White);
                        }
                    }

                    if (IS_Talisman_Puzzle_Correct[0] == true && IS_Talisman_Puzzle_Correct[1] == true && IS_Talisman_Puzzle_Correct[2] == true && IS_Talisman_Puzzle_Correct[3] == true && IS_Talisman_Puzzle_Correct[4] == true && IS_Talisman_Puzzle_Correct[5] == true && IS_Talisman_Puzzle_Correct[6] == true && IS_Talisman_Puzzle_Correct[7] == true && IS_Talisman_Puzzle_Correct[8] == true)
                    {
                        Talisman_Scrap_Num = 1;
                    }
                    else
                    {
                        Talisman_Scrap_Num = 0;
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
                    spriteBatch.DrawString(fontLarge, Convert.ToString( N_Text), new Vector2(406, 412), Color.White);
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
                        spriteBatch.Draw(ButtonTextUI, new Vector2(277f, 466), new Rectangle(0, 28, 96, 28), Color.White);
                        //เสียงปุ่ม
                        if (game.IsPlaySFX == true && IsMouseOnButton4 == true)
                        {
                            SoundEffect.MasterVolume = 1.0f;
                            game.ButtonSFX[0].Play();
                            IsMouseOnButton4 = false;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Is_MouseClick == true)
                        {
                            Is_MouseClick = false;
                            N_Text -= 1;
                            
                            if(N_Text < 1)
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
            void Pole()
            {
                PoleRectangle = new Rectangle(Convert.ToInt32(9500 - floorSpeedPos.X), Convert.ToInt32((720 - 64) - (64 * 8.5f)), 108, 400);
                spriteBatch.Draw(Tree_Talisman_Sprite, new Vector2(9500 - floorSpeedPos.X, (720 - 64) - (64 * 8.5f)), new Rectangle(108 * Direction_tree, 0, 108, 400), Color.White);

                if (PoleRectangle.Intersects(charRectangle) == true)
                {
                    spriteBatch.Draw(ButtonGame[3], new Vector2(517 + MoveChar.X, MoveChar.Y - 32), null, Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.E) && Direction_tree == 0)
                    {
                        Direction_tree = 1;
                        game.PlayEffectLevel2 = true;
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
                if (BananaBulletRectangle.Intersects(charRectangle) == true && game.IsShowChar == true)
                {
                    BananaBulletPos.X = 0;
                    BananaBulletPos.Y = 0;
                    v_X_Ghost = 10;
                    v_Y_Ghost = 2;
                    Sec[2] = 0;
                    game.CharHart -= 25;
                }
                if (game.CharHart <= 0)
                {
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[12].Play();
                    }
                    BananaBulletPos.X = 0;
                    BananaBulletPos.Y = 0;
                    v_X_Ghost = 10;
                    v_Y_Ghost = 2;
                    Sec[2] = 0;
                    MediaPlayer.Stop();
                    PauseAndDiedMenu.IsPlayMusicPauseMenu = true;
                    game.IsGamePause = true;
                    game.IsGhost_See = false;
                    game.IsDiedMenu = true;
                    game.PlayTransitionIn = true;
                }
                spriteBatch.Draw(HPCharBar[1], new Vector2(1280 - 250, 50), null, Color.White);
                spriteBatch.Draw(HPCharBar[0], new Vector2(1280 - 250, 50), new Rectangle(0, 0, game.CharHart * 2, 32), Color.White);
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

            //กดซ้ำ A D
            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.A))
            {
                IsWalk = false;
            }
            else { IsWalk = true; }

            //เดินไปข้างหน้า
            if (keyboardState.IsKeyDown(Keys.D) && IsWalk == true && Canwalk_D == true)
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

                if (floorSpeedPos.X < 9500 && MoveChar.X == 0)
                {
                    MovementFloor = -4;
                    MovementTree = -4;
                    game.IsShowChar = true;
                    Is_HideOut = 0;
                    if (IsRunProj == true)
                    {
                        MoveHolly.X -= 4;
                    }

                    if (Proj_Holly.Y >= 380)
                    {
                        MoveHolly.X = 0;
                    }
                    FloorMoveMent();
                    TreeMoveMent();
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
                    MovementTree = 0;
                    Character_Main.Pause(0, 0);
                    
                    Sec[4] = 0.0f;
                }
                if (IsWalk == false)
                {
                    MovementTree = 0;
                    Character_Main.Pause(0, 0);
                }
            }

            //เดินกลับหลัง
            if (keyboardState.IsKeyDown(Keys.A) && IsWalk == true && CanWalk_A == true)
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
                    Is_HideOut = 0;
                    MovementFloor = +4;
                    MovementTree = +4;
                    FloorMoveMent();
                    if (Proj_Holly.Y >= 380)
                    {
                        MoveHolly.X = 0;
                    }
                    TreeMoveMent();
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
                    
                    Sec[4] = 0.0f;
                    MovementTree = 0;
                    Character_Main.Pause(0, 0);
                }
                if (IsWalk == false)
                {
                    MovementTree = 0;
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
                if (game.IsPlaySFX == true && StopJump == true && IsLogSFXJump == true)
                {
                    SoundEffect.MasterVolume = 1.0f;
                    game.SoundSFX[2].Play();
                    IsLogSFXJump = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Jump == true)
            {
                IsLogSFXJump = true;
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
                Is_HideOut = 0;
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

        //ระบบต้นไม้
        void TreeMoveMent()
        {
            MoveTree[0] = MoveTree[0] + MovementTree;
            MoveTree[1] = MoveTree[1] + MovementTree;
            MoveTree[2] = MoveTree[2] + MovementTree;
            MoveTree[3] = MoveTree[3] + MovementTree;
            MoveTree[4] = MoveTree[4] + MovementTree;
            MoveTree[5] = MoveTree[5] + MovementTree;
            MoveTree[6] = MoveTree[6] + MovementTree;
            MoveTree[7] = MoveTree[7] + MovementTree;
            MoveTree[8] = MoveTree[8] + MovementTree;
            MoveTree[9] = MoveTree[9] + MovementTree;
            MoveTree[10] = MoveTree[10] + MovementTree;
            MoveTree[11] = MoveTree[11] + MovementTree;
            MoveTree[12] = MoveTree[12] + MovementTree;
            MoveTree[13] = MoveTree[13] + MovementTree;
            MoveTree[14] = MoveTree[14] + MovementTree;
            MoveTree[15] = MoveTree[15] + MovementTree;
            MoveTree[16] = MoveTree[16] + MovementTree;
            MoveTree[17] = MoveTree[17] + MovementTree;
            MoveTree[18] = MoveTree[18] + MovementTree;
            MoveTree[19] = MoveTree[19] + MovementTree;
            MoveTree[20] = MoveTree[20] + MovementTree;
        }
        void SystemTreeMove()
        {
            //เลื่อนไปข้างหน้า
            if (MoveTree[0] < -252)
            {
                MoveTree[0] = 1280;
            }
            if (MoveTree[1] < -252)
            {
                MoveTree[1] = 1280;
            }
            if (MoveTree[2] < -252)
            {
                MoveTree[2] = 1280;
            }
            if (MoveTree[3] < -252)
            {
                MoveTree[3] = 1280;
            }
            if (MoveTree[4] < -252)
            {
                MoveTree[4] = 1280;
            }
            if (MoveTree[5] < -252)
            {
                MoveTree[5] = 1280;
            }
            if (MoveTree[6] < -252)
            {
                MoveTree[6] = 1280;
            }
            if (MoveTree[7] < -252)
            {
                MoveTree[7] = 1280;
            }
            if (MoveTree[8] < -192)
            {
                MoveTree[8] = 1280;
            }
            if (MoveTree[9] < -192)
            {
                MoveTree[9] = 1280;
            }
            if (MoveTree[10] < -192)
            {
                MoveTree[10] = 1280;
            }
            if (MoveTree[11] < -64)
            {
                MoveTree[11] = 1280;
            }
            if (MoveTree[12] < -64)
            {
                MoveTree[12] = 1280;
            }
            if (MoveTree[13] < -64)
            {
                MoveTree[13] = 1280;
            }
            if (MoveTree[14] < -64)
            {
                MoveTree[14] = 1280;
            }
            if (MoveTree[15] < -64)
            {
                MoveTree[15] = 1280;
            }
            if (MoveTree[16] < -64)
            {
                MoveTree[16] = 1280;
            }
            if (MoveTree[17] < -100)
            {
                MoveTree[17] = 1280;
            }
            if (MoveTree[18] < -100)
            {
                MoveTree[18] = 1280;
            }
            if (MoveTree[19] < -192)
            {
                MoveTree[19] = 1280;
            }
            if (MoveTree[20] < -252)
            {
                MoveTree[20] = 1280;
            }

            //เลื่อนไปข้างหลัง
            if (MoveTree[0] > 1532)
            {
                MoveTree[0] = -252;
            }
            if (MoveTree[1] > 1532)
            {
                MoveTree[1] = -252;
            }
            if (MoveTree[2] > 1532)
            {
                MoveTree[2] = -252;
            }
            if (MoveTree[3] > 1532)
            {
                MoveTree[3] = -252;
            }
            if (MoveTree[4] > 1532)
            {
                MoveTree[4] = -252;
            }
            if (MoveTree[5] > 1532)
            {
                MoveTree[5] = -252;
            }
            if (MoveTree[6] > 1532)
            {
                MoveTree[6] = -252;
            }
            if (MoveTree[7] > 1532)
            {
                MoveTree[7] = -252;
            }
            if (MoveTree[8] > 1472)
            {
                MoveTree[8] = -192;
            }
            if (MoveTree[9] > 1472)
            {
                MoveTree[9] = -192;
            }
            if (MoveTree[10] > 1472)
            {
                MoveTree[10] = -192;
            }
            if (MoveTree[11] > 1344)
            {
                MoveTree[11] = -64;
            }
            if (MoveTree[12] > 1344)
            {
                MoveTree[12] = -64;
            }
            if (MoveTree[13] > 1344)
            {
                MoveTree[13] = -64;
            }
            if (MoveTree[14] > 1344)
            {
                MoveTree[14] = -64;
            }
            if (MoveTree[15] > 1344)
            {
                MoveTree[15] = -64;
            }
            if (MoveTree[16] > 1344)
            {
                MoveTree[16] = -64;
            }
            if (MoveTree[17] > 1380)
            {
                MoveTree[17] = -100;
            }
            if (MoveTree[18] > 1380)
            {
                MoveTree[18] = -100;
            }
            if (MoveTree[19] > 1472)
            {
                MoveTree[19] = -192;
            }
            if (MoveTree[20] > 1532)
            {
                MoveTree[20] = -252;
            }
        }
        void setTreePos()
        {
            //Banana-Tree-High
            MoveTree[0] = 256;
            MoveTree[1] = -30;
            MoveTree[2] = 100;
            MoveTree[3] = 453;
            MoveTree[4] = 665;
            MoveTree[5] = 886;
            MoveTree[6] = 1155;
            MoveTree[7] = 1200;
            MoveTree[20] = 1320;
            //Banana-Tree-Middle 
            MoveTree[8] = 0;
            MoveTree[9] = 500;
            MoveTree[10] = 1000;
            MoveTree[19] = 1280;
            //Banana-Tree-Low 
            MoveTree[11] = 192;
            MoveTree[12] = 282;
            MoveTree[13] = 492;
            MoveTree[14] = 882;
            MoveTree[15] = 696;
            MoveTree[16] = 1212;
            //Banana-Tree-Log
            MoveTree[17] = 356;
            //Banana-Tree-Stump
            MoveTree[18] = 1000;
        }
        void LogBlock(Rectangle charrect, Rectangle logprect, Rectangle Stumpblock)
        {

            if (charrect.Intersects(logprect) == true)
            {

                StopJump = true;
                Jump = true;
                IsHitLog = true;
                IsHitStump = false;
                if (CharMainPos + MoveChar.X >= MoveTree[17] && MoveChar.Y > 300)
                {

                    Canwalk_D = true;
                    Character_Main.Pause(0, 0);
                    CanWalk_A = false;
                    SpeedJump = -50;
                }

                if (CharMainPos + MoveChar.X <= MoveTree[17] - 100 && MoveChar.Y > 300)
                {
                    CanWalk_A = true;
                    Character_Main.Pause(0, 0);
                    Canwalk_D = false;
                    SpeedJump = -50;
                }

            }
            else if (charrect.Intersects(logprect) == false && IsHitLog == true)
            {
                CanWalk_A = true;
                Canwalk_D = true;
                StopJump = false;
            }

            if (charrect.Intersects(Stumpblock) == true)
            {

                StopJump = true;
                IsHitStump = true;
                IsHitLog = false;
                Jump = true;
                if (MoveTree[18] - 80 <= CharMainPos + MoveChar.X && MoveChar.Y > 300)
                {

                    CanWalk_A = true;
                    Character_Main.Pause(0, 0);
                    Canwalk_D = false;
                    SpeedJump = -50;

                }
                else { }

                if (CharMainPos + MoveChar.X >= MoveTree[18] && MoveChar.Y > 300)
                {
                    Canwalk_D = true;
                    Character_Main.Pause(0, 0);
                    CanWalk_A = false;
                    SpeedJump = -50;
                }
                else { }

            }
            else if (charrect.Intersects(Stumpblock) == false && IsHitStump == true)
            {
                CanWalk_A = true;
                Canwalk_D = true;
                StopJump = false;
            }
        }

        //ระบบเมนู/ฉาก
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
        void Ghost_Anima(float elapsed)
        {
            Elapsed_Ghost += elapsed;
            if (Elapsed_Ghost > TimeperFrame)
            {
                frame_ghost += 1;
                if (frame_ghost == 5)
                {
                    frame_ghost = 0;
                }
                Elapsed_Ghost -= 0.22f;
            }
        }

        //รีเซ็ตตำแหน่งผู้เล่นเมื่อเริ่มด่านใหม่
        void Reset_To_Level(int CharPos, int FloorPos, int SpeedPos, int Movechar)
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
            Is_HideOut = 0;
            TotalframeWarp = 0;
            MovementFloor = 0;
            MovementTree = 0;
            SpeedJump = -100;
            NFloor = 0;
            PFloor = 0;
            Textg1 = "0";
            Holy_Text = "0";
            Text_String = "0";
            Talisman_Text = "0";
            Show_Level2 = false;
            IsPlayMusicCutSence = true;
            IsMouseOnButton = true;
            IsMouseClick = true;
            Ghost_Shot = false;
            Ghost_Died = false;
            Show_WarpMessage = false;
            Show_HideOutMessage = false;
            directionChar = false;
            StartTime = true;
            IsJump = false;
            StopJump = false;
            IsLogSFXJump = true;
            IsShowTextUI = false;
            Jump = true;
            IsMouseOnButton3 = true;
            IsMouseClick3 = true;
            IsRunProj = false;
            IsKeySelectOne = false;
            IsHitLog = true;
            IsHolyInter_Ghost = false;
            IsHitStump = true;
            CanWalk_A = false;
            Canwalk_D = false;
            To_Level3 = false;
            IsWalk = true;
            frame_ghost = 0;
            N_Text = 1;
            frameWarp = 0;
            Direction_tree = 0;
            GhostWalkBackward = 0;
            Is_HideOut = 0;
            TotalframeWarp = 0;
            MovementFloor = 0;
            MovementTree = 0;
            SpeedJump = -100;
            Talisman_Scrap_Num = 0;
            Talisman_Num = 0;
            NFloor = 0;
            PFloor = 0;
            IsLogSFXJump = true;

            IsPlayMusicCutSence = true;

        IsMouseOnButton = true;
        IsMouseClick = true;

        IsMouseOnButton2 = true;

         IsMouseClick2 = true;
         IsMouseOnButton3 = true;

        IsMouseClick3 = true;
            Ghost_Shot = false;
            Ghost_Died = false;
            IsShowTextUI = false;
            Show_Level2 = false;
            To_Level3 = false;
            IsTalismanPuzzle_Show = false;
            Show_WarpMessage = false;
            Show_HideOutMessage = false;
            directionChar = false;
            StartTime = true;
             Is_MouseClick = false;
            IsJump = false;
            StopJump = false;
            Jump = true;
            IsRunProj = false;
            IsKeySelectOne = false;
            IsHitLog = true;
            IsHolyInter_Ghost = false;
            IsHitStump = true;
             CanWalk_A = false;
            Canwalk_D = false;
             IsWalk = true;
            setTreePos();
            for (int i = 0; i < 4; i++)
            {
                Show_Ghost[i] = false;
            }
                
            for (int i = 0; i < 3; i++)
            {
                Holy_Visi[i] = false;
            }
            for (int i = 0; i < 5; i++)
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
            for (int i = 0; i < 2; i++)
            {
                TextItem_Visi[i] = false;
            }
            IsGhostStun[0] = false;
            MoveChar.Y = ((720 - 64) - (64 * 5.2f));
        }
    }
}
