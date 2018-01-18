namespace BulletHell.Decorators.MovementDecorators
{
    public class PlayerMovementDecorator : BaseMovementDecorator
    {
        public float speed;

        public PlayerMovementDecorator(float speed) : base()
        {
            this.controllerName = "Player";

            this.speed = speed;
        }
    }
}
