using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using BulletHell.GameObjects.Bullets;
using BulletHell.Decorators.ShotDecorators;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.ShotControllers
{
    public class PlayerShotController : BaseShotController
    {
        public PlayerShotController() : base()
        {

        }

        public override  List<Bullet> Shoot(ContentManager content, GameTime gameTime, Vector2 playerPosition)
        {
            List<Bullet> bullets = new List<Bullet>();

            foreach (var character in characters)
            {
                PlayerShotDecorator shot = character.shot.shotDecorator as PlayerShotDecorator;

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameTime.TotalGameTime - shot.prevShot >= shot.shotFreq)
                {
                    shot.prevShot = gameTime.TotalGameTime;
                    bullets.Add(new PlayerBullet(content, shot.damage, character.Position, character.texture, new LinearMovementDecorator(new Vector2(0, -10))));
                }

                if (Keyboard.GetState().IsKeyDown(Keys.B) && shot.bombs > 0)
                {
                    bullets.Add(new PlayerBomb(content, shot.bombDamage, character.Position, character.texture, null));
                    shot.bombs--;
                }
            }

            return bullets;
        }
    }
}
