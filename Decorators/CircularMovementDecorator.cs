using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BulletHell.GameObjects;

namespace BulletHell.Decorators.MovementDecorators
{
    public class CircularMovementDecorator : BaseMovementDecorator
    {

        public float radius;
        public float degree;
        public GameObject lead;


        public CircularMovementDecorator(GameObject Lead, float Radius, float StartDegree)
        {
            this.controllerName = "Circular";

            this.radius = Radius;
            this.degree = StartDegree;
            this.lead = Lead;
            

        }
    }
}
