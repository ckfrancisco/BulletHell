using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.GameObjects.Bullets
{
    public abstract class Bullet : GameObject
    {
        public int damage;
        
        /// <summary>
        /// Require content manager and starting position to call Initialize(...) in subclasses
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public Bullet(ContentManager content, int damage, Vector2 startPosition, Texture2D shooterTexture, BaseMovementDecorator movement) : base(content, startPosition, movement)
        {
            this.damage = damage;
        }

        /// <summary>
        /// Used in subclasses to set texture and start position of bullets with respect to the shooter
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        /// <param name="shooterTexture">texture of shooter</param>
        public virtual void InitializeBullet(ContentManager content, Vector2 startPosition, Texture2D shooterTexture)
        {
            texture = content.Load<Texture2D>(texturePath);

            position.X = ((startPosition.X + (shooterTexture.Width) / 2)) - (texture.Width / 2);
            position.Y = ((startPosition.Y + (shooterTexture.Height) / 2)) - (texture.Height / 2);

            hitbox = new RectangleF(position.X + 10, position.Y + 10, texture.Width - 20, texture.Height - 20);
        }
    }
}
