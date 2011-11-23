using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Icone2DLibrary;

namespace Icone2DLibrary.Objects
{
    public class Ship : Sprite
    {
        public override void Initialize()
        {
            texture = game.Content.Load<Texture2D>(@"Sprites/shipSprite");
            position = Vector2.Zero;
            position.X = game.GraphicsDevice.Viewport.Width / 2;
            position.Y = game.GraphicsDevice.Viewport.Height / 2;

            scale = 0.7f;
        }

        const float acceleration = 250.0f;
        const float maximumSpeed = 250.0f;
        float timeUntilNextShot = 0.0f;
        Vector2 speed = Vector2.Zero;
        KeyboardState keyState;

        public override void Update(float seconds)
        {
            if (timeUntilNextShot > 0)
                timeUntilNextShot -= seconds;
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

            if (timeUntilNextShot <= 0 && keyState.IsKeyDown(Keys.Space))
            {
                scene.AddBullet(rotation, position);
                timeUntilNextShot = 1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Viewport viewport = game.GraphicsDevice.Viewport;
            base.Draw(spriteBatch);

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
        }

        public KeyboardState KeyState
        {
            set{keyState=value;}
        }
    }
}
