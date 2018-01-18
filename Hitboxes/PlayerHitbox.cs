using _487_game.Decorators.MovementDecorators;
using _487_game.Decorators.ShotDecorators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487_game.Hitboxes
{
    public class PlayerHitbox : Hitbox
    {
        /// <summary>
        /// Create an instance of a player hitbox
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public PlayerHitbox(ContentManager content, Vector2 startPosition, BaseMovementDecorator movement, BaseShotDecorator shot) : base(content, startPosition, movement, shot)
        {
            texturePath = "Graphics\\playerHitboxTexture";
            startPosition.X = startPosition.X + 55;
            startPosition.Y = startPosition.Y + 25;
            Initialize(content, startPosition);
        }

        ///// <summary>
        ///// Updates the position of the player based on movement and slow down ability
        ///// </summary>
        //public override void Update()
        //{
        //    KeyboardState currentKeyboardState = Keyboard.GetState();
        //    Vector2 currMoveSpeed = currentKeyboardState.IsKeyDown(Keys.RightShift) || currentKeyboardState.IsKeyDown(Keys.LeftShift) ? movement / 2 : movement;

        //    if (currentKeyboardState.IsKeyDown(Keys.Left) &&
        //        position.X >= texture.Width * 0.08f)
        //    {
        //        position.X -= currMoveSpeed.X;
        //    }
        //    if (currentKeyboardState.IsKeyDown(Keys.Right) &&
        //        position.X <= (Level.levelWidth - (texture.Width + texture.Width * 0.08f)))
        //    {
        //        position.X += currMoveSpeed.X;
        //    }
        //    if (currentKeyboardState.IsKeyDown(Keys.Up) &&
        //        position.Y >= 0)
        //    {
        //        position.Y -= currMoveSpeed.Y;
        //    }
        //    if (currentKeyboardState.IsKeyDown(Keys.Down) &&
        //        position.Y <= (Level.levelHeight - texture.Height))
        //    {
        //        position.Y += currMoveSpeed.Y;
        //    }
        //}
    }
}
