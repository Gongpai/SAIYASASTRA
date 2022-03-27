using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class Effect_Level2 : Transition_Game
    {
        GameSystem game;

        Texture2D EffectLevel2;
        Vector2 Effect_L2_Pos;
        float Sec_Scale_Effect2 = 0.1f;
        bool direction_EF_L2,
             PlaySFX = true;

        public Effect_Level2(GameSystem game, EventHandler theTransitionEvent) : base(theTransitionEvent)
        {
            EffectLevel2 = game.Content.Load<Texture2D>("Char_Power");
            this.game = game;
        }
        public override void Update(GameTime UpdateTime)
        {
            if (game.PlayEffectLevel2 == false)
            {
                
            }
            else
            {
                GameTransitionAndEffect.Invoke(game.GetEffectLevel2, new EventArgs());
                if (Sec_Scale_Effect2 > 0.0f && Sec_Scale_Effect2 < 20.0f && direction_EF_L2 == false)
                {
                    if (game.IsPlaySFX == true && PlaySFX == true)
                    {
                        game.SoundSFX[15].Play();
                        SoundEffect.MasterVolume = 0.4f;
                        PlaySFX = false;
                    }
                    Sec_Scale_Effect2 += 0.1f;
                    Effect_L2_Pos.X += 15f;
                    Effect_L2_Pos.Y += 15f;
                }
                else
                {

                    if (Sec_Scale_Effect2 < 20.0f && Sec_Scale_Effect2 > 0.0f && direction_EF_L2 == true)
                    {
                        Sec_Scale_Effect2 -= 0.1f;
                        Effect_L2_Pos.X -= 15f;
                        Effect_L2_Pos.Y -= 15f;
                    }
                    else
                    {
                        PlaySFX = false;
                        game.PlayEffectLevel2 = false;
                        Sec_Scale_Effect2 = 0.1f;
                        Effect_L2_Pos.X = 0;
                        Effect_L2_Pos.Y = 0;
                        direction_EF_L2 = false;
                    }

                }
                if (Sec_Scale_Effect2 >= 19.0f)
                {
                    if (game.IsPlaySFX == true)
                    {
                        SoundEffect.MasterVolume = 1.0F;
                        game.SoundSFX[11].Play();
                    }
                    game.PlayJumpScareEffact = false;
                    PlaySFX = false;
                    MediaPlayer.Stop();
                    game.IsplayMusicLevel = true;
                    direction_EF_L2 = true;
                    if(game.Level == 2)
                    {
                        game.IsGhost_DiedLevel2 = true;
                    }
                    if (game.Level == 3)
                    {
                        game.IsGhost_DiedLevel3 = true;
                    }
                }
            }
            base.Update(UpdateTime);
        }
        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(EffectLevel2, new Vector2(640 - Effect_L2_Pos.X / 2, 360 - Effect_L2_Pos.Y / 2), null, Color.White, 0f, new Vector2(0, 0), Sec_Scale_Effect2, SpriteEffects.None, 1);
            base.Draw(SpriteBatch);
        }
    }
}
