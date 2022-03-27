using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class GameIn : Transition_Game
    {
        GameSystem game;
        Texture2D TransitionTexture;

        float TotalElapsed = 0,
              Scale_M = 8.0f;

        public GameIn(GameSystem game, EventHandler theTransitionEvent) : base(theTransitionEvent)
        {
            TransitionTexture = game.Content.Load<Texture2D>("Transition06");
            this.game = game;
        }
        public override void Update(GameTime UpdateTime)
        {
            if(game.PlayTransitionIn == false)
            {
                TotalElapsed = 0;
                Scale_M = 9.0f;
                game.FrameTransitionIn = 0;
            }
            else
            {
                GameTransitionAndEffect.Invoke(game.GetTransitionIn, new EventArgs());
            }
            UpdateAnima((float)UpdateTime.ElapsedGameTime.TotalSeconds);
            base.Update(UpdateTime);
        }

        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(TransitionTexture, new Vector2(0, 0), new Rectangle(150 * game.FrameTransitionIn, 300, 150, 256), Color.White, 0f, new Vector2(0, 0), Scale_M, SpriteEffects.None, 1);
            base.Draw(SpriteBatch);
        }
        public void UpdateAnima(float elapsed)
        {
            TotalElapsed += elapsed;
            if(TotalElapsed > 0.1f)
            {
                game.FrameTransitionIn += 1;
                TotalElapsed = 0;
            }
            if(game.FrameTransitionIn >= 8)
            {
                game.FrameTransitionIn = 0;
                game.PlayTransitionIn = false;
            }
        }
    }
}
