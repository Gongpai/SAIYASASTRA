using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameSystem : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        string Textg1 = "0";

        public List<SoundEffect> ButtonSFX;
        public List<SoundEffect> SoundSFX;
        public List<SoundEffect> CutSenceSFX;
        public List <SoundEffect> FootStepSFX;
        public Song MusicMainMenu,
                    MusicLevel,
                    MusicGhostAttack,
                    MusicEnding,
                    MusicEnding2;
        public Song[] MusicCutScene = new Song[5];

        public Intro_Screen mIntro_Screen;
        public MainMenu mMainMenu;
        public PauseAndDiedMenu mPauseDiedMenu;
        public Screen ScreenCurrent;
        public Level1 mLevel1;
        public Level2 mLevel2;
        public Level3 mLevel3;
        public Level4 mLevel4;
        public EndingScene mEndingScene;
        public Transition_Game GetTransition;
        public GameIn GetTransitionIn;
        public GameOut GetTransitionOut;
        public Effect_Level2 GetEffectLevel2;
        public JumpScareEffact GetJumpScareEffact;

        public Vector2 MousePosition;
        public int FrameTransitionIn,
                   FrameTransitionOut,
                   Level = 0,
                   Holy_Num = 0,
                   Text_Num = 0,
                   GhostPos = -192,
                   SpeedGhostLevel1 = 0,
                   SpeedGhostLevel2 = 0,
                   GhostPos_Level2 = -192,
                   SpeedGhostLevel3 = 0,
                   GhostPos_Level3 = -192,
                   GhostWalkBackward = 0,
                   CharHart = 100;

        public bool PlayTransitionIn = false,
                    PlayTransitionOut = false,
                    PlayEffectLevel2 = false,
                    IsGhost_DiedLevel2 = false,
                    IsGhost_DiedLevel3 = false,
                    IsShowChar = true,
                    IsPlaySFXGhostSee = true,
                    PlayJumpScareEffact = false,
                    ISGhostBackward = false,
                    IsGhost_See = false,
                    IsPauseMenu = false,
                    IsDiedMenu = false,
                    IsPlayMusic = true,
                    IsPlaySFX = true,
                    IsReset = false,
                    IsPlaySFXCutSence = true,
                    IsplayMusicGAttack = true,
                    IsplayMusicLevel = false,
                    IsGamePause = false;

        public GameSystem()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            ButtonSFX = new List<SoundEffect>();
            CutSenceSFX = new List<SoundEffect>();
            SoundSFX = new List<SoundEffect>();
            FootStepSFX = new List<SoundEffect>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("November");
            mIntro_Screen = new Intro_Screen(this, new EventHandler(GameScreenEvent));
            mMainMenu = new MainMenu(this, new EventHandler(GameScreenEvent));
            mPauseDiedMenu = new PauseAndDiedMenu(this, new EventHandler(GameScreenEvent));
            mLevel1 = new Level1(this, new EventHandler(GameScreenEvent));
            mLevel2 = new Level2(this, new EventHandler(GameScreenEvent));
            mLevel3 = new Level3(this, new EventHandler(GameScreenEvent));
            mLevel4 = new Level4(this, new EventHandler(GameScreenEvent));
            mEndingScene = new EndingScene(this, new EventHandler(GameScreenEvent));
            GetTransitionIn = new GameIn(this, new EventHandler(GameTransitionEvent));
            GetTransitionOut = new GameOut(this, new EventHandler(GameTransitionEvent));
            GetEffectLevel2 = new Effect_Level2(this, new EventHandler(GameTransitionEvent));
            GetJumpScareEffact = new JumpScareEffact(this, new EventHandler(GameTransitionEvent));
            ScreenCurrent = mIntro_Screen;
            GetTransition = GetTransitionIn;

            //เสียงเท้า
            FootStepSFX.Add(Content.Load<SoundEffect>("EndingFootStep1"));
            FootStepSFX.Add(Content.Load<SoundEffect>("EndingFootStep2"));
            FootStepSFX.Add(Content.Load<SoundEffect>("EndingFootStep3"));
            //เสียง SFX
            ButtonSFX.Add(Content.Load<SoundEffect>("Button01"));
            ButtonSFX.Add(Content.Load<SoundEffect>("Button02"));
            ButtonSFX.Add(Content.Load<SoundEffect>("Button03"));
            CutSenceSFX.Add(Content.Load<SoundEffect>("CutSenceSFX"));
            //เสียงเดิน/กระโดด 0 - 2
            SoundSFX.Add(Content.Load<SoundEffect>("Walk_Footstep"));
            SoundSFX.Add(Content.Load<SoundEffect>("Jump Up"));
            SoundSFX.Add(Content.Load<SoundEffect>("Jump Landing"));
            //เสียงเก็บขวดน้ำมนต์ 3 - 6
            SoundSFX.Add(Content.Load<SoundEffect>("Item1SFX"));
            SoundSFX.Add(Content.Load<SoundEffect>("HolySFX"));
            SoundSFX.Add(Content.Load<SoundEffect>("HolyUseSFX"));
            SoundSFX.Add(Content.Load<SoundEffect>("TossItem"));
            //เสียงยันต์ 7 - 9
            SoundSFX.Add(Content.Load<SoundEffect>("TalismanSFX"));
            SoundSFX.Add(Content.Load<SoundEffect>("TalismanPickSFX"));
            SoundSFX.Add(Content.Load<SoundEffect>("TalismanPlaceSFX"));
            //เสียงผีร้อง 10 - 11
            SoundSFX.Add(Content.Load<SoundEffect>("GhostLauhing"));
            SoundSFX.Add(Content.Load<SoundEffect>("GhostScream"));
            //ตาย 12
            SoundSFX.Add(Content.Load<SoundEffect>("DeadSFX"));
            //วาร์ป 13 - 14
            SoundSFX.Add(Content.Load<SoundEffect>("WarpSFX"));
            SoundSFX.Add(Content.Load<SoundEffect>("DoorLevel2SFX"));
            //พลัง 15
            SoundSFX.Add(Content.Load<SoundEffect>("TalismanPowerSFX"));
            //เสียงน้อนเปรต 16 - 18
            SoundSFX.Add(Content.Load<SoundEffect>("GhostLevel4"));
            SoundSFX.Add(Content.Load<SoundEffect>("GhostAttack01"));
            SoundSFX.Add(Content.Load<SoundEffect>("GhostAttack02"));

            SoundEffect.MasterVolume = 1.0f;
            this.MusicMainMenu = Content.Load<Song>("MainMenuMusic");
            this.MusicEnding = Content.Load<Song>("MusicCredit");
            this.MusicEnding2 = Content.Load<Song>("MusicCredit2");
            this.MusicCutScene[4] = Content.Load<Song>("CutSenceMusic");
            this.MusicCutScene[0] = Content.Load<Song>("MusicCutSence1");
            this.MusicLevel = Content.Load<Song>("LevelMusic");
            this.MusicGhostAttack = Content.Load<Song>("GhostMusic");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            MouseState mouseState = Mouse.GetState();
            MousePosition.X = mouseState.X;
            MousePosition.Y = mouseState.Y;
            Textg1 = Convert.ToString(MousePosition + " <Out> " + FrameTransitionOut + " <In> " + FrameTransitionIn + " <GhostPos> " + GhostPos + " <IsPauseMenu> " + IsPauseMenu +" <>                               "+ IsplayMusicGAttack);
            if(IsReset == true)
            {
                mLevel1.Reset_To_Level();
                mLevel2.Reset_To_Level();
                mLevel3.Reset_To_Level();
                mLevel4.Reset_To_Level();
                mEndingScene.Reset();
                IsReset = false;
            }
            ScreenCurrent.UpdateGame(gameTime);
            GetTransition.Update(gameTime);
            GetEffectLevel2.Update(gameTime);
            GetJumpScareEffact.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //DrawGame
            ScreenCurrent.Draw(spriteBatch);

            //Effect
            if (PlayJumpScareEffact == true && IsGamePause == false)
            {
                GetJumpScareEffact.Draw(spriteBatch);
            }
            if (PlayEffectLevel2 == true)
            {
                GetEffectLevel2.Draw(spriteBatch);
            }

            //Transition
            if(PlayTransitionIn == true)
            {
                GetTransition = GetTransitionIn;
                GetTransition.Draw(spriteBatch);
            } else
            {
                FrameTransitionIn = 0;
            }
            if (PlayTransitionOut == true)
            {
                GetTransition = GetTransitionOut;
                GetTransition.Draw(spriteBatch);
            } else
            {
                FrameTransitionOut = 6;
            }


            //String
            //spriteBatch.DrawString(font, Textg1, new Vector2(20, 650), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void GameScreenEvent(object obj, EventArgs e)
        {
            ScreenCurrent = (Screen)obj;
        }
        public void GameTransitionEvent(object obj, EventArgs e)
        {
            GetTransition = (Transition_Game)obj;
        }
        public void ResetGame()
        {
            Text_Num = 0;
            PlayTransitionIn = false;
            PlayTransitionOut = false;
            PlayEffectLevel2 = false;
            IsGhost_DiedLevel2 = false;
            IsGhost_DiedLevel3 = false;
            IsShowChar = true;
            IsPlaySFXGhostSee = true;
            PlayJumpScareEffact = false;
            ISGhostBackward = false;
            IsGhost_See = false;
            IsPauseMenu = false;
            IsDiedMenu = false;
            IsPlayMusic = true;
            IsPlaySFX = true;
            IsPlaySFXCutSence = true;
            IsplayMusicGAttack = true;
            IsplayMusicLevel = false;
            IsGamePause = false;
            Level = 0;
            Holy_Num = 0;
            GhostPos = -192;
            SpeedGhostLevel1 = 0;
            SpeedGhostLevel2 = 0;
            GhostPos_Level2 = -192;
            SpeedGhostLevel3 = 0;
            GhostPos_Level3 = -192;
            GhostWalkBackward = 0;
            CharHart = 100;
        }
    }
}
