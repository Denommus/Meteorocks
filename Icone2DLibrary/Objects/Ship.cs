using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Icone2DLibrary.SceneManagement;
using Icone2DLibrary.Objects.SpriteStruct;
using Icone2DLibrary.Physics;
using System.Collections.Generic;

namespace Icone2DLibrary.Objects
{
    public class Ship : ISceneObject
    {
        #region Attributes
        Scene scene;
        Game game;
        const float acceleration = 250.0f;
        const float maximumSpeed = 250.0f;
        float timeUntilNextShot = 0.0f;
        Vector2 speed = Vector2.Zero;
        KeyboardState keyState;
        Sprite sprite = new Sprite();
        Circle circle;
        List<Texture2D> spriteReel;
        int lives = 3;
        #endregion

        #region Methods
        #region Initialization
        public void Initialize(Scene scene, Random random)
        {
            this.scene = scene;
            game = scene.Game;
            sprite.texture = game.Content.Load<Texture2D>(@"Sprites/shipSprite");

            sprite.scale = 0.7f;
            sprite.depth = 0;

            sprite.origin = new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2);
            circle.radius = sprite.origin.X < sprite.origin.Y ? sprite.origin.X : sprite.origin.Y;
            circle.radius *= sprite.scale;

            ResetPositions();

            //Animation reel for the ship's rockets
            spriteReel = new List<Texture2D>();
            spriteReel.Add(game.Content.Load<Texture2D>(@"Sprites/shipSpriteRocket1"));
            spriteReel.Add(game.Content.Load<Texture2D>(@"Sprites/shipSpriteRocket2"));
            spriteReel.Add(game.Content.Load<Texture2D>(@"Sprites/shipSpriteRocket3"));
        }

        private void ResetPositions()
        {
            sprite.position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            sprite.rotation = 0;
            speed = Vector2.Zero;
        }
        #endregion

        #region Update and Draw
        public void Update(float seconds)
        {
            if (timeUntilNextShot > 0)
                timeUntilNextShot -= seconds;
            Viewport viewport = game.GraphicsDevice.Viewport;
            if (keyState.IsKeyDown(Keys.Right))
                rotation += 5 * seconds;
            if (keyState.IsKeyDown(Keys.Left))
                rotation -= 5 * seconds;

            if (keyState.IsKeyDown(Keys.Up))
            {
                speed.X += acceleration * seconds * (float)Math.Sin(sprite.rotation);
                speed.Y -= acceleration * seconds * (float)Math.Cos(sprite.rotation);
                
                //Animate the ship's rockets while up is being pressed
                sprite.Animate(spriteReel, seconds, 0.3f);
            }
            
            //if the player is not holding up, reset the texture to the default shipSprite
            else
            {
                sprite.texture = game.Content.Load<Texture2D>(@"Sprites/shipSprite");
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                speed.X -= acceleration * seconds * (float)Math.Sin(sprite.rotation);
                speed.Y += acceleration * seconds * (float)Math.Cos(sprite.rotation);
            }

            if (speed.LengthSquared() > maximumSpeed * maximumSpeed)
            {
                speed.Normalize();
                speed *= maximumSpeed;
            }


            position += speed * seconds;
            if (position.X > viewport.Width)
                sprite.position.X -= viewport.Width;
            if (position.X < 0)
                sprite.position.X += viewport.Width;

            if (position.Y > viewport.Height)
                sprite.position.Y -= viewport.Height;
            if (position.Y < 0)
                sprite.position.Y += viewport.Height;

            if (timeUntilNextShot <= 0 && keyState.IsKeyDown(Keys.Space))
            {
                scene.AddBullet(rotation, position, speed);
                timeUntilNextShot = 0.5f;
            }

            circle.position = sprite.position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Viewport viewport = game.GraphicsDevice.Viewport;
            sprite.Draw(spriteBatch);

            if (position.X > (viewport.Width - (scale * texture.Width)))
                spriteBatch.Draw(texture, position - new Vector2(viewport.Width, 0), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);
            if (position.X < (scale * texture.Width))
                spriteBatch.Draw(texture, position + new Vector2(viewport.Width, 0), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);

            if (position.Y > (viewport.Height - (scale * texture.Height)))
                spriteBatch.Draw(texture, position - new Vector2(0, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);
            if (position.Y < (scale * texture.Height))
                spriteBatch.Draw(texture, position + new Vector2(0, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);

            if (position.X > (viewport.Width - (scale * texture.Width)) && position.Y > (viewport.Height - (scale * texture.Height)))
                spriteBatch.Draw(texture, position - new Vector2(viewport.Width, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);
            if (position.X > (viewport.Width - (scale * texture.Width)) && position.Y < (scale * texture.Height))
                spriteBatch.Draw(texture, position - new Vector2(viewport.Width, -viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);
            if (position.X < (scale * texture.Width) && position.Y > (viewport.Height - (scale * texture.Height)))
                spriteBatch.Draw(texture, position - new Vector2(-viewport.Width, viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);
            if (position.X < (scale * texture.Width) && position.Y < (scale * texture.Height))
                spriteBatch.Draw(texture, position - new Vector2(-viewport.Width, -viewport.Height), new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White, rotation, origin, scale, SpriteEffects.None, sprite.depth);
        }
        #endregion

        public void Die()
        {
            lives--;
            ResetPositions();
            if (lives < 0)
                game.Exit();
        }
        #endregion

        #region Properties
        public KeyboardState KeyState
        {
            set { keyState = value; }
        }

        Vector2 position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
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

        public Circle Circle { get { return circle; } }
        #endregion
    }
}
