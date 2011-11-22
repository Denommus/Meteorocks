using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Icone2DLibrary
{
    public abstract class Sprite
    {
        protected void BeforeConstructor(Game game, string assetName)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>(assetName);
        }

        protected void AfterConstrutor()
        {
            origin = Vector2.Zero;
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        protected Game game;
        protected Texture2D texture;
        protected float rotation = 0;
        protected float scale = 1;
        protected Vector2 position = Vector2.Zero;
        protected Vector2 origin;

        public virtual void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
