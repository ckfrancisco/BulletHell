using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Decorators.ShotDecorators
{
    public class LinearShotDecorator : BaseShotDecorator
    {
        public TimeSpan shotFreq;
        public TimeSpan prevShot;

        public float speed;

        public int numOfBullets;
        public int originalNumOfBullets;

        public LinearShotDecorator(TimeSpan shotFreq, float speed, int numOfBullets) : base()
        {
            this.controllerName = "Linear";

            this.shotFreq = shotFreq;
            this.prevShot = new TimeSpan(shotFreq.Ticks + 1);

            this.speed = speed;

            this.numOfBullets = this.originalNumOfBullets = numOfBullets;
        }
    }
}
