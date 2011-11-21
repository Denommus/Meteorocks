using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Meteorocks.Objects
{
    public class Ship
    {
        public Ship(Game game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>(@"Sprites/shipSprite");
            origin = Vector2.Zero;
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            position = Vector2.Zero;
            position.X = game.GraphicsDevice.Viewport.Width / 2;
            position.Y = game.GraphicsDevice.Viewport.Height / 2;
        }

        Game game;
        Vector2 position;
        Vector2 origin;
        const float acceleration = 250.0f;
        const float maximumSpeed = 250.0f;
        Vector2 speed = Vector2.Zero;
        float scale = 0.7f;
        float rotation = 0.0f;
        Texture2D texture;

        public void Update(float seconds, KeyboardState keyState)
        {
            Viewport viewport = game.GraphicsDevice.Viewport;
            if (keyState.IsKeyDown(Keys.Right))
                rotation += 5 * seconds;
            if (keyState.IsKeyDown(Keys.Left))
                rotation -= 5 * seconds;

            rotation = MathHelper.WrapAngle(rotation);

            if (keyState.IsKeyDown(Keys.Up))
            {
                speed.X += acceleration * seconds * (float)Math.Sin(rotation);
                speed.Y -= acceleration * seconds * (float)Math.Cos(rotation);
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                speed.X -= acceleration * seconds * (float)Math.Sin(rotation);
                speed.Y += acceleration * seconds * (float)Math.Cos(rotation);
            }

            if (speed.LengthSquared() > maximumSpeed * maximumSpeed)
            {
                speed.Normalize();
                speed *= maximumSpeed;
            }


            position += speed * seconds;
            if (position.X > viewport.Width)
                position.X -= viewport.Width;
            if (position.X < 0)
                position.X += viewport.Width;

            if (position.Y > viewport.Height)
                position.Y -= viewport.Height;
            if (position.Y < 0)
                position.Y += viewport.Height;
        }

        public void Draw(SpriteBatch sprite)
        {
            Viewport viewport = game.GraphicsDevice.Viewport;
            sprite.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White, rotation, origin, scale, SpriteEffects.None, 0f);

            if (position.X > (viewport.Width - (scale * texture.Width)))
                sprite.Draw(texture, position - new Vector2(viewport.Width, 0), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.X < (scale * texture.Width))
                sprite.Draw(texture, position + new Vector2(viewport.Width, 0), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);

            if (position.Y > (viewport.Height - (scale * texture.Height)))
                sprite.Draw(texture, position - new Vector2(0, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            if (position.Y < (scale * texture.Height))
                sprite.Draw(texture, position + new Vector2(0, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
