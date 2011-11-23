using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Icone2DLibrary;
using Microsoft.Xna.Framework.Graphics;

namespace Icone2DLibrary.Objects
{
    class Bullet : Sprite
    {
        public override void Initialize()
        {
            texture = game.Content.Load<Texture2D>("Blah");
        }

        public override void Update(float seconds, Microsoft.Xna.Framework.Input.KeyboardState keyState)
        {
            throw new NotImplementedException();
        }
    }
}
