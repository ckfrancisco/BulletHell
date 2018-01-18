using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.IO;

namespace _487_game
{
    public class Player : Character
    {
        public Player()
        {
            texturePath = "Graphics\\rick_ship";
            moveSpeed = 10;
        }

        public override void Update() 
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            int currMoveSpeed = currentKeyboardState.IsKeyDown(Keys.RightShift) || currentKeyboardState.IsKeyDown(Keys.LeftShift) ? moveSpeed / 2 : moveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Left) &&
                position.X >= 0)
            {
                position.X -= currMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) &&
                position.X <= (Level.levelWidth - texture.Width))
            {
                position.X += currMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) &&
                position.Y >= 0)
            {
                position.Y -= currMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) &&
                position.Y <= (Level.levelHeight - texture.Height))
            {
                position.Y += currMoveSpeed;
            }
        }

        public override Bullet Shoot()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                return new Bullet(new Vector2(0, 5));
            }
            else
            {
                return null;
            }
        }
    }
}
