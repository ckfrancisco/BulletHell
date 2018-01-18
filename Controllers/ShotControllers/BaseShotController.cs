using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BulletHell.GameObjects.Characters;
using BulletHell.GameObjects.Bullets;

namespace BulletHell.Controllers.ShotControllers
{
    public abstract class BaseShotController
    {
        protected HashSet<Character> characters;

        public BaseShotController()
        {
            characters = new HashSet<Character>();
        }

        /// <summary>
        /// Add reference to hashset of characters
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(ref Character character)
        {
            characters.Add(character);

            character.RemoveHandler += Remove;
        }

        /// <summary>
        /// Remove reference from hashset of gtame objects
        /// </summary>
        /// <param name="gameObject"></param>
        public void Remove(ref Character character)
        {
            characters.Remove(character);
        }

        /// <summary>
        /// Reponse to the event of the object being removed after collision
        /// </summary>
        /// <param name="sender"></param>
        private void Remove(object sender)
        {
            Character character = sender as Character;

            Remove(ref character);
        }

        /// <summary>
        /// Allow characters to shoot
        /// </summary>
        /// <param name="gameTime">current time in game</param>
        /// <returns></returns>
        public abstract List<Bullet> Shoot(ContentManager content, GameTime gameTime, Vector2 playerPosition);
    }
}
