﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Decorators.ShotDecorators
{
    public class CircularShotDecorator : BaseShotDecorator
    {
        public TimeSpan shotFreq;
        public TimeSpan prevShot;

        public TimeSpan burstFreq;
        public TimeSpan prevBurst;

        public float speed;

        public int shotsFired;

        public Vector2 startPosition;

        public CircularShotDecorator(TimeSpan shotFreq, float speed) : base()
        {
            this.controllerName = "Circular";

            this.shotFreq = shotFreq;
            this.prevShot = new TimeSpan(shotFreq.Ticks + 1);

            this.burstFreq = new TimeSpan(10000000);
            this.prevBurst = new TimeSpan(burstFreq.Ticks + 1);

            this.speed = speed;

            this.shotsFired = 0;
        }
    }
}
