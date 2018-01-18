using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BulletHell.GameObjects.Characters
{
    public class Gromflomite : Character
    {
        /// <summary>
        /// Create an instance of a gromflomite
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public Gromflomite(ContentManager content, int health, Vector2 startPosition, BaseMovementDecorator movement, BaseShotDecorator shot, int offShot) : base(content, health, startPosition, movement, shot, offShot)
        {
            this.texturePath = "Graphics\\Gromflomite";
            Initialize(content, startPosition);
            this.health = health;
            this.scoreValue = 10000;
        }
    }
}
