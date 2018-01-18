using Microsoft.Xna.Framework;
using BulletHell.GameObjects;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.MovementControllers
{
    public class TurnMovementController : BaseMovementController
    {
        public TurnMovementController() : base( )
        {
            
        }

        public void CheckBeginTurn(GameObject gameObject)
        {
            TurnMovementDecorator movement = gameObject.movement as TurnMovementDecorator;

            if (movement.speed.X >= 0)
            {
                if (movement.speed.Y >= 0)
                {
                    if (gameObject.X >= movement.turnPoint.X && gameObject.Y >= movement.turnPoint.Y)
                        movement.turn = 0;
                }
                else
                {
                    if (gameObject.Position.X >= movement.turnPoint.X && gameObject.Y <= movement.turnPoint.Y)
                        movement.turn = 0;
                }
            }
            else
            {
                if (movement.speed.Y >= 0)
                {
                    if (gameObject.X <= movement.turnPoint.X && gameObject.Y >= movement.turnPoint.Y)
                        movement.turn = 0;
                }
                else
                {
                    if (gameObject.X <= movement.turnPoint.X && gameObject.Position.Y <= movement.turnPoint.Y)
                        movement.turn = 0;
                }
            }
        }

        public void CheckFinishTurn(GameObject gameObject)
        {
            TurnMovementDecorator movement = gameObject.movement as TurnMovementDecorator;

            if (movement.turnSpeed.X >= 0)
            {
                if(movement.turnSpeed.Y >= 0)
                {
                    if (movement.speed.X >= movement.finalSpeed.X && movement.speed.Y >= movement.finalSpeed.Y)
                        movement.turn = 1;
                }
                else
                {
                    if (movement.speed.X >= movement.finalSpeed.X && movement.speed.Y <= movement.finalSpeed.Y)
                        movement.turn = 1;
                }
            }
            else
            {
                if (movement.turnSpeed.Y >= 0)
                {
                    if (movement.speed.X <= movement.finalSpeed.X && movement.speed.Y >= movement.finalSpeed.Y)
                        movement.turn = 1;
                }
                else
                {
                    if (movement.speed.X <= movement.finalSpeed.X && movement.speed.Y <= movement.finalSpeed.Y)
                        movement.turn = 1;
                }
            }

            if (movement.turn == 1)
                movement.speed = movement.finalSpeed;
        }

        public override void Move(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                TurnMovementDecorator movement = gameObject.movement as TurnMovementDecorator;

                gameObject.Position += movement.speed;

                if (movement.turn == -1)
                    CheckBeginTurn(gameObject);

                if (movement.turn == 0)
                {
                    movement.speed += movement.turnSpeed;
                    CheckFinishTurn(gameObject);
                }
            }
        }
    }
}
