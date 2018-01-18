using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Drawing;

namespace BulletHell.GameObjects.Characters
{
    public abstract class Character : GameObject
    {
        public int health;
        public int lives;
        public int scoreValue;
        //public BaseShotDecorator shot;
        public ShotManager shot;
        public Vector2 startP;
        public int startHealth;
        public int offShot;

        /// <summary>
        /// Require content manager and starting position to call base constructor and Initialize(...) in subclasses
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public Character(ContentManager content, int health, Vector2 startPosition, BaseMovementDecorator movement, BaseShotDecorator shot, int shotMan) : base(content, startPosition, movement)
        {
            this.health = health;
            this.startHealth = health;
            this.offShot = shotMan;//Shot manager mode. Keep track. 
            if(shotMan == 0)
            {
                this.shot = new ShotManager(content, startPosition, movement, shot); //todo figure out how to deside movement vs. circular 
            }
            else
            {
                this.shot = new ShotManager(content, startPosition, new CircularMovementDecorator(this, 100, 0), shot);//standard 100 radius start at 0 degress or right. 
            }
            this.lives = 3;
            this.startP = startPosition;
            this.scoreValue = 0;
        }

        public Character(ContentManager content, Character character) : base(content, character.position, character.movement)
        {
            this.health = character.health;
            this.shot = character.shot;
        }

        /// <summary>
        /// Decrement health by damage and remove when character dies
        /// </summary>
        /// <param name="d"></param>
        public void TakeDamage(int d)
        {
            health -= d;
        }

        public void ResetCharacter()
        {
            health = startHealth;
            position = startP;
            hitbox = new RectangleF(position.X + (texture.Width / 2) - 10, position.Y + (texture.Height / 2) - 10, 20, 20);
        }
    }
}
