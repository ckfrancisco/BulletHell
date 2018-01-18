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
    public class TriangleShotController : BaseShotController
    {
        public TriangleShotController() : base()
        {

        }

        private Vector2 Speed(Vector2 p1, Vector2 p2, float speed)
        {
            float x = (p2.X - p1.X);
            float y = (p2.Y - p1.Y);
            float d = (float)Math.Sqrt(x * x + y * y);

            return new Vector2(speed * x / d, speed * y / d);
        }

        public override List<Bullet> Shoot(ContentManager content, GameTime gameTime, Vector2 playerPosition)
        {
            List<Bullet> bullets = new List<Bullet>();

            foreach (var character in characters)
            {
                TriangleShotDecorator shot = character.shot.shotDecorator as TriangleShotDecorator;

                if (shot.numOfBulletShots < shot.numOfBullets && gameTime.TotalGameTime - shot.prevShot >= shot.shotFreq)
                {
                    shot.prevShot = gameTime.TotalGameTime;
                    
                    shot.startPosition = character.Position;
                    shot.targetPosition = playerPosition;

                    Vector2 speed;

                    for (int i = 1; i <= ((shot.numOfBulletShots + 2) / 2); i++)
                    {
                        if(shot.numOfBulletShots % 2 == 0)
                        {
                            if (i == 1)
                            {
                                speed = Speed(shot.startPosition, shot.targetPosition, shot.speed);
                                bullets.Add(new EnemyBullet(content, shot.damage, shot.startPosition, character.texture, new LinearMovementDecorator(speed)));

                            }

                            else
                            {
                                speed = Speed(shot.startPosition, new Vector2(shot.targetPosition.X - (shot.bulletSpread * (i - 1)), shot.targetPosition.Y), shot.speed);
                                bullets.Add(new EnemyBullet(content, shot.damage, shot.startPosition, character.texture, new LinearMovementDecorator(speed)));

                                speed = Speed(shot.startPosition, new Vector2(shot.targetPosition.X + (shot.bulletSpread * (i - 1)), shot.targetPosition.Y), shot.speed);
                                bullets.Add(new EnemyBullet(content, shot.damage, shot.startPosition, character.texture, new LinearMovementDecorator(speed)));
                            }
                        }
                        
                        else
                        {
                            speed = Speed(shot.startPosition, new Vector2(shot.targetPosition.X - (shot.bulletSpread / 2) - (shot.bulletSpread * (i - 1)), shot.targetPosition.Y), shot.speed);
                            bullets.Add(new EnemyBullet(content, shot.damage, shot.startPosition, character.texture, new LinearMovementDecorator(speed)));

                            speed = Speed(shot.startPosition, new Vector2(shot.targetPosition.X + (shot.bulletSpread / 2) + (shot.bulletSpread * (i - 1)), shot.targetPosition.Y), shot.speed);
                            bullets.Add(new EnemyBullet(content, shot.damage, shot.startPosition, character.texture, new LinearMovementDecorator(speed)));
                        }
                    }

                    shot.numOfBulletShots++;

                    if (shot.numOfBulletShots == shot.numOfBullets)
                        shot.prevBurst = gameTime.TotalGameTime;
                }

                if (shot.numOfBulletShots >= shot.numOfBullets && gameTime.TotalGameTime - shot.prevBurst >= shot.burstFreq)
                    shot.numOfBulletShots = 0;
            }

            return bullets;
        }
    }
}
