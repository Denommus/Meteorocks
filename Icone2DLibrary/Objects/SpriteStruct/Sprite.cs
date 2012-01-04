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
        public float animationTime;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White, rotation, origin, scale, SpriteEffects.None, depth);
        }

        //Plays an animation from a given list of Texture2D's
        public void Animate(List<Texture2D> textures, float time, float animationRate)
        {
            //Keep track of the elapsed time 
            animationTime += time;

            //The given animation rate determines when we switch to the next texture in the list
            if (animationTime > animationRate / textures.Count)
            {
                //Switch to the next texture in the list, or the first texture if we're at the end. 
                if (textures.IndexOf(texture) < textures.Count - 1)
                {
                    texture = textures[textures.IndexOf(texture) + 1];
                }
                else
                {
                    texture = textures[0];
                }

                //Reset the elapsed time
                animationTime = 0;
            }
        }
    }
}
