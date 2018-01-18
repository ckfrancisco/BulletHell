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
    class CurtainShotController : BaseShotController
    {
        public CurtainShotController() : base()
        {

        }

        public Vector2 XPositionOffset(Vector2 p, int x)
        {
            Vector2 offset = p;
            offset.X += x;

            return offset;
        }
        
        public override List<Bullet> Shoot(ContentManager content, GameTime gameTime, Vector2 playerPosition)
        {
            List<Bullet> bullets = new List<Bullet>();

            foreach (var character in characters)
            {
               CurtainShotDecorator shot = character.shot.shotDecorator as CurtainShotDecorator;

                if (shot.shotsFired < 7 && gameTime.TotalGameTime - shot.prevShot >= shot.shotFreq)
                {
                    shot.prevShot = gameTime.TotalGameTime;

                    if(shot.shotsFired % 2 == 0)
                        bullets.Add(new EnemyBullet(content, shot.damage, character.shot.Position, character.texture, new LinearMovementDecorator(new Vector2(0, 3))));

                    else
                    {
                        bullets.Add(new EnemyBullet(content, shot.damage, XPositionOffset(character.Position, -150), character.texture, new LinearMovementDecorator(new Vector2(0, 3))));
                        bullets.Add(new EnemyBullet(content, shot.damage, XPositionOffset(character.Position, 150), character.texture, new LinearMovementDecorator(new Vector2(0, 3))));
                    }

                    shot.shotsFired += 1;

                    if (shot.shotsFired == 7)
                        shot.prevBurst = gameTime.TotalGameTime;
                }

                else if (shot.shotsFired >= 7 && gameTime.TotalGameTime - shot.prevBurst >= shot.burstFreq)
                    shot.shotsFired = 0;
            }

            return bullets;
        }
    }
}
