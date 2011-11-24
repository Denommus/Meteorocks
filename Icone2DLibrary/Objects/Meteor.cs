using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Icone2DLibrary.SceneManagement;
using Icone2DLibrary.Objects.SpriteStruct;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Icone2DLibrary.Objects
{
    public class Meteor : ISceneObject
    {
        public void Initialize(Scene scene)
        {
            this.scene = scene;
            game = scene.Game;
            sprite.texture = game.Content.Load<Texture2D>(@"Sprites/Meteor");
            Random random = new Random(DateTime.Now.Millisecond);
            Viewport viewport = game.GraphicsDevice.Viewport;
            sprite.position = new Vector2(random.Next(viewport.Width, viewport.Height));
            speed = new Vector2(random.Next(viewport.Width, viewport.Height));
            angularSpeed = (float)random.NextDouble() * 100;

            sprite.scale = 1;

            sprite.rotation = 0;
            sprite.origin = new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2);
        }

        Sprite sprite = new Sprite();
        Scene scene;
        Game game;
        Vector2 speed;
        float angularSpeed;

        public void Update(float seconds)
        {
            Viewport viewport = game.GraphicsDevice.Viewport;

            sprite.rotation += seconds * angularSpeed;
            sprite.rotation = MathHelper.WrapAngle(sprite.rotation);

            sprite.position += speed;

            if (position.X > viewport.Width)
                sprite.position.X -= viewport.Width;
            if (position.X < 0)
                sprite.position.X += viewport.Width;

            if (position.Y > viewport.Height)
                sprite.position.Y -= viewport.Height;
            if (position.Y < 0)
                sprite.position.Y += viewport.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Viewport viewport = game.GraphicsDevice.Viewport;
            sprite.Draw(spriteBatch);

            if (position.X > (viewport.Width - (scale * texture.Width)))
                spriteBatch.Draw(texture, position - new Vector2(viewport.Width, 0), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.X < (scale * texture.Width))
                spriteBatch.Draw(texture, position + new Vector2(viewport.Width, 0), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);

            if (position.Y > (viewport.Height - (scale * texture.Height)))
                spriteBatch.Draw(texture, position - new Vector2(0, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.Y < (scale * texture.Height))
                spriteBatch.Draw(texture, position + new Vector2(0, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);

            if (position.X > (viewport.Width - (scale * texture.Width)) && position.Y > (viewport.Height - (scale * texture.Height)))
                spriteBatch.Draw(texture, position - new Vector2(viewport.Width, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.X > (viewport.Width - (scale * texture.Width)) && position.Y < (scale * texture.Height))
                spriteBatch.Draw(texture, position - new Vector2(viewport.Width, -viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.X < (scale * texture.Width) && position.Y > (viewport.Height - (scale * texture.Height)))
                spriteBatch.Draw(texture, position - new Vector2(-viewport.Width, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.X < (scale * texture.Width) && position.Y < (scale * texture.Height))
                spriteBatch.Draw(texture, position - new Vector2(-viewport.Width, -viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        Vector2 position
        {
            get { return sprite.position; }
            set
            {
                sprite.position = value;

            }
        }

        float scale
        {
            get { return sprite.scale; }
            set { sprite.scale = value; }
        }

        float rotation
        {
            get { return sprite.rotation; }
            set
            {
                sprite.rotation = value;
                sprite.rotation = MathHelper.WrapAngle(sprite.rotation);
            }
        }

        Texture2D texture
        {
            get { return sprite.texture; }
            set { sprite.texture = value; }
        }

        Vector2 origin
        {
            get { return sprite.origin; }
            set { sprite.origin = value; }
        }
    }
}
