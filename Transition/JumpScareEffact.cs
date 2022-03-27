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
    public class JumpScareEffact : Transition_Game
    {
        GameSystem game;
        Texture2D[] JumpScareSprite = new Texture2D[10];
        Random r = new Random();
        int RandomSprite = 0,
            Sec = 0;
        public JumpScareEffact(GameSystem game, EventHandler theTransitionEvent) : base(theTransitionEvent)
        {
            JumpScareSprite[0] = game.Content.Load<Texture2D>("jumpscare1");
            JumpScareSprite[1] = game.Content.Load<Texture2D>("jumpscare2");
            JumpScareSprite[2] = game.Content.Load<Texture2D>("jumpscare3");
            JumpScareSprite[3] = game.Content.Load<Texture2D>("jumpscare4");
            JumpScareSprite[4] = game.Content.Load<Texture2D>("jumpscare5");
            JumpScareSprite[5] = game.Content.Load<Texture2D>("jumpscare6");
            JumpScareSprite[6] = game.Content.Load<Texture2D>("jumpscare7");
            JumpScareSprite[7] = game.Content.Load<Texture2D>("jumpscare8");
            JumpScareSprite[8] = game.Content.Load<Texture2D>("jumpscare9");
            JumpScareSprite[9] = game.Content.Load<Texture2D>("jumpscare10");
            this.game = game;
        }
        public override void Update(GameTime UpdateTime)
        {
            Sec += 1;
            if(Sec >= 10)
            {
                RandomSprite = r.Next(9);
                Sec = 0;
            }
            
            base.Update(UpdateTime);
        }
        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(JumpScareSprite[RandomSprite], Vector2.Zero, Color.White);
            base.Draw(SpriteBatch);
        }

    }
}
