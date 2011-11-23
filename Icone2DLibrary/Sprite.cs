using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Icone2DLibrary
{
    public abstract class Sprite
    {
        public void BeforeInitialize(Scene scene)
        {
            this.game = scene.Game;
            this.scene = scene;
        }

        public abstract void Initialize();

        public void AfterInitialize()
        {
            origin = Vector2.Zero;
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        protected Game game;
        protected Scene scene;
        protected Texture2D texture;
        protected float rotation = 0;
        protected float scale = 1;
        protected Vector2 position = Vector2.Zero;
        protected Vector2 origin;

        public abstract void Update(float seconds);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
