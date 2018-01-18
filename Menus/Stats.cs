using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Menus
{
    public class Stats : Menu
    {
        public Stats(ContentManager content)
        {
            font = content.Load<SpriteFont>("Score");
            message = "Current Stats";

            lives = 3;
            kills = 0;
            score = 0;
            level = 1;
        }

        public static void updateKills()
        {
            kills++;
        }

        public static void updateScore(int increment)
        {
            score += increment;
        }

        public static void updateLevel()
        {
            level++;
        }

        public static void removeLive()
        {
            lives--;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            string temp = "\n\nKills:" + kills.ToString() + "\nCurrent Level:" + level.ToString() + "\nScore:" + score.ToString() + "\nLives:" + lives.ToString();
            spriteBatch.DrawString(font, this.message + temp, new Vector2(1200, 200), Color.White);
        }
    }
}
