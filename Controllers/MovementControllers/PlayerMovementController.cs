using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BulletHell.Decorators.MovementDecorators;

namespace BulletHell.Controllers.MovementControllers
{
    public class PlayerMovementController : BaseMovementController
    {
        public PlayerMovementController() : base()
        {

        }

        public override void Move(GameTime gameTime)
        {
            foreach(var gameObject in gameObjects)
            {
                PlayerMovementDecorator movement = gameObject.movement as PlayerMovementDecorator;

                KeyboardState currentKeyboardState = Keyboard.GetState();
                float speed = currentKeyboardState.IsKeyDown(Keys.RightShift) || currentKeyboardState.IsKeyDown(Keys.LeftShift) ? movement.speed / 2 : movement.speed;

                if (currentKeyboardState.IsKeyDown(Keys.Left) && gameObject.Position.X >= 0)
                {
                    gameObject.X -= speed;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Right) && gameObject.Position.X <= Level.levelWidth - gameObject.texture.Width)
                {
                    gameObject.X += speed;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up) && gameObject.Position.Y >= 0)
                {
                    gameObject.Y -= speed;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down) && gameObject.Position.Y <= Level.levelHeight - gameObject.texture.Height)
                {
                    gameObject.Y += speed;
                }
            }
        }
    }
}
