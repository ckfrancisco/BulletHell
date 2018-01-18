using Microsoft.Xna.Framework;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.MovementControllers
{
    class TeleportMovementController : BaseMovementController
    {
        public TeleportMovementController() : base()
        {

        }

        public override void Move(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                TeleportMovementDecorator movement = gameObject.movement as TeleportMovementDecorator;

                if (gameTime.TotalGameTime - movement.pauseStart >= movement.pauseDuration)
                {
                    movement.i++;
                    movement.i %= movement.positions.Count;
                    gameObject.Position = movement.positions[movement.i];

                    movement.pauseStart = gameTime.TotalGameTime;
                }
            }
        }
    }
}
