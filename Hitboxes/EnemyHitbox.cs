﻿using _487_game.Decorators.MovementDecorators;
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
    public class EnemyHitbox : Hitbox
    {
        /// <summary>
        /// Create an instance of a player
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public EnemyHitbox(ContentManager content, Vector2 startPosition, string texture, BaseMovementDecorator movement, BaseShotDecorator shot) : base(content, startPosition, movement, shot)
        {
            texturePath = texture;
            Initialize(content, startPosition);
        }
    }
}
