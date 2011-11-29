using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Icone2DLibrary.Objects.SpriteStruct
{
    public struct Sprite
    {
        public Texture2D texture;
        public float rotation;
        public float scale;
        public Vector2 position;
        public Vector2 origin;
        public float depth;


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White, rotation, origin, scale, SpriteEffects.None, depth);
        }
    }
}
