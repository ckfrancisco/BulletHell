using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;
using System.Drawing;

namespace BulletHell.GameObjects.Characters
{
    public class Player : Character
    {
        private string hitboxTextureStr;
        private Texture2D hitboxTexture;

        //// <summary>
        /// Create an instance of a player
        /// </summary>
        /// <param name="content">content manager from game</param>
        /// <param name="startPosition">starting positon</param>
        public Player(ContentManager content, int health, Vector2 startPosition, BaseMovementDecorator movement, BaseShotDecorator shot) : base(content, health, startPosition, movement, shot, 0)
        {
            this.texturePath = "Graphics\\Player";
            this.hitboxTextureStr = "Graphics\\playerHitboxTexture";
            this.hitboxTexture = content.Load<Texture2D>(hitboxTextureStr);
            Initialize(content, startPosition);


            hitbox = new RectangleF(position.X + (texture.Width / 2) - 10, position.Y + (texture.Height / 2) - 10, 20, 20);
        }
        

        /// <summary>
        /// Draw Player with sprite batch and hitbox if shift is held down
        /// </summary>
        /// <param name="spriteBatch">sprite batch from game</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            spriteBatch.Draw(texture, position, null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            if (currentKeyboardState.IsKeyDown(Keys.RightShift) || currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                spriteBatch.Draw(hitboxTexture, new Vector2(position.X + hitboxTexture.Width * 3, position.Y + hitboxTexture.Height), null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                //spriteBatch.Draw(texture, new Vector2(hitbox.X, hitbox.Y), null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
