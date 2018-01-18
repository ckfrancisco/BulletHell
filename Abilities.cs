using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell
{
    abstract class Ability
    {
        public GameObjects.Bullets.Bullet bullet;

        public abstract void castAbility();
    }

    // SUPERNOVA ABILITIES
   /*class MeteorShower : Ability
    {
        
    }*/

}
