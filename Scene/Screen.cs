using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAIYASASTRA
{
    public class Screen
    {
        protected EventHandler EventScreen;

        public Screen(EventHandler theScreenEvent)
        {
            EventScreen = theScreenEvent;
        }
        public virtual void UpdateGame(GameTime theTime)
        {
            
        }
        public virtual void Draw(SpriteBatch theBatch)
        {

        }
    }
}
