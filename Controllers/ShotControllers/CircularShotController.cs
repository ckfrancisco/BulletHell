using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BulletHell.GameObjects.Bullets;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Decorators.ShotDecorators;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.ShotControllers
{
    class CircularShotController : BaseShotController
    {
        public CircularShotController() : base()
        {

        }

        private Vector2 Speed(float degree, float speed)
        {
            float X = speed * (float)Math.Cos(degree * (Math.PI / 180));
            float Y = speed * (float)Math.Sin(degree * (Math.PI / 180));

            return new Vector2(X, Y);
        }

        public override List<Bullet> Shoot(ContentManager content, GameTime gameTime, Vector2 playerPosition)
        {
            List<Bullet> bullets = new List<Bullet>();

            foreach(var character in characters)
            {
                CircularShotDecorator shot = character.shot.shotDecorator as CircularShotDecorator;

                if (shot.shotsFired < 3 && gameTime.TotalGameTime - shot.prevShot >= shot.shotFreq)
                {
                    shot.prevShot = gameTime.TotalGameTime;

                   
                    shot.startPosition = character.Position;


                    for (float degree = 0; degree < 360; degree += 360 / 8)
                    {
                        Vector2 speed = Speed(degree, shot.speed);
                        bullets.Add(new EnemyBullet(content, shot.damage, shot.startPosition, character.texture, new LinearMovementDecorator(speed)));
                    }

                    shot.shotsFired += 1;

                    if (shot.shotsFired == 3)
                        shot.prevBurst = gameTime.TotalGameTime;
                }

                if (shot.shotsFired >= 3 && gameTime.TotalGameTime - shot.prevBurst >= shot.burstFreq)
                    shot.shotsFired = 0;
            }

            return bullets;
        }
    }
}
