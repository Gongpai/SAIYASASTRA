using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class GameOut : Transition_Game
    {
        GameSystem game;
        Texture2D TransitionTexture;

        float TotalElapsed = 0,
              Scale_M = 8.0f;

        public GameOut(GameSystem game, EventHandler theTransitionEvent) : base(theTransitionEvent)
        {
            TransitionTexture = game.Content.Load<Texture2D>("Transition06");
            this.game = game;
        }
        public override void Update(GameTime UpdateTime)
        {
            if (game.PlayTransitionOut == false)
            {
                TotalElapsed = 0;
                Scale_M = 8.0f;
                game.FrameTransitionOut = 0;
            } else
            {
                GameTransitionAndEffect.Invoke(game.GetTransitionOut, new EventArgs());
            }
            UpdateAnima((float)UpdateTime.ElapsedGameTime.TotalSeconds);
            base.Update(UpdateTime);
        }

        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(TransitionTexture, new Vector2(0, 0), new Rectangle(180 * game.FrameTransitionOut, 300, 180, 256), Color.White, 0f, new Vector2(0, 0), Scale_M, SpriteEffects.None, 0.5f);
            base.Draw(SpriteBatch);
        }
        public void UpdateAnima(float elapsed)
        {
            TotalElapsed += elapsed;
            if (TotalElapsed > 0.1f)
            {
                game.FrameTransitionOut -= 1;
                TotalElapsed = 0;
            }
            if (game.FrameTransitionOut < 0)
            {
                game.FrameTransitionOut = 0;
                game.PlayTransitionOut = false;
            }
        }
    }
}
