using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Decorators.ShotDecorators
{
    public class MultipleShotDecorator : BaseShotDecorator
    {

        public List<BaseShotDecorator> shots;
        public TimeSpan shotFreq;
        public TimeSpan prevShot;

        public MultipleShotDecorator(List<BaseShotDecorator> Shots, TimeSpan shotFreq) : base()
        {
            shots = Shots;
            this.shotFreq = shotFreq;
            this.controllerName = "Multi";
        }
    }
}
