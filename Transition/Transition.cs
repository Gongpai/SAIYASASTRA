using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAIYASASTRA
{
    public class Transition_Game
    {
        protected EventHandler GameTransitionAndEffect;
        public Transition_Game(EventHandler theTransition)
        {
            GameTransitionAndEffect = theTransition;
        }
        public virtual void Update(GameTime UpdateTime)
        {

        }
        public virtual void Draw(SpriteBatch SpriteBatch)
        {

        }
    }
}
