using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;
using System.Drawing;

namespace BulletHell.GameObjects
{
    public abstract class GameObject
    {
        public string texturePath;

        public Texture2D texture;
        protected Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                hitbox.X -= (position.X - value.X);
                hitbox.Y -= (position.Y - value.Y);
                position = value;
            }
        }

        public float X
        {
            get { return position.X; }
            set
            {
                hitbox.X -= (position.X - value);
                position.X = value;
            }
        }

        public float Y
        {
            get { return position.Y; }
            set
            {
                hitbox.Y -= (position.Y - value);
                position.Y = value;
            }
        }


        public BaseMovementDecorator movement;

        public RectangleF hitbox;

        public delegate void RemoveEventHandler(object sender);
        public event RemoveEventHandler RemoveHandler;

        /// <summary>
        /// Require content manager and starting position to call Initialize(...) in subclasses
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public GameObject(ContentManager content, Vector2 startPosition, BaseMovementDecorator movement)
        {
            this.movement = movement;
        }

        /// <summary>
        /// Used in subclasses to set texture and start position
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public virtual void Initialize(ContentManager content, Vector2 startPosition)
        {
            texture = content.Load<Texture2D>(texturePath);
            position = startPosition;
            hitbox = new RectangleF(position.X + 10, position.Y + 10, texture.Width - 20, texture.Height - 20);
        }

        /// <summary>
        /// Draw GameObject with sprite batch
        /// </summary>
        /// <param name="spriteBatch">sprite batch from game</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Remove reference from controllers
        /// </summary>
        public void Remove()
        {
            if(RemoveHandler != null)
                RemoveHandler(this);
        }
    }
}
