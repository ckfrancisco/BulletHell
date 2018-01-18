using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _487_game
{
    class Enemy : Character
    {
        float moveSpeedX = 0;
        float moveSpeedY = 10;

        public Enemy()
        {
            this.moveSpeed = 10;
            this.texturePath = "Graphics\\enemy";
        }
       
        public void UseAbility(Ability ability) { }

        public override Bullet Shoot()
        {
            //todo
            return null;
        }

        public override void Update()
        {
            position.Y += moveSpeedY;
            position.X += moveSpeedX;

            moveSpeedX -= 0.1f;
            moveSpeedY -= 0.1f;

            if (moveSpeedY < 0)
                moveSpeedY = 0;

            if (moveSpeedX > 10)
                moveSpeedX = 10;
        }
    }
}


//    class SuperNova : Enemy {

//        public override void Initialize(Texture2D texture, Vector2 startPosition)
//        {
//            this.objectTexture = texture;
//            this.position = startPosition;
//        }
//        public override void Update(GameTime gameTime)
//        {
//            // if health >= 50%
//            // phase 1:

//            // if health < 50%
//            // phase 2:
//        }
//        public override void Draw(SpriteBatch spriteBatch)
//        {

//        }
//    }

//}
