﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;

namespace BulletHell.GameObjects.Bullets
{
    public class EnemyBullet : Bullet
    {
        public EnemyBullet(ContentManager content, int damage, Vector2 startPosition, Texture2D shooterTexture, BaseMovementDecorator movement) : base(content, damage, startPosition, shooterTexture, movement)
        {
            this.texturePath = "Graphics\\EnemyBullet";
            InitializeBullet(content, startPosition, shooterTexture);
        }
    }
}
