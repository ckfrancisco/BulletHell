using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace BulletHell.Decorators.MovementDecorators
{
    public class TeleportMovementDecorator : BaseMovementDecorator
    {
        public int i;
        public List<Vector2> positions;
        public TimeSpan pauseDuration;
        public TimeSpan pauseStart;

        public TeleportMovementDecorator(List<Vector2> positions, long pauseDuration)
        {
            this.controllerName = "Teleport";

            this.i = 0;
            this.positions = positions;

            this.pauseDuration = System.TimeSpan.FromTicks(pauseDuration);
            this.pauseStart = new TimeSpan(0);
        }
    }
}
