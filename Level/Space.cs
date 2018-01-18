using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BulletHell
{
    public class Space: Level
    {
        public Space(ContentManager content) : base(content)
        {
            texturePath = "Graphics\\Space";
            Initialize(content);
        }
    }
}
