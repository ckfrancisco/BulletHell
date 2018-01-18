using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell
{
    public class Sidebar
    {
        public string texturePath;

        public Texture2D texture;
        public Vector2 position;

        public static int sideBarWidth = 500;
        public static int sideBarHeight = 1000;


        public Sidebar(ContentManager content)
        {
            texturePath = "Graphics//sidebar";

            position = new Vector2(1000, 0);
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sideBarWidth, sideBarHeight), null, Color.White);
        }
    }
}
