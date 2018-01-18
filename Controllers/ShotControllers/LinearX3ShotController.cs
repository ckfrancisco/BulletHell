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
    class LinearX3ShotController : BaseShotController
    {
        public LinearX3ShotController () : base()
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
                LinearX3ShotDecorator shot = character.shot.shotDecorator as LinearX3ShotDecorator;

                if (shot.numOfBullets == 0)
                    continue;

                if (gameTime.TotalGameTime - shot.prevShot >= shot.shotFreq)
                {
                    shot.prevShot = gameTime.TotalGameTime;

                    Vector2 speed = Speed(character.Position, playerPosition, shot.speed);
                    Vector2 speed2 = new Vector2(-speed.X, speed.Y);
                    Vector2 speed3 = new Vector2(speed.X, -speed.Y);

                    bullets.Add(new EnemyBullet(content, shot.damage, character.shot.Position, character.texture, new LinearMovementDecorator(speed2)));
                    bullets.Add(new EnemyBullet(content, shot.damage, character.shot.Position, character.texture, new LinearMovementDecorator(speed)));
                    bullets.Add(new EnemyBullet(content, shot.damage, character.shot.Position, character.texture, new LinearMovementDecorator(speed3)));
                    shot.numOfBullets--;
                }
            }

            return bullets;
        }
    }
}
