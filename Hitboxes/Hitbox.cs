using _487_game.Decorators.MovementDecorators;
using _487_game.Decorators.ShotDecorators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487_game.Hitboxes
{
    public abstract class Hitbox : GameObjects.GameObject
    {
        /// <summary>
        /// Require content manager and starting position to call base constructor and Initialize(...) in subclasses
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public Hitbox(ContentManager content, Vector2 startPosition, BaseMovementDecorator movement, BaseShotDecorator shot): base(content, startPosition, movement)
        {

        }
    }
}
