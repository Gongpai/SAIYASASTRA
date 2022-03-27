using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace SAIYASASTRA
{
    public class Intro_Screen : Screen
    {
        Texture2D AnimeLogoTeam;
        GameSystem game;
        SpriteFont font;
        SoundEffect IntroSound;

        string Textg1 = "0";

        int Frame = 0,
            Sec = 0,
            Direction = 0;
        float TotalElapsed = 0;

        bool IsBackward = false;

        public Intro_Screen(GameSystem game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            IntroSound = game.Content.Load<SoundEffect>("IntroSound");
            font = game.Content.Load<SpriteFont>("November");
            AnimeLogoTeam = game.Content.Load<Texture2D>("LogoTeam");

            IntroSound.Play();

            this.game = game;
        }

        public override void UpdateGame(GameTime theTime)
        {
            UpdateAnimaLogo((float)theTime.ElapsedGameTime.TotalSeconds);
            Textg1 = Convert.ToString(Frame + "<>" + TotalElapsed + "<>" + (Frame < 8) + "<>" + IsBackward + "<>" + Sec);
            if(Frame == -1)
            {
                game.PlayTransitionOut = true;
                EventScreen.Invoke(game.mMainMenu, new EventArgs());
                return;
            }
            base.UpdateGame(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(AnimeLogoTeam, new Vector2(640 - (433 / 2), 360 - (260 / 2)), new Rectangle(433 * Frame, 260 * Direction, 433, 260), Color.White);
            //theBatch.DrawString(font, Textg1, new Vector2(20, 650), Color.White);
            base.Draw(theBatch);
        }
        
        public void UpdateAnimaLogo(float elapsed)
        {
            TotalElapsed += elapsed;
            if (TotalElapsed > 0.1f && IsBackward == false)
            {
                if (Frame < 8)
                {
                    Frame += 1;
                    TotalElapsed = 0;
                }
                else
                {
                    Sec += 1;
                    if(Sec > 150)
                    {
                        Direction = 1;
                        IsBackward = true;
                    }
                    
                }
            }
            else if (TotalElapsed > 0.1f && Frame > -1 && IsBackward == true)
            {
                Frame -= 1;
                TotalElapsed = 0;
            }
        }
        
    }
}
