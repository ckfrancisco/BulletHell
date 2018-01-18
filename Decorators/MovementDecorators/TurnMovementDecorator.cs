using Microsoft.Xna.Framework;

namespace BulletHell.Decorators.MovementDecorators
{
    public class TurnMovementDecorator : BaseMovementDecorator
    {
        public Vector2 speed;

        public int turn;
        public Vector2 turnPoint;
        public Vector2 turnSpeed;

        public Vector2 finalSpeed;

        public TurnMovementDecorator(Vector2 speed, Vector2 turnPoint, float turnSpeed, Vector2 finalSpeed)
        {
            this.controllerName = "Turn";

            this.speed = speed;

            this.turn = -1;
            this.turnPoint = turnPoint;
            this.turnSpeed = new Vector2(turnSpeed * (finalSpeed.X - speed.X), turnSpeed * (finalSpeed.Y - speed.Y));

            this.finalSpeed = finalSpeed;
        }
    }
}
