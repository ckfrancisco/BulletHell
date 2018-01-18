using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHell.GameObjects.Bullets;
using BulletHell.GameObjects.Characters;
using System.Drawing;
using BulletHell.GameObjects;
using BulletHell.Menus;

namespace BulletHell.Controllers.CollisionControllers
{
    public class BaseCollisionController
    {
        protected HashSet<Character> characters;
        protected HashSet<Bullet> bullets;
        public bool isPlayerDead;//Better place to track?
        public int playerKills;//better place to track?
        public bool isCheating = false;

        public BaseCollisionController(ref HashSet<Character> characters, ref HashSet<Bullet> bullets)
        {
            this.characters = characters;
            this.bullets = bullets;
            this.isPlayerDead = false;
            playerKills = 0;
        }

        
        protected bool CheckBounds(GameObject gameObject)
        {
            if (gameObject.X < -200 || gameObject.X > 1100 || gameObject.Y < -200 || gameObject.Y > 1100)
                return false;
            else
                return true;
        }

        protected bool IsBulletCollision(Character character, Bullet bullet)
        {
            RectangleF tmp = RectangleF.Intersect(character.hitbox, bullet.hitbox);

            if (!tmp.IsEmpty)
                return true;
            else
                return false;
        }

        protected bool IsCharacterCollision(Character character, Character enemy)
        {
            RectangleF tmp = RectangleF.Intersect(character.hitbox, enemy.hitbox);

            if (!tmp.IsEmpty)
                return true;
            else
                return false;
        }

        public void DetectCollisions()
        {
            HashSet<Character> removeCharacters = new HashSet<Character>();
            HashSet<Bullet> removeBullets = new HashSet<Bullet>();

            foreach(var character in characters)
            {
                if (character is Player)
                {
                    if (isCheating)
                        continue;
                    foreach (var character2 in characters)
                    {
                        if (!(character2 is Player))
                        {
                            if (IsCharacterCollision(character, character2))
                            {
                                removeCharacters.Add(character);
                                removeCharacters.Add(character2);
                            }
                        }
                    }
                }

                foreach (var bullet in bullets)
                {
                    if ((character is Player && !(bullet is PlayerBullet)) && !(bullet is PlayerBomb) || (!(character is Player) && (bullet is PlayerBullet || bullet is PlayerBomb)))
                    {
                        if (IsBulletCollision(character, bullet))
                        {
                             character.TakeDamage(bullet.damage);

                            removeBullets.Add(bullet);

                            if (character.health <= 0)
                            {
                                if (!(character is Player))
                                {
                                    Stats.updateKills();
                                    Stats.updateScore(character.scoreValue);
                                }

                                removeCharacters.Add(character);
                            }
                        }
                    }

                    if(bullet is PlayerBomb)
                        removeBullets.Add(bullet);
                }
                //Maybe each object should check it's own bounds?
                if (!CheckBounds(character))
                    removeCharacters.Add(character);
            }
            //Bullets too
            foreach(var bullet in bullets)
                if (!CheckBounds(bullet))
                    removeBullets.Add(bullet);

            foreach (var character in removeCharacters)
            {
                if (character is Player && character.lives > 0)
                {
                    character.lives--;
                    Stats.removeLive();
                    character.ResetCharacter();

                    foreach (var bullet in bullets)
                    {
                        removeBullets.Add(bullet);
                    }
                }
                else
                {
                    characters.Remove(character);
                    character.Remove();
                }

                if (character.lives == 0)
                {
                    isPlayerDead = true;
                }
            }

            foreach(var bullet in removeBullets)
            {
                bullets.Remove(bullet);
                bullet.Remove();
            }
        }

      
    }
}
