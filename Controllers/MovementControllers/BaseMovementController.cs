using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BulletHell.GameObjects;

namespace BulletHell.Controllers.MovementControllers
{
    public abstract class BaseMovementController
    {
        protected HashSet<GameObject> gameObjects;

        public BaseMovementController()
        {
            gameObjects = new HashSet<GameObject>();
        }

        /// <summary>
        /// Add reference to hashset of game objects
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(ref GameObject gameObject)
        {
            gameObjects.Add(gameObject);

            gameObject.RemoveHandler += Remove;
        }

        /// <summary>
        /// Remove reference from hashset of gtame objects
        /// </summary>
        /// <param name="gameObject"></param>
        public void Remove(ref GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        /// <summary>
        /// Reponse to the event of the object being removed after collision
        /// </summary>
        /// <param name="sender"></param>
        private void Remove(object sender)
        {
            GameObject gameObject = sender as GameObject;

            Remove(ref gameObject);
        }

        /// <summary>
        /// Move all game objects in hashset
        /// </summary>
        /// <param name="gameTime">current time in game</param>
        /// <returns></returns>
        public abstract void Move(GameTime gameTime);
    }
}
