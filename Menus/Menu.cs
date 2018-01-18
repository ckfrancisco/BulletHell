using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Menus
{
    public abstract class Menu
    {
        //Size of screen part to overwrite. 
        public const int levelWidth = 1000;
        public const int levelHeight = 1000;
        public string message;
        protected static int lives;
        protected static int kills;
        protected static int score;
        protected static int level;

        protected SpriteFont font;

    }
}
