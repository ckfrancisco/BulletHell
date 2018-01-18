using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Decorators.ShotDecorators
{
    public class PlayerShotDecorator : BaseShotDecorator
    {
        public TimeSpan shotFreq;
        public TimeSpan prevShot;

        public int bombs;
        public int bombDamage;

        public PlayerShotDecorator(TimeSpan shotFreq) : base()
        {
            this.controllerName = "Player";

            this.shotFreq = shotFreq;
            this.prevShot = new TimeSpan(shotFreq.Ticks + 1);

            this.bombs = 1;
            this.bombDamage = 99;
        }
    }
}
