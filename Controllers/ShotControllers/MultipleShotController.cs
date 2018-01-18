using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHell.GameObjects.Bullets;
using BulletHell.Decorators.ShotDecorators;
using BulletHell.GameObjects.Characters;
using BulletHell.Controllers.MovementControllers;
using BulletHell.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BulletHell.Controllers.ShotControllers
{
    public class MultipleShotController : BaseShotController
    {
        //private BaseShotDecorator previousShotDec;
        BaseShotController currController;
        BaseMovementController shotMovement;
        Character currChar;

        public MultipleShotController() : base()
        {
            currController = null;
            shotMovement = null;
        }

        public override List<Bullet> Shoot(ContentManager content, GameTime gameTime, Vector2 playerPosition)
        {
            List<Bullet> bullets = new List<Bullet>();

            for (int i = 0; i < characters.Count; i++)
            {
                Character character = characters.ElementAt<Character>(i);

                MultipleShotDecorator MPS = character.shot.shotDecorator as MultipleShotDecorator;
                if (gameTime.TotalGameTime.TotalSeconds - MPS.prevShot.TotalSeconds >= MPS.shotFreq.Seconds) //time to update controllers again
                {
                    MPS.prevShot = gameTime.TotalGameTime;

                    Random rnd = new Random();
                    int a = rnd.Next(0, MPS.shots.Count);
                    BaseShotDecorator shot = MPS.shots[a];
                    switch (shot.controllerName)
                    {
                        case "Linear":
                            (shot as LinearShotDecorator).numOfBullets = (shot as LinearShotDecorator).originalNumOfBullets;
                            currController = new LinearShotController();
                            break;
                        case "Circular":
                            currController = new CircularShotController();
                            break;
                        case "Curtain":
                            currController = new CurtainShotController();
                            break;
                        case "LinearX3":
                            (shot as LinearX3ShotDecorator).numOfBullets = (shot as LinearX3ShotDecorator).originalNumOfBullets;
                            currController = new LinearX3ShotController();
                            break;
                        case "Triangle":
                            currController = new TriangleShotController();
                            break;

                    }
                    switch (character.shot.movement.controllerName)
                    {
                        case "Linear":
                            shotMovement = new LinearMovementController();
                            break;
                        case "Turn":
                            shotMovement = new TurnMovementController();
                            break;
                        case "Teleport":
                            shotMovement = new TeleportMovementController();
                            break;
                        case "Multi":
                            shotMovement = new MultiPositionMovementController();
                            break;
                        case "Circular":
                            shotMovement = new CircularMovementController();
                            break;
                    }
                    Character temp = null;
                    if (character is MillionAnts)
                    {
                        temp = new MillionAnts(content, character.health, character.Position, character.movement, MPS.shots[a], character.offShot);
                    }
                    else if (character is Supernova)
                    {
                        temp = new Supernova(content, character.health, character.Position, character.movement, MPS.shots[a], character.offShot);
                    }
                    currChar = temp;
                    GameObject shotMove = currChar.shot;
                    
                    currController.Add(ref currChar);
                    shotMovement.Add(ref shotMove);
                }

                if(currChar != null)
                    currChar.Position = character.Position;

                if(shotMovement != null)
                    shotMovement.Move(gameTime);

                if(currController != null)
                    bullets.AddRange(currController.Shoot(content, gameTime, playerPosition));

            }
            return bullets;
        }
    }
}
