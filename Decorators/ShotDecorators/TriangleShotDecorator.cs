using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Decorators.ShotDecorators
{
    public class TriangleShotDecorator : BaseShotDecorator
    {
        public TimeSpan shotFreq;
        public TimeSpan prevShot;

        public TimeSpan burstFreq;
        public TimeSpan prevBurst;

        public float speed;

        public int numOfBullets;
        public int numOfBulletShots;
        public int bulletSpread;

        public Vector2 startPosition;
        public Vector2 targetPosition;

        public TriangleShotDecorator(TimeSpan shotFreq, TimeSpan burstFreq, float speed, int numOfBullets, int bulletSpread) : base()
        {
            this.controllerName = "Triangle";

            this.shotFreq = shotFreq;
            this.prevShot = new TimeSpan(shotFreq.Ticks + 1);

            this.burstFreq = burstFreq;
            this.prevBurst = new TimeSpan(burstFreq.Ticks + 1);

            this.speed = speed;

            this.numOfBullets = numOfBullets;
            this.numOfBulletShots = 0;
            this.bulletSpread = bulletSpread;
        }
    }
}
