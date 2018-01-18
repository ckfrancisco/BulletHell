using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.Decorators.MovementDecorators
{
    public class MultiPositionMovementDecorator : BaseMovementDecorator
    {
        public int i;
        public int j;
        public int move;
        public List<Vector2> positions;
        public Vector2 speed;
        public float speedMax;
        public TimeSpan pauseDuration;
        public TimeSpan pauseStart;

        public MultiPositionMovementDecorator(List<Vector2> positions, float speedMax, TimeSpan pauseDuration)
        {
            this.controllerName = "Multi";

            this.i = 0;
            this.j = (i + 1) % positions.Count;
            this.move = 0;
            this.positions = positions;

            this.speedMax = speedMax;

            this.pauseDuration = pauseDuration;
            this.pauseStart = new TimeSpan(0);
        }
    }
}
