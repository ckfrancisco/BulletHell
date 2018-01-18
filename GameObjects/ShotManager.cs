using BulletHell.Decorators.MovementDecorators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Decorators.ShotDecorators;

namespace BulletHell.GameObjects
{
    public class ShotManager : GameObject
    {
        public BaseShotDecorator shotDecorator;

        public ShotManager(ContentManager content, Vector2 startPosition, BaseMovementDecorator movement, BaseShotDecorator shotDecorator) : base(content, startPosition, movement)
        {
            this.shotDecorator = shotDecorator;
            this.Initialize(content, startPosition);
        }

        public override void Initialize(ContentManager content, Vector2 startPosition)
        {
            this.position = startPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //do nothing
        }
    }
}
