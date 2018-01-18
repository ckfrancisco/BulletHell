using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell.Decorators.ShotDecorators
{
    public class BaseShotDecorator
    {
        public string controllerName;
        public int damage;

        public BaseShotDecorator()
        {
            this.damage = 1;
        }
    }
}
