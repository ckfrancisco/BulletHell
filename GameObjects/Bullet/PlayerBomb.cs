using System;
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
    public class PlayerBomb : Bullet
    {
        /// <summary>
        /// Create an instance of a basic player bullet
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public PlayerBomb(ContentManager content, int damage, Vector2 startPosition, Texture2D shooterTexture, BaseMovementDecorator movement) : base(content, damage, startPosition, shooterTexture, movement)
        {
            this.texturePath = "Graphics\\PlayerBomb";
            InitializeBullet(content, startPosition, shooterTexture);
        }


        public override void InitializeBullet(ContentManager content, Vector2 startPosition, Texture2D shooterTexture)
        {
            texture = content.Load<Texture2D>(texturePath);

            position.X = ((startPosition.X + (shooterTexture.Width) / 2)) - (texture.Width / 2);
            position.Y = ((startPosition.Y + (shooterTexture.Height) / 2)) - (texture.Height / 2);

            hitbox = new System.Drawing.RectangleF(0, 0, 1000, 1000);
        }
    }
}
