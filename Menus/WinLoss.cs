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
    public class WinLoss : Menu
    {
        Texture2D background;

        public WinLoss(ContentManager content)
        {
            font = content.Load<SpriteFont>("Score");
            message = "YOU'VE LOST";
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            //Get background to draw on. Blank rectangle
            background = new Texture2D(gd, 1,1);
            background.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Draw(background, new Rectangle(345, 295, 325, 325), Color.Black);
            string temp = "\n\nSTATS:\nKills:" + kills.ToString() + "\nLevel:" + level.ToString() + "\nFinal Score:" + score.ToString() + "\n\nPress ESC to exit.";
            spriteBatch.DrawString(font, this.message + temp, new Vector2(350, 300), Color.White);
        }
    }
}
