using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.MovementControllers
{
    class CircularMovementController : BaseMovementController
    {
        public CircularMovementController() : base()
        {

        }

        public Vector2 nextPosition(Vector2 center, float Radius, float degree)
        {
            float X = Radius * (float)Math.Cos(degree * (Math.PI / 180));
            float Y = Radius * (float)Math.Sin(degree * (Math.PI / 180));


            return new Vector2((int)Math.Floor(X)+center.X, (int)Math.Floor(Y)+center.Y);
        }

        public override void Move(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                CircularMovementDecorator movement = gameObject.movement as CircularMovementDecorator;

                Vector2 curCenter = movement.lead.Position;
                gameObject.Position = this.nextPosition(curCenter, movement.radius, movement.degree); //Math for Center of curCenter + Radius based off degree.
                movement.degree += 10;
            }
        }
    }
}
