using System;
using Microsoft.Xna.Framework;
using BulletHell.GameObjects;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.MovementControllers
{
    public class MultiPositionMovementController : BaseMovementController
    {
        public MultiPositionMovementController() : base()
        {

        }

        private Vector2 Speed(Vector2 p1, Vector2 p2, float speed)
        {
            float x = (p2.X - p1.X);
            float y = (p2.Y - p1.Y);
            float d = (float)Math.Sqrt(x*x + y*y);

            return new Vector2(speed * x / d, speed * y / d);
        }

        public void CheckDestination(GameObject gameObject, GameTime gameTime)
        {
            MultiPositionMovementDecorator movement = gameObject.movement as MultiPositionMovementDecorator;

            if (movement.speed.X >= 0)
            {
                if (movement.speed.Y >= 0)
                {
                    if (gameObject.Position.X >= movement.positions[movement.j].X && gameObject.Position.Y >= movement.positions[movement.j].Y)
                        movement.move = 0;
                }
                else
                {
                    if (gameObject.Position.X >= movement.positions[movement.j].X && gameObject.Position.Y <= movement.positions[movement.j].Y)
                        movement.move = 0;
                }
            }
            else
            {
                if (movement.speed.Y >= 0)
                {
                    if (gameObject.Position.X <= movement.positions[movement.j].X && gameObject.Position.Y >= movement.positions[movement.j].Y)
                        movement.move = 0;
                }
                else
                {
                    if (gameObject.Position.X <= movement.positions[movement.j].X && gameObject.Position.Y <= movement.positions[movement.j].Y)
                        movement.move = 0;
                }
            }

            if (movement.move == 0)
            {
                gameObject.Position = movement.positions[movement.j];
                movement.pauseStart = gameTime.TotalGameTime;


                movement.i++;
                movement.i %= movement.positions.Count;

                movement.j++;
                movement.j %= movement.positions.Count;
            }
        }

        public override void Move(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                MultiPositionMovementDecorator movement = gameObject.movement as MultiPositionMovementDecorator;

                if (movement.move == 0 && gameTime.TotalGameTime - movement.pauseStart >= movement.pauseDuration)
                {
                    movement.speed = Speed(movement.positions[movement.i], movement.positions[movement.j], movement.speedMax);

                    movement.move = 1;
                }

                if (movement.move == 1)
                {
                    gameObject.Position += movement.speed;
                    CheckDestination(gameObject, gameTime);
                }
            }
        }
    }
}
