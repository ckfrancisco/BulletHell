using Microsoft.Xna.Framework;

namespace BulletHell.Decorators.MovementDecorators
{
    public class LinearMovementDecorator : BaseMovementDecorator
    {
        public Vector2 speed;

        public LinearMovementDecorator(Vector2 speed)
        {
            this.controllerName = "Linear";
            this.speed = speed;
        }
    }
}
