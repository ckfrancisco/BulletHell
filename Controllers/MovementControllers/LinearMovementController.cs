using Microsoft.Xna.Framework;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.MovementControllers
{
    public class LinearMovementController : BaseMovementController
    {
        public LinearMovementController() : base()
        {

        }

        public override void Move(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                LinearMovementDecorator movement = gameObject.movement as LinearMovementDecorator;

                gameObject.Position += movement.speed;
            }
        }
    }
}
